﻿using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CAS.UI.Helpers
{
	public static class APIExtensions
	{
		public static ApiResult<long> GetSequenceAsync(this HttpClient client, string requestUri)
		{
			var res = client.GetAsync(requestUri).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			return new ApiResult<long>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode ? long.Parse(content) : -666,
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static ApiResult<TResult> GetJsonAsync<TResult>(this HttpClient client, string requestUri)
		{
			var res =  client.GetAsync(requestUri).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			return new ApiResult<TResult>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode ? JsonConvert.DeserializeObject<TResult>(content) : default(TResult),
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static ApiResult<TResult> SendJsonAsync<TModel, TResult>(this HttpClient client, HttpMethod httpMethod, string requestUri, TModel model)
		{
			var json = "[{}]";
			if(model != null)
				json = JsonConvert.SerializeObject(model);
			var message = new HttpRequestMessage(httpMethod, requestUri)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};


			var res = client.SendAsync(message, HttpCompletionOption.ResponseContentRead).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			return new ApiResult<TResult>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode
					? (string.IsNullOrWhiteSpace(content) ? default(TResult) : JsonConvert.DeserializeObject<TResult>(content, new JsonSerializerSettings()))
					: default(TResult),
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static ApiResult SendJsonAsync<TModel>(this HttpClient client, HttpMethod httpMethod, string requestUri, TModel model)
		{
			var json = "[{}]";
			if (model != null)
				json = JsonConvert.SerializeObject(model);
			var message = new HttpRequestMessage(httpMethod, requestUri)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
			var res = client.SendAsync(message).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			return new ApiResult
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static ApiResult SendJsonAsync(this HttpClient client, HttpMethod httpMethod, string requestUri)
		{
			var json = "[{}]";
			var message = new HttpRequestMessage(httpMethod, requestUri)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
			var res = client.SendAsync(message).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			return new ApiResult
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}


		public static ApiResult<TResult> GetXMLAsync<TResult>(this HttpClient client, string requestUri)
		{
			var res = client.GetAsync(requestUri).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			object data = null;
			if (!string.IsNullOrWhiteSpace(content))
			{
				var serializer = new XmlSerializer(typeof(TResult));
				using (TextReader reader = new StringReader(content))
					data = serializer.Deserialize(reader);
			}
			

			return new ApiResult<TResult>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode ? (TResult)data : default(TResult),
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static ApiResult<TResult> SendXMLAsync<TModel, TResult>(this HttpClient client, HttpMethod httpMethod, string requestUri, TModel model)
		{
			var json = JsonConvert.SerializeObject(model);
			var message = new HttpRequestMessage(httpMethod, requestUri)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
			var res = client.SendAsync(message).GetAwaiter().GetResult();
			var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

			object data = null;
			if (!string.IsNullOrWhiteSpace(content))
			{
				var serializer = new XmlSerializer(typeof(TResult));
				using (TextReader reader = new StringReader(content))
					data = serializer.Deserialize(reader);
			}

			return new ApiResult<TResult>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode
					? (string.IsNullOrWhiteSpace(content) ? default(TResult) : (TResult)data)
					: default(TResult),
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

	}


	public class ApiResult<TView> : ApiResult
	{
		/// <summary>
		/// Данные
		/// </summary>
		public TView Data { get; set; }
	}

	public class ApiResult
	{
		/// <summary>
		/// Флаг успешности
		/// </summary>
		public bool IsSuccessful { get; set; }

		/// <summary>
		/// Код ошибки
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Сообщение об ошибке
		/// </summary>
		public string Error { get; set; }
	}

	public class QueryParams
	{
		public string Query { get; set; }
		public string SqlParams { get; set; }
	}
}