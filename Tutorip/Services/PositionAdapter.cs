using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Tutorip.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Tutorip.Services
{
    class PositionAdapter
    {
        private Geocoder geoCoder;
        public PositionAdapter() 
        {
            this.geoCoder = new Geocoder();
        }

        public async Task<Posizione> calcolaPosizione()
        {
            Posizione p = null;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    p = new Posizione();
                    p.latitudine = location.Latitude;
                    p.longitudine = location.Longitude;
                    p.indirizzo =  (string) await Posizione2IndirizzoAsync(new Position(location.Latitude, location.Longitude));
                }

            }
            catch
            {
                return p;
            }
            return p;
        }

        public async Task<string> Posizione2IndirizzoAsync(Position position)
        {
            IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
            string address = possibleAddresses.FirstOrDefault();
            return address;
        }

        public async Task<Posizione> Indirizzo2Posizione(String indirizzo)
        {
            Posizione p = new Posizione();
            
            Console.WriteLine(indirizzo);
            var locations = await Geocoding.GetLocationsAsync(indirizzo);
            var location = locations?.FirstOrDefault();
            try
            {
                if (location == null)
                {
                    return p;
                }
                else
                {
                    p.latitudine = location.Latitude;
                    p.longitudine = location.Longitude;
                    p.indirizzo = indirizzo;
                }
            }
            catch
            {
                Console.WriteLine("ERORE FAI I TESTE");
            }
            //String i = await this.Posizione2IndirizzoAsync(new Position(p.latitudine, p.longitudine));
            //Console.WriteLine(i);
            return p;
        }

        public String approssimaDistanza (String distanza)
        {
            int max_length = 4;
            int l = distanza.Length;
            if(l < max_length)
            {
                return distanza;
            }
            distanza = distanza.Substring(0, max_length);
            if (distanza.Equals("0.00"))
                return "0";
            var i = 0;
            if(distanza.Contains('.'))
                i = distanza.IndexOf('.');
            if(i == 3)
                return distanza.Substring(0, 3);
            distanza = distanza.Substring(0, max_length - i);

            distanza += "Km";
            return distanza;
        }

    }
}
