using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace RestWrapper
{
    public static class RestSharpCall
    {
        private static IRestClient client;
        private static IRestRequest request;

        public static void Init(string url, RestSharpMethod method)
        {
            client = new RestClient(url);
            request = new RestRequest((Method)Enum.Parse(typeof(Method), method.ToString()));
        }

        public static string Make()
        {
            string response = Execute();
            return response;
        }

        public static string Make(IDictionary<string, string> parameters = null,
                                                IDictionary<string, string> urlSegments = null,
                                                IDictionary<string, string> headers = null,
                                                Object objectForUri = null,
                                                Object objectForBody = null,
                                                Object jsonObjectForBody = null,
                                                string token = null)
        {
            MakeRequest(parameters, urlSegments, headers, objectForUri, objectForBody, jsonObjectForBody, token);
            string response = Make();
            return response;
        }

        public static TResponse Make<TResponse>() where TResponse : class, new()
        {
            IRestResponse<TResponse> response = Execute<TResponse>();
            return response.Data;
        }

        public static TResponse Make<TResponse>(IDictionary<string, string> parameters = null,
                                                IDictionary<string, string> urlSegments = null,
                                                IDictionary<string, string> headers = null,
                                                Object objectForUri = null,
                                                Object objectForBody = null,
                                                Object jsonObjectForBody = null,
                                                string token = null)
                                                where TResponse : class, new()
        {
            MakeRequest(parameters, urlSegments, headers, objectForUri, objectForBody, jsonObjectForBody, token);
            TResponse response = Execute<TResponse>().Data;
            return response;
        }

        private static void MakeRequest(IDictionary<string, string> parameters,
                                                IDictionary<string, string> urlSegments,
                                                IDictionary<string, string> headers,
                                                Object objectForUri,
                                                Object objectForBody,
                                                Object jsonObjectForBody,
                                                string token)
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

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (objectForUri != null)
            {
                request.AddObject(objectForUri);
            }

            if (objectForBody != null)
            {
                request.AddBody(objectForBody);
            }

            if (jsonObjectForBody != null)
            {
                request.AddJsonBody(JsonConvert.SerializeObject(jsonObjectForBody));
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
