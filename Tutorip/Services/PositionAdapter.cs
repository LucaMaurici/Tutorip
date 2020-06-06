﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Posizione p = null;
            var locations = await Geocoding.GetLocationsAsync(indirizzo);
            if (locations == null)
            {
                return p;
            }
            else
            {
                Location location = locations.FirstOrDefault();
                p.latitudine = location.Latitude;
                p.longitudine = location.Longitude;
                p.indirizzo = indirizzo;
            }
            return p;
        }
    }
}