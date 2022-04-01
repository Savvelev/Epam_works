namespace Domain.Exceptions.NotFound
{
    public sealed class LectorNotFoundException : NotFoundException
    {
        public LectorNotFoundException(int Id)
            : base($"The lector with the identifier {Id} was not found.")
        {
        }
    }
}
