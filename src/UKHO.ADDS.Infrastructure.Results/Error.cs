﻿namespace UKHO.ADDS.Infrastructure.Results
{
    /// <summary>Represents an error with a message and associated metadata.</summary>
    public class Error : IError
    {
        private static readonly IReadOnlyDictionary<string, object> _emptyMetaData = new Dictionary<string, object>();

        /// <summary>Initializes a new instance of the <see cref="Error" /> class.</summary>
        public Error()
            : this("")
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified error message.</summary>
        /// <param name="message">The error message.</param>
        public Error(string message)
        {
            Message = message;
            Metadata = _emptyMetaData;
        }

        /// <summary>Initializes a new instance of the <see cref="Error" /> class with the specified error message and metadata.</summary>
        /// <param name="message">The error message.</param>
        /// <param name="metadata">The metadata associated with the error.</param>
        public Error(string message, IReadOnlyDictionary<string, object> metadata)
        {
            Message = message;
            Metadata = metadata;
        }

        internal static IError Empty { get; } = new Error("", new Dictionary<string, object>());
        internal static IReadOnlyList<IError> EmptyErrorList { get; } = [];
        internal static IReadOnlyList<IError> DefaultErrorList { get; } = [new Error("", new Dictionary<string, object>())];

        /// <inheritdoc />
        public string Message { get; init; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, object> Metadata { get; init; }

        /// <inheritdoc />
        public override string ToString()
        {
            var errorType = GetType()
                .Name;

            if (Message.Length == 0)
            {
                return errorType;
            }

            return StringUtilities.GetErrorString(errorType, Message);
        }
    }
}
