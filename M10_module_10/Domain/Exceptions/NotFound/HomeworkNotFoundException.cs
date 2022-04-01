namespace Domain.Exceptions.NotFound
{
    public sealed class HomeworkNotFoundException : NotFoundException
    {
        public HomeworkNotFoundException(int homeworkId)
            : base($"The homework with the identifier {homeworkId} was not found.")
        {
        }
    }
}
