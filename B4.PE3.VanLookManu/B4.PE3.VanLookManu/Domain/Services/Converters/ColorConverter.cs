using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Linq;

namespace B4.PE3.VanLookManu.Domain.Services
{
    /// <summary>
    /// converter for the colors coming from the json object
    /// </summary>
    public class ColorConverter : CustomCreationConverter<Color>
    {
        public override bool CanWrite { get { return false; } }
        public override bool CanRead { get { return true; } }
        public ColorConverter() { }
        public override Color Create(Type objectType)
        {
            return new Color();
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            Color target = Create(objectType);
            target = Color.FromArgb(jObject["A"].Value<Int32>(), jObject["R"].Value<Int32>(), jObject["G"].Value<Int32>(), jObject["B"].Value<Int32>());
            ColorList colors = new ColorList();
            Color color = colors.Where(c => (c.A == target.A) && (c.R == target.R) && (c.B == target.B) && (c.G == target.G)).FirstOrDefault();

            return color;
        }
    }
}
