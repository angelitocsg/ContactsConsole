using System;
using System.Diagnostics;
using Contacts.Interfaces;
using Contacts.ValueObjects;

namespace Contacts.Services
{
    public class OpenLinkService : IOpenLinkService
    {
        // Source: https://faq.whatsapp.com/general/chats/how-to-use-click-to-chat/?lang=pt_br
        public void WhatsAppChat(Cellphone cellphone)
        {
            Process.Start($"https://wa.me/{cellphone.OnlyNumbers()}");
        }

        // Source: https://developers.google.com/maps/documentation/urls/get-started
        public void GoogleMaps(Address address)
        {
            Process.Start($"https://www.google.com/maps/search/?api=1&query={address}");
        }
    }
}
