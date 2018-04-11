using Domain.Dto;

namespace Domain
{
    public struct PackageDto
    {
        public string Size { get; set; }
        public string Provider { get; set; }
        public ShipmentDto Shipment {get;set;}
    }
}
