using NGpt.Domain.Edit;
using NGpt.Services.Edit;

namespace NGpt
{
    public class Edit
    {
        EditService _service;

        public Edit(string apiKey, string organization = "")
        {
            _service = new EditService(apiKey, organization);
        }

        public EditResponse EditContent(EditRequest request)
        {
            var response = _service.Edit(request);

            return response;
        }

        public string EditText(string text, string instruction)
        {
            var request = new EditRequest(EditModel.TextDavinciEdit001, instruction)
            {
                Input = text,
                Temperature = 0,
                N = 1,
                TopP = 1
            };

            var response = EditContent(request);
            var content = response.Choices[0].Text;

            return content;
        }

        public string EditCode(string code, string instruction)
        {
            var request = new EditRequest(EditModel.CodeDavinciEdit001, instruction)
            {
                Input = code,
                Temperature = 0,
                N = 1,
                TopP = 1
            };

            var response = EditContent(request);
            var content = response.Choices[0].Text;

            return content;
        }
    }
}