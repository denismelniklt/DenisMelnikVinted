namespace Domain
{
    public class Package
    {
        public PackageSize Size { get; set; }
        public PackageProvider Provider { get; set; }
        public Shipment Shipment { get; set; } = new Shipment();
    }
}