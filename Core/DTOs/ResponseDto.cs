using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }


        //Static Factory Design Pattern
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode };
        }
        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode, Errors = errors };
        }
        public static ResponseDto<T> Fail(string error,int statusCode)
        {
            return new ResponseDto<T> { Data = default(T), StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
