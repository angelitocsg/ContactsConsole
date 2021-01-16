using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Contacts.Interfaces;
using Contacts.ValueObjects;

namespace Contacts.Services
{
    public class OpenLinkService : IOpenLinkService
    {
        // Source: https://faq.whatsapp.com/general/chats/how-to-use-click-to-chat/?lang=pt_br
        public void WhatsAppChat(Cellphone cellphone)
        {
            OpenUrl($"https://wa.me/55{cellphone.OnlyNumbers()}?text=Vamos%20conversar?");
        }

        // Source: https://developers.google.com/maps/documentation/urls/get-started
        public void GoogleMaps(Address address)
        {
            OpenUrl($"https://www.google.com/maps/search/?api=1&query={address}");
        }

        private void OpenUrl(string url)
        {
            url = System.Uri.EscapeUriString(url);
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
