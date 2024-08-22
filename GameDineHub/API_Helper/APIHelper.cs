using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace RestSharp.API_Helper
{
    public class APIHelper
    {
        #region GenricMethodsForConsumingAPI

        #region GetMethods

        /// <summary>
        /// Gets RestRequest object for Get Request with single perameter.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="perameterName">The perameterName.</param>
        /// <param name="perameterValue">The perameterValue.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequest(string resource, string perameterName, object perameterValue)
        {
            /* api request object */
            var request = new RestRequest(resource) { RequestFormat = DataFormat.Json };
            request.AddQueryParameter(perameterName, perameterValue.ToString());
            return request;
        }
        /// <summary>
        /// Gets RestRequest object for Get Request with multiple perameter.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="perameter Names array">The perameter Name Array.</param>
        /// <param name="perameterValue">The perameterValue.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequest(string resource, string[] perameterNames, object perameterValue)
        {
            /* api request object */
            var request = new RestRequest(resource) { RequestFormat = DataFormat.Json };
            request.AddObject(perameterValue, perameterNames);
            return request;
        }

        /// <summary>
        /// Gets RestRequest object for Get Request with list of perameters.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="perametersList">The perametersList which will be a dictionary having perameter name as key and perameter value as value.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequest(string resource, Dictionary<string, string> perametersList)
        {
            /* api request object */
            var apiRequest = new RestRequest(resource, Method.GET);
            apiRequest.AddHeader("Accept", "application/json");
            apiRequest.Parameters.Clear();
            foreach (var perameter in perametersList)
            {
                apiRequest.AddQueryParameter(perameter.Key, perameter.Value);
            }
            return apiRequest;
        }

        /// <summary>
        /// Gets RestRequest object for Get Request with list of string with single key.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequest(string resource, string perameterName, List<string> listOfString)
        {
            /* api request object */
            var apiRequest = new RestRequest(resource) { RequestFormat = DataFormat.Json };
            foreach (var item in listOfString)
            {
                apiRequest.AddQueryParameter(perameterName, item.ToString());
            }
            return apiRequest;
        }

        /// <summary>
        /// Gets RestRequest object for Get Request with list of string with single key.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequest(string resource, string perameterName, List<int> listOfInteger)
        {
            /* api request object */
            var apiRequest = new RestRequest(resource) { RequestFormat = DataFormat.Json };
            foreach (var item in listOfInteger)
            {
                apiRequest.AddQueryParameter(perameterName, item.ToString());
            }
            return apiRequest;
        }

        /// <summary>
        /// Gets RestRequest object for Get Request without perameters.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequest(string resource)
        {
            /* api request object */
            var apiRequest = new RestRequest(resource) { RequestFormat = DataFormat.Json };
            return apiRequest;
        }

        /// <summary>
        /// Gets RestRequest object for Get Request with list of object with single parameter name.
        /// This method use AddParameter 
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetGetRequestWithAddParameter(string resource, string perameterName, List<object> listOfObjects)
        {
            /* api request object */
            var apiRequest = new RestRequest(resource) { RequestFormat = DataFormat.Json };
            foreach (var item in listOfObjects)
            {
                apiRequest.AddParameter(perameterName, item, ParameterType.GetOrPost);
            }
            return apiRequest;
        }

        #endregion

        #region PostMethods
        /* This Method was required because HubAPI is not using the Rest Standards for APIEndpoints  */
        /// <summary>
        /// Gets RestRequest object for Post Request with single perameter.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="perameterName">The perameterName.</param>
        /// <param name="parameterValue">The perameterValue.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetPostRequest(string resource, string perameterName, string parameterValue)
        {
            var apiRequest = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json };
            apiRequest.AddQueryParameter(perameterName, parameterValue);
            return apiRequest;
        }
        /* This Method was required because HubAPI is not using the Rest Standards for APIEndpoints  */
        /// <summary>
        /// Gets RestRequest object for Post Request with single perameter.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="perameterName">The perameterName.</param>
        /// <param name="perameterValue">The perameterValue.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetPostRequest(string resource, Dictionary<string, string> requestParameters)
        {
            var apiRequest = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json };
            foreach (var item in requestParameters)
            {
                apiRequest.AddQueryParameter(item.Key, item.Value);
            }
            return apiRequest;
        }

        /// <summary>
        /// Gets RestRequest object for Post Request with complex object.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="requestBody">The requestBody which will be a complex object.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetPostRequest(string resource, object requestBody)
        {
            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
            var request = new RestRequest(resource, Method.POST);
            request.AddParameter(new Parameter("application/json", jsonRequestBody, ParameterType.RequestBody));
            return request;
        }


        /// <summary>
        /// Gets RestRequest object for Put Request with complex object.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="requestBody">The requestBody which will be a complex object.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetPutRequest(string resource, object requestBody)
        {
            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
            var request = new RestRequest(resource, Method.PUT) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(requestBody);
            return request;
        }

        /// <summary>
        /// Gets RestRequest object for Post Request with complex object.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="parameterList">The perametersList which will be a dictionary having perameter name as key and perameter value as value.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetPostRequest(string resource, Dictionary<string, object> parameterList)
        {
            var apiRequest = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json };
            foreach (var parameter in parameterList)
            {
                apiRequest.AddParameter(parameter.Key, parameter.Value);
            }
            return apiRequest;
        }

        #endregion

        #region DeleteMethods
        /// <summary>
        /// Delete RestRequest object for delete Request with multiple perameter.
        /// </summary>
        /// <param name="resource">The resource which is Action method you want to consume.</param>
        /// <param name="perameter Names array">The perameter Name Array.</param>
        /// <param name="perameterValue">The perameterValue.</param>
        /// <returns>RestRequest.</returns>
        protected RestRequest GetDeleteRequest(string resource, string[] perameterNames, object perameterValue)
        {
            /* api request object */
            var request = new RestRequest(resource, Method.DELETE) { RequestFormat = DataFormat.Json };
            request.AddObject(perameterValue, perameterNames);
            return request;
        }
        #endregion

        /// <summary>
        /// Process Anykind of any API Request and return it's reponse after Deserializing it.
        /// </summary>
        /// <param name="apiRequest">The apiRequest.</param>
        /// <returns>RestRequest.</returns>
        protected T ProcessRequest<T>(RestClient _client, RestRequest apiRequest, bool isAPIResponseRequireInAnyCase = false)
        {
            /* execute the api request */
            var response = _client.Execute(apiRequest);
            // If API response status code is OK deserialize response object to desired entity
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                // Return default object for specified entity If API response status code is not OK
                return isAPIResponseRequireInAnyCase ? JsonConvert.DeserializeObject<T>(response.Content) : default(T);
            }
        }

        /// <summary>
        /// Process Anykind of any API Request and return it's reponse after Deserializing it.
        /// </summary>
        /// <param name="apiRequest">The apiRequest.</param>
        /// <returns>RestRequest.</returns>
        protected IRestResponse ProcessRequest(RestClient _client, RestRequest apiRequest)
        {
            /* execute the api request */
            return _client.Execute(apiRequest);
        }

        #endregion
    }
}