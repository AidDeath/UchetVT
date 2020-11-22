using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UchetVT
{
    public class VTNetworkDevice
    {
        public int Id { get; set; }
        public NetworkDevice NetworkDevice { get; set; }
        public int DeviceId { get; set; }
        public int YearUsingSince { get; set; }
        public string InventoryNo { get; set; }
        public string SerialNumber { get; set; }
        public string Ip { get; set; }
        public string Room { get; set; }
        public string Note { get; set; }
        public bool InUse { get; set; }
        public int OwnerRegion { get; set; }
    }
}
