namespace UchetVT
{
    public class VTComputer
    {
        public int Id { get; set; }
        public bool InUse { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; }

        public int CpuId { get; set; }
        public CPU Cpu { get; set; }

        public int CpuClockSpeed { get; set; }

        public int HddId { get; set; }
        public HDD Hdd { get; set; }

        public int HddCapacity { get; set; }
        public double RamCapacity { get; set; }
        public int YearUsingSince { get; set; }
        public string InventoryNo { get; set; }
        public string Hostname { get; set; }
        public string IpAddress { get; set; }
        public string WorkerName { get; set; }
        public string Room { get; set; }
        public string Note { get; set; }

        public int OwnerRegion { get; set; }


        public int OsId { get; set; }
        public OS OS { get; set; }

        public int LicenseId { get; set; }
        public License License { get; set; }


    }
}
