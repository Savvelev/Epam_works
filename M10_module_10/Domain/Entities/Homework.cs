using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Homework
    {
        [Key]
        [ForeignKey("Attendance")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseMark { get; set; }
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    }
}

