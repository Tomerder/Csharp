
namespace DynamicEvents
{
    class Box
    {
        public string Name { get; set; }
        public decimal Volume { get; set; }

        public void Log()
        {
            System.Console.WriteLine(Name + " contains " + Volume);
        }
    }
}
