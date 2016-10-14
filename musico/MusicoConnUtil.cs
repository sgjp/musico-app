using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Java.Util;
using System.Net.Http.Headers;

namespace Musico
{
	public class MusicoConnUtil
	{


		public static async Task<IList<Band>> GetAllBandsAsync ()
		{
			IList<Band> bandList;
			HttpResponseMessage response = await MakeServerGetRequest (Globals.BANDS);

			if (response.IsSuccessStatusCode){
				bandList = JsonConvert.DeserializeObject <IList<Band>> (response.Content.ReadAsStringAsync ().Result);
				return bandList;
			}

			return null;

		}


		private static Task<HttpResponseMessage> MakeServerGetRequest (string url)
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

		private static async Task<HttpResponseMessage> MakeServerPostRequest (string url, StringContent content)
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

			if ((int)response.StatusCode == 500 || (int)response.StatusCode == 401 || (int)response.StatusCode == 403 || (int)response.StatusCode == 404 || (int)response.StatusCode == 502 || (int)response.StatusCode == 503 || (int)response.StatusCode == 504) {
				throw new WebException ("Error connecting to the server. Status code: " + response.StatusCode);
			}
			return response;

		}




		public MusicoConnUtil ()
		{
		}
	}
}

