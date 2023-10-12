

using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Media;

namespace FarFromFreedom.Model.Helpers
{
    public class JsonRectConverter : JsonConverter<RectangleGeometry>
    {
        public override RectangleGeometry? ReadJson(JsonReader reader, Type objectType, RectangleGeometry? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new RectangleGeometry();
        }

        public override void WriteJson(JsonWriter writer, RectangleGeometry? value, JsonSerializer serializer)
        {
            Rect convertedValue = value.Rect;
            serializer.Serialize(writer, convertedValue);
        }
    }
}
