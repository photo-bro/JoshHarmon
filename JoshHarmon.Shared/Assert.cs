using System;
using System.Runtime.Serialization;

namespace JoshHarmon.Shared
{
    public static class Assert
    {
        public static void NotNull<T>(T? obj, string? parameterName = null) where T : class
        {
            if (obj == null)
            {
                var message = string.IsNullOrEmpty(parameterName)
                    ? $"Object with type '{typeof(T)}' was null when not-null was expected."
                    : $"Parameter '{parameterName}' with type '{typeof(T)}' was null when not-null was expected.";
                throw new AssertionFailedException(message);
            }
        }

        public static void True<T>(Predicate<T> predicate, T obj, string? parameterName = null)
        {
            NotNull(predicate, parameterName);
            True(predicate.Invoke(obj), parameterName);
        }

        public static void True(bool value, string? parameterName = null)
        {
            if (!value)
            {
                var message = string.IsNullOrEmpty(parameterName)
                    ? $"'{nameof(value)}' was 'false' when 'true' was expected."
                    : $"Parameter '{parameterName}' was 'false' when 'true' was expected.";
                throw new AssertionFailedException(message);
            }
        }

        public static void NotNullOrEmpty(string? value, string? parameterName = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                var message = string.IsNullOrEmpty(parameterName)
                    ? $"'{nameof(value)}' was null or empty."
                    : $"Parameter '{parameterName}' was null or empty.";
                throw new AssertionFailedException(message);
            }
        }

    }

    [Serializable]
    public class AssertionFailedException : Exception
    {
        public AssertionFailedException()
        { }

        public AssertionFailedException(string message) : base(message)
        { }

        public AssertionFailedException(string message, Exception innerException) : base(message, innerException)
        { }

        protected AssertionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
