namespace TestBed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int start = 10;
            int end = 3;
            List<int> list = new List<int>();

            for (int i = start; i >= end; i--)
                list.Add(i);

            int[] result = list.ToArray();
        }
    }
}