using Domain;

namespace DAL
{
    public interface IShipmentService
    {
        Shipment GetShipment(Transaction transaction);
    }
}