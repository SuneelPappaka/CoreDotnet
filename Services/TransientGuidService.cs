namespace CoreDotnet.Services
{
    public class TransientGuidService : ITransientGuidService
    {
        private readonly Guid  _guid;
        public TransientGuidService()
        {
            _guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return _guid.ToString();
        }
    }
}
