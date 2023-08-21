using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Shopping.Utility
{

    public class HttpResponseException : Exception
    {
        public HttpResponseException(ExType value) =>
            (StatusCode, Value) = (value.Status, value);

        public HttpStatusCode StatusCode { get; }

        public object? Value { get; }
    }

    public class ExType
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; }

    }
}
