using System.Text.Json.Serialization;

namespace models;

/// <summary>
/// Represents the geometric shape of a circuit track in GeoJSON format.
/// This class uses a custom converter to support multiple geometry types (e.g., LineString, MultiLineString),
/// which are deserialized into dynamic coordinate structures.
/// </summary>
[JsonConverter(typeof(GeometryConverter))]
public class Geometry
{
    /// <summary>
    /// The GeoJSON geometry type — commonly "LineString" or "MultiLineString".
    /// Determines how the Coordinates property should be interpreted.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Holds the coordinate data defining the circuit’s layout.
    /// Can be:
    /// - List&lt;List&lt;double&gt;&gt; for LineString
    /// - List&lt;List&lt;List&lt;double&gt;&gt;&gt; for MultiLineString
    /// The exact structure is handled by GeometryConverter at runtime.
    /// </summary>
    public object? Coordinates { get; set; }
}