namespace Todos
{
    public class Todo :
        IIsComplete
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public bool IsComplete { get; set; } = false;
    }
}