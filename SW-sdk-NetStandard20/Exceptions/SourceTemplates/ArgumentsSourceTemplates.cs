using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using JetBrains.Annotations;
using SW.NetStandard20.Exceptions.Asserts;

namespace SW.NetStandard20.Exceptions.SourceTemplates
{
    [ExcludeFromCodeCoverage]
    public static class ArgumentsSourceTemplates
    {
        [SourceTemplate]
        [Conditional("DEBUG")]
        public static void AssertIsNotNull(this object arg)
        {
            ArgsCheck.IsNotNull(nameof(arg), arg);
            //$ $END$
        }

        [SourceTemplate]
        [Conditional("DEBUG")]
        public static void AssertDetailIsNotNull<T>(this FaultException<T> arg)
        {
            ArgsCheck.DetailIsNotNull(arg);
            //$ $END$
        }


        [SourceTemplate]
        [Conditional("DEBUG")]
        public static void AssertIsNotNullOrEmptyOrWhiteSpace(this string arg)
        {
            ArgsCheck.IsNotNullOrEmptyOrWhiteSpace(nameof(arg), arg);
            //$ $END$
        }
    }
}
