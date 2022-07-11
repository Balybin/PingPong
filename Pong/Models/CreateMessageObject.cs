using System.ComponentModel.DataAnnotations;

namespace Pong.Model
{
    public class CreateMessageObject
    {
        [Required]
        public int User { get; set; }
        [Required]
        [MinLength(1)]
        public string Message { get; set; }
    }
}
