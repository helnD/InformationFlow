namespace WebApplication.ViewEntities
{
    public class GraphInfo
    {
        public GraphInfo(int graphOrder, int[] inputElements, int[] outputElements)
        {
            GraphOrder = graphOrder;
            InputElements = inputElements;
            OutputElements = outputElements;
        }

        public int GraphOrder { get; }
        public int[] InputElements { get; }
        public int[] OutputElements { get; }
    }
}