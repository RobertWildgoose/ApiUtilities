﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApiUtilities.Common.Models
{
	public class BaseResponse
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
	}
}
