using Contacts.ValueObjects;

namespace Contacts.Interfaces
{
    public interface IOpenLinkService
    {
        void WhatsAppChat(Cellphone cellphone);
        void GoogleMaps(Address address);
    }
}
