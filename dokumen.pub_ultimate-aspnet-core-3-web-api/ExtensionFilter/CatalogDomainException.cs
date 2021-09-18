using System;
namespace dokumen.pub_ultimate_aspnet_core_3_web_api.ExtensionFilter
{   
        public class CatalogDomainException : Exception
    {
        public CatalogDomainException()
        { }

        public CatalogDomainException(string message)
            : base(message)
        { }

        public CatalogDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}