using System;
using Newtonsoft.Json;

namespace TezosService.Model.TzScan.Converter
{
    internal class HashConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Hash) || t == typeof(Hash?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "PsddFKi32cMJ2qPjf43Qv5GDWLDPZb3T3bF6fLKiF5HtvHNU7aP")
            {
                return Hash.PsddFKi32CMj2QPjf43Qv5GdwldpZb3T3BF6FLKiF5HtvHnu7AP;
            }
            throw new Exception("Cannot unmarshal type Hash");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Hash)untypedValue;
            if (value == Hash.PsddFKi32CMj2QPjf43Qv5GdwldpZb3T3BF6FLKiF5HtvHnu7AP)
            {
                serializer.Serialize(writer, "PsddFKi32cMJ2qPjf43Qv5GDWLDPZb3T3bF6fLKiF5HtvHNU7aP");
                return;
            }
            throw new Exception("Cannot marshal type Hash");
        }

        public static readonly HashConverter Singleton = new HashConverter();
    }
}