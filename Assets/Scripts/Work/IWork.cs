namespace Ekonomika.Work
{
    public interface IWork : IClickableObject
    {
        public string WorkName { get; }
        public Item ReceivedItem { get; }
    }
}
