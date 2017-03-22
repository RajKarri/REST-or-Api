using System;
using System.Collections.Generic;
using RestSharp;

namespace RestWrapper
{
    public static class RestSharpCall
    {
        private static IRestClient client;
        private static IRestRequest request;

        public static void Init(string url, Method method)
        {
            client = new RestClient(url);
            request = new RestRequest(method);
        }

        public static string Make()
        {
            string response = Execute();
            return response;
        }

        public static string Make(IDictionary<string, string> parameters = null, IDictionary<string, string> urlSegments = null,
                                                IDictionary<string, object> models = null, string header = null, string jsonBody = null, string token = null)
        {
            MakeRequest(parameters, urlSegments, models, jsonBody, header, token);
            string response = Make();
            return response;
        }

        public static TResponse Make<TResponse>() where TResponse : class, new()
        {
            IRestResponse<TResponse> response = Execute<TResponse>();
            return response.Data;
        }

        public static TResponse Make<TResponse>(IDictionary<string, string> parameters = null, IDictionary<string, string> urlSegments = null,
                                                IDictionary<string, object> models = null, string header = null, string jsonBody = null, string token = null)
                                                where TResponse : class, new()
        {
            MakeRequest(parameters, urlSegments, models, jsonBody, header, token);
            TResponse response = Execute<TResponse>().Data;
            return response;
        }

        private static void MakeRequest(IDictionary<string, string> parameters, IDictionary<string, string> urlSegments,
                                        IDictionary<string, object> models, string jsonBody, string header, string token)
        {
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            if (urlSegments != null)
            {
                foreach (KeyValuePair<string, string> segment in urlSegments)
                {
                    request.AddUrlSegment(segment.Key, segment.Value);
                }
            }

            if (models != null)
            {
                foreach (KeyValuePair<string, object> model in models)
                {
                    var json = request.JsonSerializer.Serialize(model);
                    request.AddParameter(model.Key, json);
                }
            }

            if (!string.IsNullOrEmpty(jsonBody))
            {
                request.AddJsonBody(jsonBody);
            }

            if (!string.IsNullOrEmpty(header))
            {
                request.AddHeader("header", header);
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", token);
            }
        }

        private static IRestResponse<TResponse> Execute<TResponse>() where TResponse : class, new()
        {
            try
            {
                if (client == null || request == null)
                {
                    throw new Exception("Initialize method call must happen before this call");
                }

                IRestResponse<TResponse> response = client.Execute<TResponse>(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Bad request", ex);
            }
        }

        private static string Execute()
        {
            try
            {
                if (client == null || request == null)
                {
                    throw new Exception("Initialize method call must happen before this call");
                }

                IRestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {
                throw new Exception("Bad request", ex);
            }
        }
    }
}
