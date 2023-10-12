public class YourComplexModel
{
    public string? Name { get; set; }
    public int Age { get; set; }

    // Examples of methods for performing operations on models
    public string GetGreeting()
    {
        return $"Hello, my name is {Name} and I am {Age} years old.";
    }
}
