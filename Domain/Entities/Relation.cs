namespace Domain.Entities
{
    public class Relation
    {
        public Relation(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int Start { get; }
        public int End { get; }
    }
}