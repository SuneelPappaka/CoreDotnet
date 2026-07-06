namespace CoreDotnet.Services
{
    public class SingletonGuidService:ISingletonGuidService
    {
        private readonly Guid _guid;
        public SingletonGuidService()
        {
            _guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _guid.ToString(); 
        }
    }
}
