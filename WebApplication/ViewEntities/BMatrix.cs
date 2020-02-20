namespace WebApplication.ViewEntities
{
    public class BMatrix
    {
        public BMatrix(int[][] matrix, int[] sum)
        {
            Matrix = matrix;
            Sum = sum;
        }

        public int[][] Matrix { get; }
        public int[] Sum { get; }
    }
}