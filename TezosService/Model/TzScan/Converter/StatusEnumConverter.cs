using System;
using Newtonsoft.Json;

namespace TezosService.Model.TzScan.Converter
{
    internal class StatusEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(StatusEnum) || t == typeof(StatusEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "cycle_in_progress":
                    return StatusEnum.CycleInProgress;
                case "cycle_pending":
                    return StatusEnum.CyclePending;
                case "rewards_delivered":
                    return StatusEnum.RewardsDelivered;
                case "rewards_pending":
                    return StatusEnum.RewardsPending;
            }
            throw new Exception("Cannot unmarshal type StatusEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (StatusEnum)untypedValue;
            switch (value)
            {
                case StatusEnum.CycleInProgress:
                    serializer.Serialize(writer, "cycle_in_progress");
                    return;
                case StatusEnum.CyclePending:
                    serializer.Serialize(writer, "cycle_pending");
                    return;
                case StatusEnum.RewardsDelivered:
                    serializer.Serialize(writer, "rewards_delivered");
                    return;
                case StatusEnum.RewardsPending:
                    serializer.Serialize(writer, "rewards_pending");
                    return;
            }
            throw new Exception("Cannot marshal type StatusEnum");
        }

        public static readonly StatusEnumConverter Singleton = new StatusEnumConverter();
    }
}