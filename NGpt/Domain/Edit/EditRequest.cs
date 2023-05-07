using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NGpt.Domain.Edit
{
    public class EditRequest
    {
        public EditModel Model { get; set; }

        public string Input { get; set; } = string.Empty;

        [Required]
        public string Instruction { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value for 'n' should be greater than or equal to 1.")]
        public int? N { get; set; }

        [Range(0, 2, ErrorMessage = "Value for 'temperature' should be between 0 and 2.")]
        public double? Temperature { get; set; }

        [Range(0, 1, ErrorMessage = "Value for 'top_p' should be between 0 and 1.")]
        public double? TopP { get; set; }

        public EditRequest(EditModel model, string instruction)
        {
            Model = model;
            Instruction = instruction ?? throw new ArgumentNullException(nameof(instruction));
        }
    }
}
