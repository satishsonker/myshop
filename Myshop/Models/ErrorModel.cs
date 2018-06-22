using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
}