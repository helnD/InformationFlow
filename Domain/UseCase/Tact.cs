namespace Domain.UseCase
{
    public class Tact
    {
        public Tact(int node, int tact)
        {
            Node = node;
            Value = tact;
        }

        public int Node { get; }
        public int Value { get; }
    }
}