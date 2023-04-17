using System;

namespace Hotel.Application
{
    public class ApplicationModuleException : Exception
    {
        public ApplicationModuleException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}