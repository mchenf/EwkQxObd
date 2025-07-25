using System.Text.Json;
using System.Text.Json.Serialization;

namespace EwkQxObd.WebApi.Conversion
{
    public class JsonSingleArrayConverter<T> : JsonConverter<List<T>>
    {
        public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<T>();
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    var item = JsonSerializer.Deserialize<T>(ref reader, options);
                    if (item == null) continue;
                    list.Add(item);
                }
            }
            else
            {
                var item = JsonSerializer.Deserialize<T>(ref reader, options);
                if (item != null) list.Add(item);

            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {

            if (value.Count == 1)
            {
                JsonSerializer.Serialize(writer, value[0], options);
            } 
            else
            {
                writer.WriteStartArray();
                foreach (var item in value)
                {
                    JsonSerializer.Serialize(writer, item, options);
                }
                writer.WriteEndArray();
            }
        }
    }
}
