using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Java.Util;
using System.Net.Http.Headers;
using System.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace Musico
{
	public class MusicoConnUtil
	{


		public static List<Band> SearchBandsAsync (string location, string genre, int minPrice, int maxPrice, int minAvgRate, DateTime availableDate, string name)
		{
			List<Band> bandList;
			HttpResponseMessage response;

			NameValueCollection values = System.Web.HttpUtility.ParseQueryString(string.Empty);


			if (location != null)
				values ["location"] = location;
			if (genre != null)
				values ["genre"] = genre;
			if (minPrice >= 0)
				values ["minPrice"] = minPrice.ToString();
			if (maxPrice > 0)
				values ["maxPrice"] = maxPrice.ToString();
			if (minAvgRate > 0)
				values ["minRate"] = minAvgRate.ToString();
			if (name !=null)
				values ["name"] = name;
			

			values ["availableDate"] = availableDate.ToString ("yyyy-MM-dd");
				
			
			if (values.Count > 0){
				 response = MakeServerGetRequest (Globals.BANDS_SEARCH+"?"+values.ToString());
			}else{
				 response = MakeServerGetRequest (Globals.BANDS);
			}


			if (response.IsSuccessStatusCode){
				bandList = JsonConvert.DeserializeObject <List<Band>> (response.Content.ReadAsStringAsync ().Result);
				return bandList;
			}

			return null;

		}

		public static Band GetBandByNameAsync(string name)
		{
			Band band;
			HttpResponseMessage response = MakeServerGetRequest (Globals.BAND_BY_NAME+"?name="+name);

			if (response.IsSuccessStatusCode){
				band = JsonConvert.DeserializeObject <Band> (response.Content.ReadAsStringAsync ().Result);
				return band;
			}

			return null;

		}

		public static List<Band> GetAllBandsAsync ()
		{
			List<Band> bandList;
			HttpResponseMessage response = MakeServerGetRequest (Globals.BANDS);

			if (response.IsSuccessStatusCode){
				bandList = JsonConvert.DeserializeObject <List<Band>> (response.Content.ReadAsStringAsync ().Result);
				return bandList;
			}

			return null;

		}

		public static async Task<int> AuthenticateUser(string email, string pass){
			int result;

			var values = new Dictionary<string, string> {
				{ "username", email },
				{ "password", pass }
			};

			var content = new FormUrlEncodedContent (values);

			HttpResponseMessage response = null;
			response = await MakeServerPostRequest(Globals.AUTH,content);

			if ( response.StatusCode==HttpStatusCode.NotFound){
				return -1;
			}else{
				JObject resultJson = JObject.Parse (response.Content.ReadAsStringAsync ().Result);

				result = Int32.Parse(resultJson.GetValue ("id").ToString());

				return result;
			}

		} 

		public static List<TopUser> GetTopUsers(){
			List<TopUser> topUserList;

			HttpResponseMessage response = MakeServerGetRequest (Globals.TOP_USERS);

			if (response.IsSuccessStatusCode){
				topUserList = JsonConvert.DeserializeObject <List<TopUser>> (response.Content.ReadAsStringAsync ().Result);
				return topUserList;
			}

			return null;
		}

		public static async Task<int> AddReview(string comment, float overall, float quality, float punctuality, float flexibility, float enthusiasm, float similarity, string userId, string bandId )
		{
			int result;
			var values = new Dictionary<string, string> {
				{ "comment", comment },
				{ "rate", Convert.ToInt32 (overall)+"" },
				{"rateQuality", Convert.ToInt32(quality)+""},
				{"ratePunctuality", Convert.ToInt32(punctuality)+""},
				{"rateFlexibility", Convert.ToInt32(flexibility)+""},
				{"rateEnthusiasm", Convert.ToInt32(enthusiasm)+""},
				{"rateSimilarity", Convert.ToInt32(similarity)+""},
				{"userId", userId}
			};

			var content = new FormUrlEncodedContent (values);

			HttpResponseMessage response = null;
			response = await MakeServerPostRequest(Globals.BAND+"/"+bandId+Globals.BAND_REVIEW,content);

			if ( response.StatusCode==HttpStatusCode.InternalServerError){
				return -1;
			}else{
				JObject resultJson = JObject.Parse (response.Content.ReadAsStringAsync ().Result);

				result = Int32.Parse(resultJson.GetValue ("id").ToString());

				return result;
			}

		}

		public static async Task<int> AddComment(string comment, string type, string bandId, string userId){
			int result;

			var values = new Dictionary<string,string> {
				{ "comment", comment },
				{ "type", type },
				{ "userId", userId }
			};

			var content = new FormUrlEncodedContent (values);

			HttpResponseMessage response = null;
			response = await MakeServerPostRequest(Globals.BAND+"/"+bandId+Globals.BAND_COMMENT,content);


			if ( response.StatusCode==HttpStatusCode.InternalServerError){
				return -1;
			}else{
				JObject resultJson = JObject.Parse (response.Content.ReadAsStringAsync ().Result);

				result = Int32.Parse(resultJson.GetValue ("id").ToString());

				return result;
			}

		}

		public static async Task<int> AddBooking (string description, DateTime date, string userId, string bandId){
			int result;

			var values = new Dictionary<string,string> {
				{ "description", description },
				{ "date", date.ToString ("yyyy-MM-dd")},
				{ "userId", userId}
			};

			var content = new FormUrlEncodedContent (values);

			HttpResponseMessage response = null;

			response = await MakeServerPostRequest(Globals.BAND+"/"+bandId+Globals.BAND_BOOKING,content);


			if ( response.StatusCode==HttpStatusCode.InternalServerError){
				return -1;
			}else{
				JObject resultJson = JObject.Parse (response.Content.ReadAsStringAsync ().Result);

				result = Int32.Parse(resultJson.GetValue ("id").ToString());

				return result;
			}

		}

		private static HttpResponseMessage MakeServerGetRequest (string url)
		{
			HttpClient client = new HttpClient ();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			
			//client.MaxResponseContentBufferSize = 256000;

			var uri = new Uri (string.Format (url));

			HttpResponseMessage response = null;
			try {

				response = client.GetAsync (uri).Result;

			} catch (WebException e) {
				if (e.Response == null)
					throw new WebException ("Error connecting to the server: " + url + " Possible Internet problems");

				throw new WebException ("Error connecting to the server: " + url + " Status code: " + ((HttpWebResponse)e.Response).StatusCode);
			}

			if ((int)response.StatusCode == 500 || (int)response.StatusCode == 401 || (int)response.StatusCode == 403 || (int)response.StatusCode == 404 || (int)response.StatusCode == 502 || (int)response.StatusCode == 503 || (int)response.StatusCode == 504) {
				throw new WebException ("Error connecting to the server. Status code: " + response.StatusCode);
			}
			return response;

		}

		private static async Task<HttpResponseMessage> MakeServerPostRequest (string url, FormUrlEncodedContent content)
		{
			HttpClient client = new HttpClient ();
			client.MaxResponseContentBufferSize = 256000;

			var uri = new Uri (string.Format (url));

			HttpResponseMessage response = null;
			try {

				response = await client.PostAsync (uri, content);

			} catch (WebException e) {
				if (e.Response == null)
					throw new WebException ("Error connecting to the server: " + url + " Possible Internet problems");

				throw new WebException ("Error connecting to the server: " + url + " Status code: " + ((HttpWebResponse)e.Response).StatusCode);
			}

			if ((int)response.StatusCode == 500 || (int)response.StatusCode == 401 || (int)response.StatusCode == 403 || (int)response.StatusCode == 502 || (int)response.StatusCode == 503 || (int)response.StatusCode == 504) {
				throw new WebException ("Error connecting to the server. Status code: " + response.StatusCode);
			}
			Console.WriteLine ("----RETURNING FROM POST: "+response.Content.ReadAsStringAsync ().Result.ToString());
			return response;

		}





		public MusicoConnUtil ()
		{
		}
	}
}

