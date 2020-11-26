using UchetVT.Repositories;

namespace UchetVT
{
    /// <summary>
    /// Инициализация репозиториев
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        BoardRepository BoardRepository;
        BookRepository BookRepository;
        CPURepository CPURepository;
        HDDRepository HDDRepository;
        LicenseRepository LicenseRepository;
        NetworkDeviceRepository NetworkDeviceRepository;
        OSRepository OSRepository;
        PrinterRepository PrinterRepository;
        RegionRepository RegionRepository;
        UPSRepository UPSRepository;
        UserRepository UserRepository;
        VTComputerRepository VTComputerRepository;

        VTNetworkDeviceRepository VTNetworkDeviceRepository;
        VTPrinterRepository VTPrinterRepository;
        VTUPSRepository VTUPSRepository;



        public IRepository<Board> Boards =>
            BoardRepository ?? (BoardRepository = new BoardRepository());

        public IRepository<Book> Books =>
            BookRepository ?? (BookRepository = new BookRepository());

        public IRepository<CPU> CPUs =>
            CPURepository ?? (CPURepository = new CPURepository());

        public IRepository<License> Licenses =>
            LicenseRepository ?? (LicenseRepository = new LicenseRepository());

        public IRepository<HDD> HDDs =>
            HDDRepository ?? (HDDRepository = new HDDRepository());

        public IRepository<NetworkDevice> NetworkDevices =>
            NetworkDeviceRepository ?? (NetworkDeviceRepository = new NetworkDeviceRepository());

        public IRepository<OS> OSes =>
            OSRepository ?? (OSRepository = new OSRepository());

        public IRepository<Printer> Printers =>
            PrinterRepository ?? (PrinterRepository = new PrinterRepository());

        public IRepository<Region> Regions =>
            RegionRepository ?? (RegionRepository = new RegionRepository());

        public IRepository<UPS> UPSes =>
            UPSRepository ?? (UPSRepository = new UPSRepository());

        public IRepository<User> Users =>
            UserRepository ?? (UserRepository = new UserRepository());

        public IVtRepository<VTComputer> VTComputers =>
            VTComputerRepository ?? (VTComputerRepository = new VTComputerRepository());

        public IVtRepository<VTNetworkDevice> VTNetworkDevices =>
            VTNetworkDeviceRepository ?? (VTNetworkDeviceRepository = new VTNetworkDeviceRepository());

        public IVtRepository<VTPrinter> VTPrinters =>
            VTPrinterRepository ?? (VTPrinterRepository = new VTPrinterRepository());

        public IVtRepository<VTUPS> VTUPSes => VTUPSRepository ?? (VTUPSRepository = new VTUPSRepository());

    }
}
