using System.Collections.Generic;

namespace DataAccess.DbEntities
{
    public class StudentDb
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<AttendanceDb> Attendances { get; set; } = new List<AttendanceDb>();
    }
}
