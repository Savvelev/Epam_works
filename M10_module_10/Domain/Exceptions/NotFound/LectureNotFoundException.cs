namespace Domain.Exceptions.NotFound
{
    public sealed class LectureNotFoundException : NotFoundException
    {
        public LectureNotFoundException(int Id)
             : base($"The lecture with the identifier {Id} was not found.")
        {
        }
        public LectureNotFoundException(string name)
             : base($"The lecture with the name {name} was not found.")
        {
        }
    }
}
