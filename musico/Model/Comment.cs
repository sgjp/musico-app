using System;
using Newtonsoft.Json;

namespace Musico
{
	public class Comment
	{
		public string Id  {get; set;}

		[JsonProperty(PropertyName = "comment",NullValueHandling=NullValueHandling.Ignore)]
		public string CommentText {get; set;}

		public int Type { get; set;}


		public Comment ()
		{
		}
	}
}

