﻿using Newtonsoft.Json;
using NGpt.Domain.Edit;

namespace NGpt.Services.Edit
{
    internal class EditService : BaseService
    {
        public override string Url { get; } = "https://api.openai.com/v1/edits";

        public EditService(string apiKey, string organization)
            : base(apiKey, organization)
        {
        }

        public EditResponse Edit(EditRequest request)
        {
            var requestDto = new EditRequestDto()
            {
                Model = GetModelName(request.Model),
                Input = request.Input,
                Instruction = request.Instruction,
                N = request.N,
                Temperature = request.Temperature,
                TopP = request.TopP
            };

            string responseBody = CallApi(requestDto);
            var responseDto = JsonConvert.DeserializeObject<EditResponseDto>(responseBody);

            var response = new EditResponse(
                Created : responseDto.Created,
                Choices : responseDto.Choices
                    .Select(x => new Choice(x.Text, x.Index)).ToList(),
                Usage : new Usage(responseDto.Usage.PromptTokens,
                    responseDto.Usage.CompletionTokens,
                    responseDto.Usage.TotalTokens)
            );

            return response;
        }
    }
}