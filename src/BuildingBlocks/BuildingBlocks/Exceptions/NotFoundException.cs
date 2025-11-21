namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entityType, Guid id) : base($"Entity \"{entityType}\" with key ({id}) was not found")
        {
        }
    }
}