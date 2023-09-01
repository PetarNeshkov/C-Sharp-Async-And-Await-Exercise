namespace _02.BurgerPreparation;

public static class AllAtOnceCooker
{
    public static async Task Work()
    {
        var tasks = new List<Task>
        {
            HeatThePans(),
            UnfreezeMeat(),
            PeelPotatoes(),
            CookBurgers(),
            FryFries(),
            PourDrinks(),
            ServeAndEat()
        };

        await Task.WhenAll(tasks);
    }

    private static async Task HeatThePans()
    {
        await Task.Delay(2000);
        Console.WriteLine("Pan heated!");
    }

    private static async Task UnfreezeMeat()
    {
        await Task.Delay(3000);
        Console.WriteLine("Meat ready!");
    }

    private static async Task PeelPotatoes()
    {
        await Task.Delay(2000);
        Console.WriteLine("Potatoes peeled!");
    }

    private static async Task CookBurgers()
    {
        await Task.Delay(5000);
        Console.WriteLine("Burgers cooked!");
    }

    private static async Task FryFries()
    {
        await Task.Delay(3000);
        Console.WriteLine("Fries fried!");
    }

    private static async Task PourDrinks()
    {
        await Task.Delay(1000);
        Console.WriteLine("Drinks poured!");
    }

    private static async Task ServeAndEat()
    {
        await Task.Delay(5000);
        Console.WriteLine("Delicious!");
    }
}