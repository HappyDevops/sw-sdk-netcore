using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;

namespace SW.NetStandard20.Exceptions.Asserts
{
    [ExcludeFromCodeCoverage]
    public static class ArgsCheck
    {
        [DebuggerStepThrough]
        public static void IsNotNull(string nameof, object arg)
        {
            if (arg != null)
                return;
            if (string.IsNullOrWhiteSpace(nameof))
                throw new ArgumentNullException();
            throw new ArgumentNullException(nameof);
        }

        [DebuggerStepThrough]
        public static void IsNotNull(object arg)
        {
            IsNotNull((string)null, arg);
        }

        [DebuggerStepThrough]
        public static void DetailIsNotNull<T>(FaultException<T> exception)
        {
            IsNotNull("Detail", (object)exception.Detail);
        }

        [DebuggerStepThrough]
        public static void IsNotNullOrEmptyOrWhiteSpace(string nameof, string arg)
        {
            if (!string.IsNullOrWhiteSpace(arg))
                return;
            if (string.IsNullOrWhiteSpace(nameof))
                throw new ArgumentNullException();
            throw new ArgumentNullException(nameof);
        }
    }
}
