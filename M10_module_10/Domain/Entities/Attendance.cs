namespace Domain.Entities
{
    public record Attendance
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Lecture Lecture { get; set; }
        public Homework Homework { get; set; }
        public bool IsStudentAttended { get; set; }

    }
}
