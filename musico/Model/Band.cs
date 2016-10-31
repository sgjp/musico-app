using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Android.Text.Style;

namespace Musico
{
	public class Band: IComparable<Band>
	{
		[JsonProperty(PropertyName = "id",NullValueHandling=NullValueHandling.Ignore)]
		public string Id {get; set;}

		[JsonProperty(PropertyName = "name",NullValueHandling=NullValueHandling.Ignore)]
		public string Name {get; set;}

		[JsonProperty(PropertyName = "genre",NullValueHandling=NullValueHandling.Ignore)]
		public string Genre {get; set;}

		[JsonProperty(PropertyName = "youtube",NullValueHandling=NullValueHandling.Ignore)]
		public string Youtube {get; set;}

		[JsonProperty(PropertyName = "facebook",NullValueHandling=NullValueHandling.Ignore)]
		public string Facebook {get; set;}

		[JsonProperty(PropertyName = "requirements",NullValueHandling=NullValueHandling.Ignore)]
		public string Requirements {get; set;}

		[JsonProperty(PropertyName = "location",NullValueHandling=NullValueHandling.Ignore)]
		public string Location {get; set;}

		[JsonProperty(PropertyName = "avgPrice",NullValueHandling=NullValueHandling.Ignore)]
		public float AvgPrice {get; set;}

		[JsonProperty(PropertyName = "reviews",NullValueHandling=NullValueHandling.Ignore)]
		public IList<Review> Reviews { get; set;}

		[JsonProperty(PropertyName = "comments",NullValueHandling=NullValueHandling.Ignore)]
		public IList<Comment> Comments { get; set;}

		[JsonProperty(PropertyName = "bookings",NullValueHandling=NullValueHandling.Ignore)]
		public IList<Booking> Bookings { get; set;}

		[JsonProperty(PropertyName = "avgRate",NullValueHandling=NullValueHandling.Ignore)]
		public float AvgRate {get; set;}


		public int CompareTo(Band band)
		{
			return this.AvgRate.CompareTo(band.AvgRate);
		}

		public Band ()
		{
		}
	}
}

