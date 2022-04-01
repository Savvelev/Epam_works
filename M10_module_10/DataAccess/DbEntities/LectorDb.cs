using System.Collections.Generic;

namespace DataAccess.DbEntities
{
    public class LectorDb
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<LectureDb> Lectures { get; set; } = new List<LectureDb>();

    }
}
