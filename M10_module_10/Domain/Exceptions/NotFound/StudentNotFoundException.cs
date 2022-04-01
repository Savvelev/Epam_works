namespace Domain.Exceptions.NotFound
{
    public sealed class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(int Id)
             : base($"The student with the identifier {Id} was not found.")
        {
        }
        public StudentNotFoundException(string firstName, string lastName)
             : base($"The student with name: {firstName} {lastName} was not found.")
        {
        }
    }
}
