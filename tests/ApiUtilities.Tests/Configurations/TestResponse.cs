using ApiUtilities.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Tests.Configurations
{
	public class TestResponse : BaseResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
	}
}
