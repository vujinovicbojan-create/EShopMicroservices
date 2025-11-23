namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entityType, object key) : base($"Entity \"{entityType}\" with key ({key}) was not found")
        {
        }
    }
}