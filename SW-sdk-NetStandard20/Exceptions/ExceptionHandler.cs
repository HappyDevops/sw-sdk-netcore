using System;

namespace SW.NetStandard20.Exceptions
{
    public static class ExceptionHandler
    {
        public static void HandleException(Exception exception, ErrorLevel level, ErrorHandlePolicy policy)
        {

        } 
        
        public static void HandleError(Exception exception, ErrorHandlePolicy policy)
        {
            HandleException(exception,ErrorLevel.Error, policy);
        }
    }
}