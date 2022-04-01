using System.Collections.Generic;

namespace Domain.Entities
{

    public record Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public Lector Lector { get; set; }
    }
}

