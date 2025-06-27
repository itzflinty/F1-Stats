using System.Text.Json;
using System.Text.Json.Serialization;

namespace models;

/// <summary>
/// Custom JSON converter for the Geometry class.
/// Dynamically deserializes GeoJSON geometry objects that may have different coordinate structures
/// based on their type (e.g., LineString vs. MultiLineString).
/// </summary>
public class GeometryConverter : JsonConverter<Geometry>
{
    /// <summary>
    /// Reads and deserializes a Geometry object from JSON,
    /// inspecting the "type" field to determine the correct shape for the coordinates.
    /// </summary>
    public override Geometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Parse the entire geometry object from the incoming JSON
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        // Extract the GeoJSON type (e.g., "LineString", "MultiLineString")
        var type = root.GetProperty("type").GetString();
        var geometry = new Geometry { Type = type };

        // Deserialize coordinates based on the geometry type
        if (type == "LineString")
        {
            // A LineString has shape: List<List<double>> (lon/lat point list)
            geometry.Coordinates = root.GetProperty("coordinates").Deserialize<List<List<double>>>(options);
        }
        else if (type == "MultiLineString")
        {
            // A MultiLineString has shape: List<List<List<double>>> (multiple point lists)
            geometry.Coordinates = root.GetProperty("coordinates").Deserialize<List<List<List<double>>>>(options);
        }
        else
        {
            // Unsupported or unknown geometry type — prevent silent failure
            throw new JsonException($"Unsupported geometry type: {type}");
        }

        return geometry;
    }

    /// <summary>
    /// Not implemented because Geometry is intended to be deserialized only.
    /// Serialization to JSON isn't required in this application's context.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, Geometry value, JsonSerializerOptions options)
        => throw new NotImplementedException();
}
