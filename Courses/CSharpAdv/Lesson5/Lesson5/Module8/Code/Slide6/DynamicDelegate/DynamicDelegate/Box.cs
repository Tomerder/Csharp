
namespace DynamicDelegate
{
    class Box
    {
        public string Name { get; set; }
        public decimal Volume { get; set; }

        public string Log()
        {
            return Name + " contains " + Volume;
        }
    }
}
