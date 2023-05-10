using Flurl.Http;
using Newtonsoft.Json;
using Polly;
using System.ComponentModel;

namespace NGpt.Services
{
    internal abstract class BaseService
    {
        private string _apiKey;
        private readonly string _organization;
        public virtual string Url { get; }

        protected BaseService(string apiKey, string organization)
        {
            _apiKey = apiKey;
            _organization = organization;
        }

        protected T CallApi<T>(object requestDto)
        {
            //var response = Url
            //    .WithHeader("Content-Type", "application/json")
            //    .WithHeader("Authorization", $"Bearer {_apiKey}")
            //    .WithHeader("organization", _organization)
            //    .PostJsonAsync(requestDto).Result;

            //return "";

            var policy = Policy
                .Handle<AggregateException>(aggregateException =>
                    aggregateException.InnerExceptions.Any(innerException => innerException is FlurlHttpException))
                .Or<FlurlHttpException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));

            IFlurlResponse response = null;
            policy.Execute(() =>
            {
                response = Url
                    .WithHeader("Content-Type", "application/json")
                    .WithHeader("Authorization", $"Bearer {_apiKey}")
                    .WithHeader("organization", _organization)
                    .PostJsonAsync(requestDto).Result;
            });

            if (response == null)
            {
                throw new Exception("Failed to send request to OpenAI API.");
            }

            string responseBody = response.ResponseMessage.Content.ReadAsStringAsync().Result;

            var responseDto = JsonConvert.DeserializeObject<T>(responseBody);

            return responseDto;
        }

        protected string EnumToString<T>(T enumValue) where T : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return descriptionAttribute?.Description ?? enumValue.ToString().ToLowerInvariant();
        }
    }
}
