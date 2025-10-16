using System;

namespace shopify.Services
{
    public class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception e) : base(message,e) { }
    }


    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() { }  
        public CustomerNotFoundException(string message) : base(message) { }
        public CustomerNotFoundException(string message, Exception e) : base(message,e) { }
    }
}
