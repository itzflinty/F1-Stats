using System.Text.Json.Serialization;

namespace models;

/// <summary>
/// Represents a single GeoJSON feature, typically corresponding to one F1 circuit.
/// Each feature contains its geometry (track shape) and metadata (like name, location).
/// </summary>
public class Feature
{
    /// <summary>
    /// The GeoJSON object type — usually "Feature".
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Geometric description of the circuit's layout (e.g., LineString or MultiLineString).
    /// </summary>
    [JsonPropertyName("geometry")]
    public Geometry? Geometry { get; set; }

    /// <summary>
    /// Track-specific metadata such as name, location, length, etc.
    /// </summary>
    [JsonPropertyName("properties")]
    public Properties? Properties { get; set; }
}
