using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public string LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public IList<Student> Students { get; set; }
    }
}
