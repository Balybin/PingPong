using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pong.Model
{
    public class MessageDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int User { get; set; }
        [Required]
        [MinLength(1)]
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
