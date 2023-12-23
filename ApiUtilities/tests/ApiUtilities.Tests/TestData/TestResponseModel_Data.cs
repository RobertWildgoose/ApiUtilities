using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUtilities.Tests.TestData
{
	public static class TestResponseModel_Data
	{
		public const string EmptyDataObject = "";
		public const string ValidDataObject = "\r\n  {\"Name\": \"Item1\"}";
		public const string ValidListDataObject = "[\r\n  {\"Name\": \"Item1\"},\r\n  {\"Name\": \"Item2\"},\r\n  {\"Name\": \"Item3\"},\r\n  {\"Name\": \"Item4\"}\r\n]";
		public const string InvalidDataObject = "[\r\n  {\"Name\": \"Item1\"},\r\n  {\"Name\": \"Item2\"},\r\n  {\"Name\": \"Item3\"},\r\n  {\"Name\": \"Item4\"}\r\n]";
	}
}
