using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using mockingbird.Models;
using Newtonsoft.Json;

namespace mockingbird.Handlers
{
    public class MockRequestsInterceptor : DelegatingHandler
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// Will read from database if incoming payload matches a record.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await GetMockEndpoint(request, cancellationToken);
        }

        private async Task<HttpResponseMessage> GetMockEndpoint(HttpRequestMessage incomingRequestMessage, CancellationToken cancellationToken)
        {
            //Lets get the url, verb and other information for this new request. 
            //We will look up for this information in database if a endpoint exists for mocking.
            var incomingRequestData = await HydrateApiLogEntryAsync(incomingRequestMessage);

            //Picking only endpoints.
            //If a endpoint is started or not.
            //if request payload matched that of mock data saved in database.
            var mockEndpointApiData = _db.MockApiHttpDatas.FirstOrDefault(x => x.ApiStatus == ApiStatus.Started
                && x.Verb.ToString() == incomingRequestData.RequestMethod.ToString()
                && x.Path == incomingRequestData.RequestUriPathAndQuery);
            if (mockEndpointApiData == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            //Create response for sending back mocked data.
            var outputResponse = new HttpResponseMessage
            {
                StatusCode = mockEndpointApiData.ResponseStatus,//Whatever status is from db.
                Content = new StringContent(mockEndpointApiData.ResponseBody) //Whatever data from db.
            };

            //Setting response content type as per the user specification from db column.
            outputResponse.Content.Headers.ContentType = new MediaTypeHeaderValue(mockEndpointApiData.ContentType);

            int responseDelay;
            if (!int.TryParse(mockEndpointApiData.ResponseDelay, out responseDelay))
            {
                return outputResponse;
            }

            //Delay sending back to client if a delayed value is set.
            var t = Task.Delay(TimeSpan.FromSeconds(responseDelay), cancellationToken);
            t.Wait(cancellationToken);

            return outputResponse;
        }

        private async Task<ApiLogEntry> HydrateApiLogEntryAsync(HttpRequestMessage request)
        {
            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);
            var routeData = request.GetRouteData();
            var apiLogEntry = new ApiLogEntry
            {
                CorrelationId = request.GetCorrelationId().ToString(),
                Application = "mockingbird",
                User = context.User.Identity.Name,
                Machine = Environment.MachineName,
                RequestContentType = context.Request.ContentType,
                RequestRouteTemplate = routeData.Route.RouteTemplate,
                RequestRouteData = SerializeRouteData(routeData),
                RequestIpAddress = context.Request.UserHostAddress,
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.Now,
                RequestUriPathAndQuery = request.RequestUri.PathAndQuery
            };
            return await Task.FromResult(apiLogEntry);
        }

        private static string SerializeRouteData(IHttpRouteData routeData)
        {
            return JsonConvert.SerializeObject(routeData, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private static string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();
            foreach (var item in headers.ToList())
            {
                if (item.Value == null)
                {
                    continue;
                }
                string header = item.Value.Aggregate(string.Empty, (current, value) => current + (value + " "));
                // Trim the trailing space and add item to the dictionary
                header = header.TrimEnd(" ".ToCharArray());
                dict.Add(item.Key, header);
            }
            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }

    }
}