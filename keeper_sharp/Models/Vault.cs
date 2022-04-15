using System.ComponentModel.DataAnnotations;

namespace keeper_sharp.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        public string CreatorId { get; set; }
        public string Img { get; set; }
        public Account Creator { get; set; }
    }
}