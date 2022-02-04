namespace WorkloadsApi.Mediators
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class CustomJsonConverterForType : JsonConverter<System.Type>
    {
        //WARNING! This is a breach in NET security recommendations regarding (de)serializing types
        public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? assemblyQualifiedName = reader.GetString();
            return Type.GetType(assemblyQualifiedName);
        }

        public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
        {
            string assemblyQualifiedName = value.AssemblyQualifiedName;
            writer.WriteStringValue(assemblyQualifiedName);
        }
    }
}