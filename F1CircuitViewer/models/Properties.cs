using System.Text.Json.Serialization;

namespace models;

/// <summary>
/// Represents metadata associated with a single F1 circuit feature in GeoJSON.
/// These values include the name, geographic location, historical data, and physical characteristics of the circuit.
/// </summary>
public class Properties
{
    /// <summary>
    /// A unique identifier for the circuit.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Geographical location of the circuit (e.g., "England", "Monaco").
    /// </summary>
    [JsonPropertyName("Location")]
    public string? Location { get; set; }

    /// <summary>
    /// Official name of the circuit (e.g., "Silverstone Circuit").
    /// </summary>
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    /// <summary>
    /// Year the circuit was originally opened.
    /// </summary>
    [JsonPropertyName("opened")]
    public int? Opened { get; set; }

    /// <summary>
    /// Year the circuit first hosted a Formula 1 Grand Prix.
    /// </summary>
    [JsonPropertyName("firstgp")]
    public int? FirstGP { get; set; }

    /// <summary>
    /// Total length of the circuit in meters.
    /// </summary>
    [JsonPropertyName("length")]
    public int? Length { get; set; }

    /// <summary>
    /// Altitude of the circuit above sea level in meters.
    /// </summary>
    [JsonPropertyName("altitude")]
    public int? Altitude { get; set; }
}
