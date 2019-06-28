﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using EFCore.Contract;
using EFCore.DTO;
using EFCore.DTO.General;
using EFCore.Filter;
using SmartCore.Management;
using SmartCore.Queries;

namespace CAS.UI.Helpers
{
	public class ApiProvider
	{
		private HttpClient _httpClient;

		public ApiProvider(string serverName)
		{
			_httpClient = new HttpClient() {BaseAddress = new Uri(serverName)};
		}

		#region User

		public void GetAPIConnection()
		{
			try
			{
				var res = _httpClient.GetAsync("/monitoring/ping").Result;
				if (res.StatusCode != HttpStatusCode.OK)
					throw new Exception($"API was not found on server");
			}
			catch
			{
				throw new Exception($"API was not found on server");
			}
		}

		public UserDTO GetUserAsync(string login, string password)
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["login"] = login,
				["password"] = password,
			});
			var res = _httpClient.GetJsonAsync<UserDTO>($"user/get?{param}");
			return res?.Data;
		}


		public  List<UserDTO> GetAllUsersAsync()
		{
			var res =  _httpClient.GetJsonAsync<List<UserDTO>>($"user/getall");
			return res?.Data;
		}

		#endregion

		#region Executor

		public DataSet Execute(string sql)
		{
			var res = _httpClient.SendXMLAsync<QueryParams, DataSet>(HttpMethod.Post, "executor/query", new QueryParams(){Query = sql});
			return res?.Data ?? new DataSet() { Tables = { new DataTable() } }; ;
		}

		public DataSet Execute(string query, SqlParameter[] parameters)
		{
			var res = _httpClient.SendXMLAsync<QueryParams, DataSet>(HttpMethod.Post, "executor/queryparams", new QueryParams() { Query = query, SqlParams = parameters});
			return res?.Data ?? new DataSet(){Tables = { new DataTable()}};
		}

		#endregion

		public IList<int> GetSelectColumnOnly<T>(IEnumerable<Filter> filters, string selectProperty) where T: BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["selectProperty"] = selectProperty,
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, List<int>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO","").ToLower()}/getcolumn?{param}", filters);
			return res?.Data;
		}

		public T GetObjectById<T>(int id, bool loadChild = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["id"] = id.ToString(),
				["loadChild"] = loadChild.ToString()
			});
			var res = _httpClient.GetJsonAsync<T>($"{typeof(T).Name.Replace("DTO", "").ToLower()}/getbyid?{param}");
			return res?.Data;
		}

		public T GetObject<T>(IEnumerable<Filter> filters = null, bool loadChild = false, bool getDeleted = false,
			bool getAll = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = loadChild.ToString(),
				["getDeleted"] = getDeleted.ToString(),
				["getAll"] = getAll.ToString(),
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, T>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/get?{param}", filters);
			return res?.Data;
		}

		public List<T> GetObjectList<T>(IEnumerable<Filter> filters = null, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = loadChild.ToString(),
				["getDeleted"] = getDeleted.ToString(),
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, List<T>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/getlist?{param}", filters);
			return res?.Data;
		}

		public List<T> GetObjectListAll<T>(IEnumerable<Filter> filters = null, bool loadChild = false,
			bool getDeleted = false) where T : BaseEntity
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = loadChild.ToString(),
				["getDeleted"] = getDeleted.ToString(),
			});
			var res = _httpClient.SendJsonAsync<IEnumerable<Filter>, List<T>>(HttpMethod.Post, $"{typeof(T).Name.Replace("DTO", "").ToLower()}/getlistall?{param}", filters);
			return res?.Data;
		}

	}
}