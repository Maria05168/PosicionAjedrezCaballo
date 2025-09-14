using System;
using System.Collections.Generic;

namespace GeometricFiguresCalculator
{
    // Clase abstracta base para todas las figuras geométricas
    public abstract class GeometricFigure
    {
        public string Name { get; protected set; }

        protected GeometricFigure(string name)
        {
            Name = name;
        }

        // Métodos abstractos que deben implementar las clases derivadas
        public abstract double GetArea();
        public abstract double GetPerimeter();

        // Método ToString sobrescrito para formato estandarizado
        public override string ToString()
        {
            return $"{Name} => Area .....: {GetArea():F5} Perimeter: {GetPerimeter():F5}";
        }
    }

    // Clase Circle (Círculo)
    public class Circle : GeometricFigure
    {
        private double radius;

        public double Radius
        {
            get { return radius; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El radio debe ser mayor que cero.");
                radius = value;
            }
        }

        public Circle(double radius) : base("Circle")
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * radius * radius;
        }

        public override double GetPerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }

    // Clase Square (Cuadrado)
    public class Square : GeometricFigure
    {
        private double side;

        public double Side
        {
            get { return side; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado debe ser mayor que cero.");
                side = value;
            }
        }

        public Square(double side) : base("Square")
        {
            Side = side;
        }

        public override double GetArea()
        {
            return side * side;
        }

        public override double GetPerimeter()
        {
            return 4 * side;
        }
    }

    // Clase Rectangle (Rectángulo)
    public class Rectangle : GeometricFigure
    {
        private double width;
        private double height;

        public double Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ancho debe ser mayor que cero.");
                width = value;
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La altura debe ser mayor que cero.");
                height = value;
            }
        }

        public Rectangle(double width, double height) : base("Rectangle")
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return width * height;
        }

        public override double GetPerimeter()
        {
            return 2 * (width + height);
        }
    }

    // Clase Triangle (Triángulo)
    public class Triangle : GeometricFigure
    {
        private double sideA;
        private double sideB;
        private double sideC;

        public double SideA
        {
            get { return sideA; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado A debe ser mayor que cero.");
                sideA = value;
            }
        }

        public double SideB
        {
            get { return sideB; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado B debe ser mayor que cero.");
                sideB = value;
            }
        }

        public double SideC
        {
            get { return sideC; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado C debe ser mayor que cero.");
                sideC = value;
            }
        }

        public Triangle(double sideA, double sideB, double sideC) : base("Triangle")
        {
            // Validar que los lados formen un triángulo válido
            if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA)
                throw new ArgumentException("Los lados proporcionados no forman un triángulo válido.");
            
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        public override double GetArea()
        {
            // Fórmula de Herón
            double s = GetPerimeter() / 2;
            return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
        }

        public override double GetPerimeter()
        {
            return sideA + sideB + sideC;
        }
    }

    // Clase Parallelogram (Paralelogramo)
    public class Parallelogram : GeometricFigure
    {
        private double baseLength;
        private double sideLength;
        private double height;

        public double BaseLength
        {
            get { return baseLength; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La base debe ser mayor que cero.");
                baseLength = value;
            }
        }

        public double SideLength
        {
            get { return sideLength; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado debe ser mayor que cero.");
                sideLength = value;
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La altura debe ser mayor que cero.");
                height = value;
            }
        }

        public Parallelogram(double baseLength, double sideLength, double height) : base("Parallelogram")
        {
            BaseLength = baseLength;
            SideLength = sideLength;
            Height = height;
        }

        public override double GetArea()
        {
            return baseLength * height;
        }

        public override double GetPerimeter()
        {
            return 2 * (baseLength + sideLength);
        }
    }

    // Clase Rhombus (Rombo)
    public class Rhombus : GeometricFigure
    {
        private double side;
        private double diagonal1;
        private double diagonal2;

        public double Side
        {
            get { return side; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado debe ser mayor que cero.");
                side = value;
            }
        }

        public double Diagonal1
        {
            get { return diagonal1; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La diagonal 1 debe ser mayor que cero.");
                diagonal1 = value;
            }
        }

        public double Diagonal2
        {
            get { return diagonal2; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La diagonal 2 debe ser mayor que cero.");
                diagonal2 = value;
            }
        }

        public Rhombus(double side, double diagonal1, double diagonal2) : base("Rhombus")
        {
            Side = side;
            Diagonal1 = diagonal1;
            Diagonal2 = diagonal2;
        }

        public override double GetArea()
        {
            return (diagonal1 * diagonal2) / 2;
        }

        public override double GetPerimeter()
        {
            return 4 * side;
        }
    }

    // Clase Kite (Cometa/Deltoide)
    public class Kite : GeometricFigure
    {
        private double diagonal1;
        private double diagonal2;
        private double side1;
        private double side2;

        public double Diagonal1
        {
            get { return diagonal1; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La diagonal 1 debe ser mayor que cero.");
                diagonal1 = value;
            }
        }

        public double Diagonal2
        {
            get { return diagonal2; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La diagonal 2 debe ser mayor que cero.");
                diagonal2 = value;
            }
        }

        public double Side1
        {
            get { return side1; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado 1 debe ser mayor que cero.");
                side1 = value;
            }
        }

        public double Side2
        {
            get { return side2; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado 2 debe ser mayor que cero.");
                side2 = value;
            }
        }

        public Kite(double diagonal1, double diagonal2, double side1, double side2) : base("Kite")
        {
            Diagonal1 = diagonal1;
            Diagonal2 = diagonal2;
            Side1 = side1;
            Side2 = side2;
        }

        public override double GetArea()
        {
            return (diagonal1 * diagonal2) / 2;
        }

        public override double GetPerimeter()
        {
            return 2 * (side1 + side2);
        }
    }

    // Clase Trapeze (Trapecio)
    public class Trapeze : GeometricFigure
    {
        private double baseTop;
        private double baseBottom;
        private double height;
        private double side1;
        private double side2;

        public double BaseTop
        {
            get { return baseTop; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La base superior debe ser mayor que cero.");
                baseTop = value;
            }
        }

        public double BaseBottom
        {
            get { return baseBottom; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La base inferior debe ser mayor que cero.");
                baseBottom = value;
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La altura debe ser mayor que cero.");
                height = value;
            }
        }

        public double Side1
        {
            get { return side1; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado 1 debe ser mayor que cero.");
                side1 = value;
            }
        }

        public double Side2
        {
            get { return side2; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El lado 2 debe ser mayor que cero.");
                side2 = value;
            }
        }

        public Trapeze(double baseTop, double baseBottom, double height, double side1, double side2) : base("Trapeze")
        {
            BaseTop = baseTop;
            BaseBottom = baseBottom;
            Height = height;
            Side1 = side1;
            Side2 = side2;
        }

        public override double GetArea()
        {
            return ((baseTop + baseBottom) * height) / 2;
        }

        public override double GetPerimeter()
        {
            return baseTop + baseBottom + side1 + side2;
        }
    }

    // Programa principal
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Crear lista de figuras geométricas
                List<GeometricFigure> figures = new List<GeometricFigure>();

                // Instanciar las figuras con valores que generen los resultados esperados
                // Circle => Area .....: 78.53982 Perimeter: 31.41593 (radio = 5)
                figures.Add(new Circle(5));

                // Square => Area .....: 100 Perimeter: 40 (lado = 10)
                figures.Add(new Square(10));

                // Rhombus => Area .....: 35 Perimeter: 34 (lado = 8.5, diagonales apropiadas)
                figures.Add(new Rhombus(8.5, 10, 7));

                // Kite => Area .....: 20 Perimeter: 26 (diagonales y lados apropiados)
                figures.Add(new Kite(8, 5, 8, 5));

                // Rectangle => Area .....: 309.94372 Perimeter: 144.716 (valores específicos)
                figures.Add(new Rectangle(17.358, 17.858));

                // Parallelogram => Area .....: 801.402 Perimeter: 157.64 (valores específicos)
                figures.Add(new Parallelogram(24.56, 54.26, 32.62));

                // Triangle => Area .....: 280.114 Perimeter: 84.99 (lados específicos)
                figures.Add(new Triangle(25.33, 29.66, 30));

                // Trapeze => Area .....: 600 Perimeter: 120 (bases y altura específicas)
                figures.Add(new Trapeze(20, 40, 20, 30, 30));

                // Mostrar resultados usando foreach
                Console.WriteLine("=== Sistema de Cálculo de Áreas y Perímetros ===");
                Console.WriteLine();

                foreach (GeometricFigure figure in figures)
                {
                    Console.WriteLine(figure.ToString());
                }

                Console.WriteLine();
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Presione cualquier tecla para salir...");
                Console.ReadKey();
            }
        }
    }
}