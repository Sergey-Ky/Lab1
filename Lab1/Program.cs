using System;

IShape[] shapes = new IShape[4];

shapes[0] = new Square();
shapes[1] = new Triangle();
shapes[2] = new EquilateralTriangle();
shapes[3] = new RightTriangle();
foreach (var shape in shapes)
{
    shape.DisplayProperties();
    Console.WriteLine($"Площадь: {shape.Area()}");
    Console.WriteLine();
}

Console.ReadLine();

public interface IShape
{
    int Area();
    void DisplayProperties();
}

public abstract class Shape : IShape
{
    private static readonly Random random = new Random();

    protected static int GetRandomSide(int min = 1, int max = 20)
    {
        return random.Next(min, max + 1);
    }

    public abstract int Area();
    public abstract void DisplayProperties();
}

public abstract class Quadrilateral : Shape
{
    public int SideA;
    public int SideB;
    public int SideC;
    public int SideD;

    protected Quadrilateral()
    {
        SideA = GetRandomSide();
        SideB = GetRandomSide();
        SideC = GetRandomSide();
        SideD = GetRandomSide();
    }
}

public class Square : Quadrilateral
{
    public Square()
    {
        int side = GetRandomSide();
        SideA = SideB = SideC = SideD = side;
    }

    public override int Area()
    {
        return SideA * SideA;
    }

    public override void DisplayProperties()
    {
        Console.WriteLine($"Квадрат со стороной: {SideA}");
    }

    ~Square()
    {
        return;
    }
}

public class Triangle : Shape
{
    public int SideA;
    public int SideB;
    public int SideC;

    public Triangle()
    {
        do {
            SideA = GetRandomSide();
            SideB = GetRandomSide();
            SideC = GetRandomSide();
        } while (!IsValidTriangle(SideA, SideB, SideC));
    }

    public bool IsValidTriangle(int a, int b, int c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    public override int Area()
    {
        // Формула Герона (результат округляется до целого)
        double p = (SideA + SideB + SideC) / 2.0;
        double area = Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        return (int)Math.Round(area);
    }

    public override void DisplayProperties()
    {
        Console.WriteLine($"Треугольник со сторонами: {SideA}, {SideB}, {SideC}");
    }

    ~Triangle()
    {
        return;
    }
}

public class IsoscelesTriangle : Triangle
{
    protected IsoscelesTriangle(int equalSides, int baseSide)
    {
        SideA = SideB = equalSides;
        SideC = baseSide;
    }

    public override void DisplayProperties()
    {
        Console.WriteLine($"Равнобедренный треугольник: равные стороны {SideA}, основание {SideC}");
    }

    ~IsoscelesTriangle()
    {
        return;
    }
}

public class EquilateralTriangle : IsoscelesTriangle
{
    public EquilateralTriangle() : base(GetRandomSide(), GetRandomSide())
    {
        SideC = SideA;
    }

    public override int Area()
    {
        double area = (Math.Sqrt(3) / 4) * SideA * SideA;
        return (int)Math.Round(area);
    }

    public override void DisplayProperties()
    {
        Console.WriteLine($"Равносторонний треугольник со стороной: {SideA}");
    }

    ~EquilateralTriangle()
    {
        return;
    }
}

public class RightTriangle : Triangle
{
    public RightTriangle()
    {
        do
        {
            SideA = GetRandomSide();
            SideB = GetRandomSide();
            SideC = (int)Math.Round(Math.Sqrt(SideA * SideA + SideB * SideB));
        } while (!IsValidTriangle(SideA, SideB, SideC));
    }

    public override int Area()
    {
        return (SideA * SideB) / 2;
    }

    public override void DisplayProperties()
    {
        Console.WriteLine($"Прямоугольный треугольник с катетами: {SideA}, {SideB} и гипотенузой: {SideC}");
    }

    ~RightTriangle()
    {
        return;
    }
}