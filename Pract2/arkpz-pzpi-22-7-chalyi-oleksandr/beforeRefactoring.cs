// До рефакторингу
string input = Console.ReadLine();
try
{
    int value = int.Parse(input);
    Console.WriteLine($"Valid number: {value}");
}
catch (FormatException)
{
    Console.WriteLine("Invalid number format");
}

// До рефакторингу
class Circle
{
    public double Radius { get; set; }

    public double CalculateArea(double pi)
    {
        return pi * Radius * Radius;
    }
}




// До рефакторингу
class Animal
{
    public void Walk()
    {
        // Логіка для ходьби
    }

    public void Bark()
    {
        Console.WriteLine("Barking");
    }

    public void Meow()
    {
        Console.WriteLine("Meowing");
    }
}


