namespace Domain.Exceptions.NotFound
{
    public sealed class AttendanceNotFoundException : NotFoundException
    {
        public AttendanceNotFoundException(int Id)
            : base($"The Attendance with the identifier {Id} was not found.")
        {
        }
    }
}
