using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SW.NetStandard20.Services.Interfaces
{
    interface IResponseable
    {
         string Status { get; set; }
         string Message { get; set; }
         string MessageDetail { get; set; }
    }
}
