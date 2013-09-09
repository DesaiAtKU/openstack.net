using System;
using System.Runtime.Serialization;

namespace net.openstack.Core.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a parameter is incorrect or missing. 
    /// </summary>
    [Serializable]
    public class InvalidArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArgumentException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidArgumentException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArgumentException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="info"/> is <c>null</c>.</exception>
        protected InvalidArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
