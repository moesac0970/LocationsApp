using System;
using Xamarin.Essentials;


namespace B4.PE3.VanLookManu.Domain.Models
{
    public class LocationUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public Location Location { get; set; }
        public DateTime Created { get; set; }
        public System.Drawing.Color Color { get; set; }
    }
}
