using System.ComponentModel.DataAnnotations;

namespace FignumCodingTest.Models
{
    public class Request
    {
        [Required]
        public string InputStringNumber { get; set; }

        public Request()
        {
            this.InputStringNumber = string.Empty;
        }
    }
}
