using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DNSUpdater.Utils.Exceptions
{    

    [Serializable]
    // Important: This attribute is NOT inherited from Exception, and MUST be specified 
    // otherwise serialization will fail with a SerializationException stating that
    // "Type X in Assembly Y is not marked as serializable."
    public class ProjectException : Exception
    {
        public ProjectException()
        {
        }

        public ProjectException(string message)
            : base(message)
        {
        }

        public ProjectException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected ProjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}