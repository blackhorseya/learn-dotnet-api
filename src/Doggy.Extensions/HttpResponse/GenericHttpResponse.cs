using System;

namespace Doggy.Extensions.HttpResponse
{
    public class GenericHttpResponse
    {
        public int Code { get; set; }
        public bool Ok { get; set; }
        public object Data { get; set; }
    }
}