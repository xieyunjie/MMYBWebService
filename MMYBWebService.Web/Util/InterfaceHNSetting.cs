using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Util
{
    public record InterfaceHNSetting
    {
        public int Port { get; init; }
        public string Server { get; init; }
        public string Servle { get; init; }
        public string CenterId { get; init; }
        public string CenterName { get; init; }
        public string HospitalId { get; init; }
        public string HospitalName { get; init; }
        public string HospitalId_pwd { get; init; }
        public string StaffId { get; init; }
        public string StaffId_pwd { get; init; }
        public int SetDebug { get; init; }
        public string LogPath { get; init; }
    }
}
