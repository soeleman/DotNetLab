namespace AutoIniProperty
{
    internal class One
    {
        private int _count = 0;

        public One() 
        {
            Console.WriteLine("One");
        }

        public int Count()
        {
            return _count++;
        }
    }
}
