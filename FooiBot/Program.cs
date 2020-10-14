namespace FooiBot
{
    class Program
    {
        static void Main()
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
