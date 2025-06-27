using System.Text.Json;
using models;

namespace services;

/// <summary>
/// Responsible for loading and parsing a GeoJSON file containing circuit data.
/// Provides querying capabilities to find circuits by name from the loaded data.
/// </summary>
public class GeoJsonParser
{
    /// <summary>
    /// Holds the deserialized GeoJSON data, including all circuit features.
    /// </summary>
    private readonly FeatureCollection _featureCollection;

    /// <summary>
    /// Constructs the parser and loads the GeoJSON file into memory.
    /// Deserialization is configured to be case-insensitive to handle property naming variations.
    /// </summary>
    /// <param name="filePath">Path to the .geojson file containing F1 circuit features.</param>
    public GeoJsonParser(string filePath)
    {
        // Load the raw GeoJSON string from disk
        var json = File.ReadAllText(filePath);

        // Configure deserialization with case-insensitive matching for property names
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Deserialize the GeoJSON into a FeatureCollection
        var parsed = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Fallback to empty list if deserialization fails or features list is null
        _featureCollection = parsed?.Features == null
            ? new FeatureCollection { Features = new List<Feature>() }
            : parsed;
    }

    /// <summary>
    /// Finds all circuits whose name contains the specified search term (case-insensitive).
    /// </summary>
    /// <param name="name">The circuit name or keyword to search for.</param>
    /// <returns>A list of matching circuit features.</returns>
    public List<Feature> FindCircuitsByName(string name)
    {
        var results = _featureCollection.Features
            .Where(f =>
                f.Properties?.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) == true
            )
            .ToList();

        return results;
    }
}