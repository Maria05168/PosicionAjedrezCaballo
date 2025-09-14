using System;
using System.Collections.Generic;
using System.Linq;

namespace BeamValidationSystem
{
    // Clase abstracta base para todas las partes de la viga
    public abstract class BeamPart
    {
        public char Symbol { get; protected set; }
        public string Name { get; protected set; }

        protected BeamPart(char symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public abstract int CalculateWeight(int sequencePosition = 0);
        public abstract bool IsValidConnection(BeamPart previousPart);
    }

    // Clase Base - representa las bases de la viga (%, &, #)
    public class Base : BeamPart
    {
        public int Resistance { get; private set; }

        public Base(char symbol) : base(symbol, "Base")
        {
            switch (symbol)
            {
                case '%':
                    Resistance = 10;
                    break;
                case '&':
                    Resistance = 30;
                    break;
                case '#':
                    Resistance = 90;
                    break;
                default:
                    throw new ArgumentException("Símbolo de base inválido: " + symbol);
            }
        }

        public override int CalculateWeight(int sequencePosition = 0)
        {
            return 0; // Las bases no tienen peso
        }

        public override bool IsValidConnection(BeamPart previousPart)
        {
            return previousPart == null; // Solo puede ser el primer elemento
        }
    }

    // Clase Larguero - representa los largueros (=)
    public class Larguero : BeamPart
    {
        public Larguero() : base('=', "Larguero")
        {
        }

        public override int CalculateWeight(int sequencePosition = 0)
        {
            return sequencePosition; // El peso es igual a la posición en la secuencia
        }

        public override bool IsValidConnection(BeamPart previousPart)
        {
            // Puede conectarse con Base, otro Larguero o Conexion
            return previousPart is Base || previousPart is Larguero || previousPart is Conexion;
        }
    }

    // Clase Conexion - representa las conexiones (*)
    public class Conexion : BeamPart
    {
        public Conexion() : base('*', "Conexion")
        {
        }

        public override int CalculateWeight(int sequencePosition = 0)
        {
            // El peso se calcula como el doble del peso de la secuencia anterior
            // Este cálculo se manejará en el validador
            return 0;
        }

        public override bool IsValidConnection(BeamPart previousPart)
        {
            // Solo puede conectarse después de un Larguero, nunca después de otra Conexion
            return previousPart is Larguero;
        }
    }

    // Clase BeamValidator - maneja la validación y cálculo de la viga
    public class BeamValidator
    {
        public static BeamPart CreateBeamPart(char symbol)
        {
            switch (symbol)
            {
                case '%':
                case '&':
                case '#':
                    return new Base(symbol);
                case '=':
                    return new Larguero();
                case '*':
                    return new Conexion();
                default:
                    throw new ArgumentException("Símbolo inválido: " + symbol);
            }
        }

        public static bool IsValidBeamStructure(string beamString)
        {
            if (string.IsNullOrEmpty(beamString))
                return false;

            // Debe comenzar con una base
            char firstChar = beamString[0];
            if (firstChar != '%' && firstChar != '&' && firstChar != '#')
                return false;

            // Validar conexiones secuenciales
            BeamPart previousPart = null;
            
            for (int i = 0; i < beamString.Length; i++)
            {
                try
                {
                    BeamPart currentPart = CreateBeamPart(beamString[i]);
                    
                    if (!currentPart.IsValidConnection(previousPart))
                        return false;
                    
                    previousPart = currentPart;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }

            return true;
        }

        public static int CalculateTotalWeight(string beamString)
        {
            if (!IsValidBeamStructure(beamString))
                return -1;

            int totalWeight = 0;
            int currentSequenceWeight = 0;
            int largueroPosition = 1;

            for (int i = 1; i < beamString.Length; i++) // Empezar desde 1 para saltar la base
            {
                char symbol = beamString[i];
                
                if (symbol == '=')
                {
                    // Larguero: agregar peso según posición en la secuencia
                    currentSequenceWeight += largueroPosition;
                    largueroPosition++;
                }
                else if (symbol == '*')
                {
                    // Conexión: agregar el peso de la secuencia actual al total
                    totalWeight += currentSequenceWeight;
                    // Agregar el doble del peso de la secuencia por la conexión
                    totalWeight += currentSequenceWeight * 2;
                    // Reiniciar para nueva secuencia
                    currentSequenceWeight = 0;
                    largueroPosition = 1;
                }
            }

            // Agregar el peso de la última secuencia si no terminó en conexión
            totalWeight += currentSequenceWeight;

            return totalWeight;
        }

        public static bool CanBaseSupport(string beamString)
        {
            if (!IsValidBeamStructure(beamString))
                return false;

            Base baseBeam = (Base)CreateBeamPart(beamString[0]);
            int totalWeight = CalculateTotalWeight(beamString);
            
            return baseBeam.Resistance >= totalWeight;
        }
    }

    // Programa principal
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Validación de Vigas ===");
            Console.WriteLine();
            Console.WriteLine("Reglas:");
            Console.WriteLine("- Bases: % (resiste 10), & (resiste 30), # (resiste 90)");
            Console.WriteLine("- Largueros: = (peso secuencial: 1, 2, 3...)");
            Console.WriteLine("- Conexiones: * (peso doble de secuencia anterior)");
            Console.WriteLine("- La viga debe empezar con una base");
            Console.WriteLine("- Las conexiones (*) solo pueden seguir a largueros (=)");
            Console.WriteLine();

            // Ejecutar casos de prueba automáticamente
            RunTestCases();

            // Permitir entrada manual del usuario
            Console.WriteLine("\n=== Prueba Manual ===");
            
            while (true)
            {
                Console.Write("Ingrese la viga (o 'salir' para terminar): ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input) || input.ToLower() == "salir")
                    break;

                ProcessBeam(input);
                Console.WriteLine();
            }

            Console.WriteLine("¡Gracias por usar el sistema!");
        }

        public static void ProcessBeam(string beamString)
        {
            try
            {
                if (!BeamValidator.IsValidBeamStructure(beamString))
                {
                    Console.WriteLine("Ingrese la viga: " + beamString);
                    Console.WriteLine("La estructura de la viga es inválida!");
                    return;
                }

                bool canSupport = BeamValidator.CanBaseSupport(beamString);
                int totalWeight = BeamValidator.CalculateTotalWeight(beamString);
                
                Base baseBeam = (Base)BeamValidator.CreateBeamPart(beamString[0]);
                
                Console.WriteLine("Ingrese la viga: " + beamString);
                Console.WriteLine("Base: " + beamString[0] + " (resiste " + baseBeam.Resistance + " unidades)");
                Console.WriteLine("Peso total: " + totalWeight + " unidades");
                
                if (canSupport)
                {
                    Console.WriteLine("La viga soporta el peso!");
                }
                else
                {
                    Console.WriteLine("La viga NO soporta el peso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar la viga: " + ex.Message);
            }
        }

        public static void RunTestCases()
        {
            Console.WriteLine("=== Casos de Prueba Automáticos ===");
            
            string[] testCases = { "%", "%=*", "#===*==*=", "%===*==*====" };
            
            foreach (string testCase in testCases)
            {
                Console.WriteLine("\nPrueba: " + testCase);
                ProcessBeam(testCase);
            }
            
            Console.WriteLine("\n" + new string('=', 50));
        }
    }
}