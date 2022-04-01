using System.Collections.Generic;

namespace DataAccess.DbEntities
{
    public class LectureDb
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AttendanceDb> Attendances { get; set; } = new List<AttendanceDb>();
        public LectorDb Lector { get; set; }
    }
}
