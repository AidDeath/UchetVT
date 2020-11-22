namespace UchetVT
{/// <summary>
/// Интерфейс для класса инициализации репозиториев
/// </summary>
    public interface IUnitOfWork
    { 
        IRepository<Board> Boards { get; }
        IRepository<Book> Books { get; }
        IRepository<CPU> CPUs { get; }
        IRepository<HDD> HDDs { get; }
        IRepository<License> Licenses { get; }
        IRepository<NetworkDevice> NetworkDevices { get; }
        IRepository<OS> OSes { get; }
        IRepository<Printer> Printers { get; }
        IRepository<Region> Regions { get; }
        IRepository<UPS> UPSes { get; }
        IRepository<User> Users { get; }

        IVtRepository<VTComputer> VTComputers { get; }
        IVtRepository<VTNetworkDevice> VTNetworkDevices { get; }
        IVtRepository<VTPrinter> VTPrinters { get; }
        IVtRepository<VTUPS> VTUPSes { get; }

    }
}
