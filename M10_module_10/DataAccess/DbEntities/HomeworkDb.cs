using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities
{
    public record HomeworkDb
    {
        [Key]
        [ForeignKey("Attendance")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseMark { get; set; }
        public ICollection<AttendanceDb> Attendances { get; set; } = new List<AttendanceDb>();


    }
}
