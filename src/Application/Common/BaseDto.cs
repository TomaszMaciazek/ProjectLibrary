using System.ComponentModel.DataAnnotations;

namespace Application.Common
{
    public abstract class BaseDto
    {
        [Required]
        public int Id { get; set; }
    }
}
