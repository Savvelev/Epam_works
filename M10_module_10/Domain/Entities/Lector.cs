using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace Domain.Entities
{
    public record Lector
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
