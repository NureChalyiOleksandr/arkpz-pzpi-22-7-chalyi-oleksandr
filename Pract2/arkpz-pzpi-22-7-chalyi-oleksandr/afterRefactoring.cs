// Після рефакторингу
string input = Console.ReadLine();
if (int.TryParse(input, out int value))
{
    Console.WriteLine($"Valid number: {value}");
}
else
{
    Console.WriteLine("Invalid number format");
}


// Після рефакторингу
class Circle
{
    public double Radius { get; set; }

    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}




// Після рефакторингу
class Animal
{
    public void Walk()
    {
        // Логіка для ходьби
    }
}
class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine("Barking");
    }
}
class Cat : Animal
{
    public void Meow()
    {
        Console.WriteLine("Meowing");
    }
}


