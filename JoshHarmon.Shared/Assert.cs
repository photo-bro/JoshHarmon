using System;
using System.Runtime.Serialization;

namespace JoshHarmon.Shared
{
    public static class Assert
    {
        public static void NotNull<T>(T obj) where T : class
        {
            if (obj == null)
            {
                throw new AssertionFailedException($"Object with type '{typeof(T)}' was null when not-null was expected.");
            }
        }

        public static void True<T>(Predicate<T> predicate, T obj)
        {
            NotNull(predicate);
            True(predicate.Invoke(obj));
        }

        public static void True(bool value)
        {
            if (!value)
            {
                throw new AssertionFailedException($"'{nameof(value)}' was 'false' when 'true' was expected.");
            }
        }

        public static void NotNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new AssertionFailedException($"'{nameof(value)}' was null or empty.");
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
