using System.ComponentModel.DataAnnotations;

namespace Buoi4.Models
{
    public class User1
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}