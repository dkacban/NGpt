using Flurl.Http;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System.ComponentModel;

namespace NGpt.Services
{
    internal abstract class BaseService
    {
        protected string _apiKey;
        protected readonly string _organization;
        public virtual string Url { get; }

        protected BaseService(string apiKey, string organization)
        {
            _apiKey = apiKey;
            _organization = organization;
        }

        protected async Task<T?> CallApi<T>(object requestDto)
        {
            AsyncRetryPolicy policy = CreateRetryPolicy();

            IFlurlResponse response = null;
            await policy.ExecuteAsync(async () =>
                {
                    response = await Url
                    .WithHeader("Content-Type", "application/json")
                    .WithHeader("Authorization", $"Bearer {_apiKey}")
                    .WithHeader("organization", _organization)
                    .PostJsonAsync(requestDto);
                }
            );

            if (response == null)
            {
                throw new Exception("Failed to send request to OpenAI API.");
            }

            string responseBody = await response.ResponseMessage.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<T>(responseBody);

            return responseDto;
        }

        protected AsyncRetryPolicy CreateRetryPolicy()
        {
            return Policy
                .Handle<FlurlHttpException>()
                .Or<AggregateException>(aggregateException =>
                    aggregateException.InnerExceptions.Any(innerException => innerException is FlurlHttpException))
                .Or<FlurlHttpException>()
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        protected string EnumToString<T>(T enumValue) where T : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return descriptionAttribute?.Description ?? enumValue.ToString().ToLowerInvariant();
        }
    }
}
