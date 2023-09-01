namespace _02.BurgerPreparation;

public class AsyncCooker
{
    public static async Task Work()
    {
        await Task.WhenAll(
            HeatThePans(),
            UnfreezeMeat(),
            PeelPotatoes());

        await Task.WhenAll(
            CookBurgers(),
            FryFries(),
            PourDrinks());

        await ServeAndEat();
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