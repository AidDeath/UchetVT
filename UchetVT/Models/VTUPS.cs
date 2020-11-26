namespace UchetVT
{
    public class VTUPS
    {
        public int Id { get; set; }
        public UPS UPS { get; set; }
        public int UPSId { get; set; }
        public int YearUsingSince { get; set; }
        public string InventoryNo { get; set; }
        public string SerialNumber { get; set; }
        public string Room { get; set; }
        public string Note { get; set; }
        public bool InUse { get; set; }
        public int OwnerRegion { get; set; }

    }
}
