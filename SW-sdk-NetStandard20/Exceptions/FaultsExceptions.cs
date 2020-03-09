using System;
using System.ServiceModel;
using SW.NetStandard20.Services.Responses;

namespace SW.NetStandard20.Exceptions
{
    public static class FaultsExceptions
    {
        public static FaultException<FaultResponse> GetHttpPostFailedException()
        {
            throw  new NotImplementedException();
        }

        public static FaultException<FaultResponse> GetHttpPostCancelledException()
        {
            throw  new NotImplementedException();
        }
    }
}
