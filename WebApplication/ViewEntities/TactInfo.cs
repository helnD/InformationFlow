namespace WebApplication.ViewEntities
{
    public class TactInfo
    {
        public TactInfo(int element, int tactOfCreation, int tactOfDestroy, int tactOfStore)
        {
            Element = element;
            TactOfCreation = tactOfCreation;
            TactOfDestroy = tactOfDestroy;
            TactOfStore = tactOfStore;
        }

        public int Element { get; }
        public int TactOfCreation { get; }
        public int TactOfDestroy { get; }
        public int TactOfStore { get; }
    }
}