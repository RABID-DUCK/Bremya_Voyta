namespace Ekonomika.Work
{
    public interface IWork : IClickableObject
    {
        public string WorkName { get; }
        public string WorkerName { get; }
        public CharacterType WorkerType { get; }
        public Item ReceivedItem { get; }
    }
}
