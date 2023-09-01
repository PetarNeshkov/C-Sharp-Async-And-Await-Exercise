namespace _02.BurgerPreparation;

public static class SyncCooker
{
    public static void Work()
    {
        HeatThePans();
        UnFreezeMeat();
        PeelPotatoes();
        CookBurgers();
        FryFries();
        PourDrinks();
        ServeAndEat();
    }

    private static void HeatThePans()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Pan heated!");
    }

    private static void UnFreezeMeat()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Meat Ready");
    }

    private static void PeelPotatoes()
    {
        Thread.Sleep(5000);
        Console.WriteLine("Potatoes peeled!");
    }

    private static void CookBurgers()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Burgers cooked!");
    }

    private static void FryFries()
    {
        Thread.Sleep(5000);
        Console.WriteLine("Fries fried!");
    }

    private static void PourDrinks()
    {
        Thread.Sleep(1000);
        Console.WriteLine("Drinks poured");
    }

    private static void ServeAndEat()
    {
        Thread.Sleep(5000);
        Console.WriteLine("Delicious");
    }
}