using System.Text.Json.Serialization;

namespace models;

/// <summary>
/// Represents a GeoJSON FeatureCollection — the top-level structure that groups multiple geographic features,
/// each describing an individual F1 circuit with geometry and metadata.
/// </summary>
public class FeatureCollection
{
    /// <summary>
    /// Identifies this object as a "FeatureCollection" (per GeoJSON specification).
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// A list of circuit features included in this collection.
    /// Each feature contains both geometry and descriptive properties.
    /// </summary>
    [JsonPropertyName("features")]
    public List<Feature>? Features { get; set; }
}