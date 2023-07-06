using NGpt.Domain.Edit;
using NGpt.Services.Edit;

namespace NGpt
{
    public class Edit
    {
        EditService _service;

        public Edit(string apiKey = "", string organization = "")
        {
            _service = new EditService(apiKey, organization);
        }

        public async Task<EditResponse> EditContentAsync(EditRequest request)
        {
            var response = await _service.Edit(request);

            return response;
        }

        public async Task<string> EditTextAsync(string text, string instruction)
        {
            var request = new EditRequest(EditModel.TextDavinciEdit001, instruction)
            {
                Input = text,
                Temperature = 0,
                N = 1,
                TopP = 1
            };

            var response = await EditContentAsync(request);
            var content = response.Choices[0].Text;

            return content;
        }

        public async Task<string> EditCodeAsync(string code, string instruction)
        {
            var request = new EditRequest(EditModel.CodeDavinciEdit001, instruction)
            {
                Input = code,
                Temperature = 0,
                N = 1,
                TopP = 1
            };

            var response = await EditContentAsync(request);
            var content = response.Choices[0].Text;

            return content;
        }
    }
}