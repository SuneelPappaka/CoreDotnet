namespace CoreDotnet.Services
{
    public class ScopedGuidService:IScopedGuidService
    {
        private readonly Guid _guid;
        public ScopedGuidService()
        {
            _guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
