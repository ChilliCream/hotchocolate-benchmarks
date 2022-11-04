using System.ComponentModel.DataAnnotations;

namespace HotChocolate.Types.Benchmarks.Project.Data
{
    public class Track
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public ICollection<Session> Sessions { get; set; } = 
            new List<Session>();
    }
}