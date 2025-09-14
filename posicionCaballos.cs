using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessKnightConflict
{
    // Estructura para representar una coordenada en el tablero
    public struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
            {
                Coordinate coord = (Coordinate)obj;
                return X == coord.X && Y == coord.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }

    // Clase Knight - representa un caballo en el tablero
    public class Knight
    {
        public string PositionAlgebraic { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Knight(string algebraicPosition)
        {
            PositionAlgebraic = algebraicPosition.ToUpper();
            ConvertAlgebraicToCoordinates();
        }

        // Convierte notación algebraica a coordenadas numéricas
        private void ConvertAlgebraicToCoordinates()
        {
            if (string.IsNullOrEmpty(PositionAlgebraic) || PositionAlgebraic.Length != 2)
                throw new ArgumentException("Posición algebraica inválida: " + PositionAlgebraic);

            char column = PositionAlgebraic[0];
            char row = PositionAlgebraic[1];

            // Convertir columna (A-H) a número (1-8)
            if (column >= 'A' && column <= 'H')
                X = column - 'A' + 1;
            else
                throw new ArgumentException("Columna inválida en posición: " + PositionAlgebraic);

            // Convertir fila (1-8) a número
            if (row >= '1' && row <= '8')
                Y = row - '0';
            else
                throw new ArgumentException("Fila inválida en posición: " + PositionAlgebraic);
        }

        // Obtiene todas las posiciones posibles que puede atacar este caballo
        public List<Coordinate> GetPossibleMoves()
        {
            List<Coordinate> possibleMoves = new List<Coordinate>();

            // Los 8 movimientos posibles del caballo en forma de L
            int[] deltaX = { 2, 2, -2, -2, 1, 1, -1, -1 };
            int[] deltaY = { 1, -1, 1, -1, 2, -2, 2, -2 };

            for (int i = 0; i < 8; i++)
            {
                int newX = X + deltaX[i];
                int newY = Y + deltaY[i];

                // Verificar que la nueva posición esté dentro del tablero (1-8)
                if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8)
                {
                    possibleMoves.Add(new Coordinate(newX, newY));
                }
            }

            return possibleMoves;
        }

        // Convierte coordenadas numéricas de vuelta a notación algebraica
        public static string CoordinateToAlgebraic(int x, int y)
        {
            if (x < 1 || x > 8 || y < 1 || y > 8)
                return "";

            char column = (char)('A' + x - 1);
            char row = (char)('0' + y);
            return column.ToString() + row.ToString();
        }

        public override string ToString()
        {
            return PositionAlgebraic + " (" + X + "," + Y + ")";
        }
    }

    // Clase ChessBoard - maneja el tablero y análisis de conflictos
    public class ChessBoard
    {
        public List<Knight> Knights { get; private set; }

        public ChessBoard()
        {
            Knights = new List<Knight>();
        }

        // Agrega un caballo al tablero
        public void AddKnight(Knight knight)
        {
            Knights.Add(knight);
        }

        // Agrega caballos desde una cadena de posiciones separadas por comas
        public void AddKnightsFromString(string positions)
        {
            if (string.IsNullOrEmpty(positions))
                return;

            string[] positionArray = positions.Split(',');
            foreach (string position in positionArray)
            {
                string trimmedPosition = position.Trim();
                if (!string.IsNullOrEmpty(trimmedPosition))
                {
                    try
                    {
                        Knight knight = new Knight(trimmedPosition);
                        AddKnight(knight);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Error al agregar caballo en posición " + trimmedPosition + ": " + ex.Message);
                    }
                }
            }
        }

        // Encuentra qué caballos están amenazados por un caballo específico
        public List<Knight> FindThreatenedKnights(Knight attacker)
        {
            List<Knight> threatenedKnights = new List<Knight>();
            List<Coordinate> possibleMoves = attacker.GetPossibleMoves();

            foreach (Knight targetKnight in Knights)
            {
                // No puede amenazarse a sí mismo
                if (targetKnight == attacker)
                    continue;

                // Verificar si el caballo objetivo está en alguna de las posiciones atacadas
                Coordinate targetPosition = new Coordinate(targetKnight.X, targetKnight.Y);
                foreach (Coordinate move in possibleMoves)
                {
                    if (move.Equals(targetPosition))
                    {
                        threatenedKnights.Add(targetKnight);
                        break;
                    }
                }
            }

            return threatenedKnights;
        }

        // Analiza todos los conflictos en el tablero
        public void AnalyzeConflicts()
        {
            Console.WriteLine("=== Análisis de Conflictos de Caballos ===");
            Console.WriteLine();

            foreach (Knight knight in Knights)
            {
                List<Knight> threatenedKnights = FindThreatenedKnights(knight);

                Console.Write("Analizando Caballo en " + knight.Y + knight.PositionAlgebraic[0] + " => ");

                if (threatenedKnights.Count == 0)
                {
                    Console.WriteLine("");
                }
                else
                {
                    for (int i = 0; i < threatenedKnights.Count; i++)
                    {
                        Knight threatened = threatenedKnights[i];
                        Console.Write("Conflicto con " + threatened.Y + threatened.PositionAlgebraic[0]);
                        
                        if (i < threatenedKnights.Count - 1)
                        {
                            Console.WriteLine("");
                            Console.Write("Conflicto con ");
                        }
                    }
                    Console.WriteLine("");
                }
            }
        }

        // Muestra información del tablero
        public void DisplayBoard()
        {
            Console.WriteLine("Caballos en el tablero:");
            foreach (Knight knight in Knights)
            {
                Console.WriteLine("- " + knight.ToString());
            }
            Console.WriteLine();
        }
    }

    // Programa principal
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Análisis de Caballos en Conflicto ===");
            Console.WriteLine();
            Console.WriteLine("Ingrese las posiciones de los caballos en notación algebraica");
            Console.WriteLine("(ejemplo: B7,C5,E2,H7,G5,F6)");
            Console.WriteLine();

            // Ejecutar caso de prueba automático
            RunTestCase();

            // Permitir entrada manual del usuario
            Console.WriteLine("\n=== Prueba Manual ===");
            
            while (true)
            {
                Console.Write("Ingrese ubicación de los caballos (o 'salir' para terminar): ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input) || input.ToLower() == "salir")
                    break;

                ProcessKnightPositions(input);
                Console.WriteLine();
            }

            Console.WriteLine("¡Gracias por usar el sistema!");
        }

        public static void ProcessKnightPositions(string positions)
        {
            try
            {
                ChessBoard board = new ChessBoard();
                
                Console.WriteLine("Ingrese ubicación de los caballos: " + positions);
                
                // Agregar caballos al tablero
                board.AddKnightsFromString(positions);
                
                if (board.Knights.Count == 0)
                {
                    Console.WriteLine("No se pudieron agregar caballos válidos.");
                    return;
                }

                // Mostrar información del tablero (opcional)
                // board.DisplayBoard();

                // Analizar conflictos
                board.AnalyzeConflicts();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar las posiciones: " + ex.Message);
            }
        }

        public static void RunTestCase()
        {
            Console.WriteLine("=== Caso de Prueba Automático ===");
            
            string testCase = "B7,C5,E2,H7,G5,F6";
            
            Console.WriteLine("Ejecutando caso de prueba: " + testCase);
            Console.WriteLine();
            
            ProcessKnightPositions(testCase);
            
            Console.WriteLine("\n" + new string('=', 50));
        }
    }
}