//-----------------------------------------------------------------------
// <copyright file="RestSharpCall.cs" company="XXXXXXX">
// Copyright (c) XXXXXXX. All rights reserved
// </copyright>
//-----------------------------------------------------------------------
namespace RestWrapper
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using RestSharp;

    /// <summary>
    /// Static class having methods to make rest calls
    /// </summary>
    public static class RestSharpCall
    {
        /// <summary>
        /// REST client
        /// </summary>
        private static IRestClient client;

        /// <summary>
        /// REST request
        /// </summary>
        private static IRestRequest request;

        /// <summary>
        /// Method to initialize REST with url and verb (GET, POST and Etc.,.)
        /// </summary>
        /// <param name="url">REST path</param>
        /// <param name="method">Http verb</param>
        public static void Init(string url, RestSharpMethod method)
        {
            client = new RestClient(url);
            request = new RestRequest((Method)Enum.Parse(typeof(Method), method.ToString()));
        }

        /// <summary>
        /// Method to make REST call for string response
        /// </summary>
        /// <returns>String response</returns>
        public static string Make()
        {
            string response = Execute();
            return response;
        }

        /// <summary>
        /// Method to make Rest call for string response
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <param name="urlSegments">Url segments</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="objectForUri">Object for uri</param>
        /// <param name="objectForBody">Object for body</param>
        /// <param name="objectForJsonBody">Object for JSON body</param>
        /// <returns>String response</returns>
        public static string Make(
                             IDictionary<string, string> parameters = null,
                             IDictionary<string, string> urlSegments = null,
                             IDictionary<string, string> headers = null,
                             object objectForUri = null,
                             object objectForBody = null,
                             object objectForJsonBody = null)
        {
            CreateRequest(parameters, urlSegments, headers, objectForUri, objectForBody, objectForJsonBody);
            string response = Make();
            return response;
        }

        /// <summary>
        /// Method to make REST call for object response
        /// </summary>
        /// <typeparam name="TResponse">Response object type</typeparam>
        /// <returns>Object response</returns>
        public static TResponse Make<TResponse>() where TResponse : class, new()
        {
            IRestResponse<TResponse> response = Execute<TResponse>();
            return response.Data;
        }

        /// <summary>
        /// Method to make Rest call for object response
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <param name="urlSegments">Url segments</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="objectForUri">Object for uri</param>
        /// <param name="objectForBody">Object for body</param>
        /// <param name="objectForJsonBody">Object for JSON body</param>
        /// <typeparam name="TResponse">Response object type</typeparam>
        /// <returns>Object response</returns>
        public static TResponse Make<TResponse>(
                                IDictionary<string, string> parameters = null,
                                IDictionary<string, string> urlSegments = null, 
                                IDictionary<string, string> headers = null,
                                object objectForUri = null,
                                object objectForBody = null,
                                object objectForJsonBody = null)
                                where TResponse : class, new()
        {
            CreateRequest(parameters, urlSegments, headers, objectForUri, objectForBody, objectForJsonBody);
            TResponse response = Execute<TResponse>().Data;
            return response;
        }

        /// <summary>
        /// Helper method to create request
        /// </summary>
        /// <param name="parameters">Query string parameters</param>
        /// <param name="urlSegments">Url segments</param>
        /// <param name="headers">Headers for the request</param>
        /// <param name="objectForUri">Object for uri</param>
        /// <param name="objectForBody">Object for body</param>
        /// <param name="objectForJsonBody">Object for JSON body</param>
        private static void CreateRequest(
                            IDictionary<string, string> parameters,
                            IDictionary<string, string> urlSegments,
                            IDictionary<string, string> headers,
                            object objectForUri,
                            object objectForBody,
                            object objectForJsonBody)
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

            if (objectForJsonBody != null)
            {
                request.AddJsonBody(JsonConvert.SerializeObject(objectForJsonBody));
            }
        }

        /// <summary>
        /// Helper method to execute request
        /// </summary>
        /// <typeparam name="TResponse">Response object type</typeparam>
        /// <returns>Object response</returns>
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

        /// <summary>
        /// Helper method to execute request
        /// </summary>
        /// <returns>String response</returns>
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
