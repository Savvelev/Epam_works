namespace DataAccess.DbEntities
{
    public record AttendanceDb
    {
        public int Id { get; set; }
        public StudentDb Student { get; set; }
        public LectureDb Lecture { get; set; }
        public HomeworkDb Homework { get; set; }
        public bool IsStudentAttended { get; set; }


    }
}
