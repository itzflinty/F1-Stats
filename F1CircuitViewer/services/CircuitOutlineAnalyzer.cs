using models;

namespace services;

/// <summary>
/// Provides analysis utilities for interpreting and summarizing circuit geometries.
/// Specifically used to extract user-friendly information such as point counts and geometry types.
/// </summary>
public static class CircuitOutlineAnalyzer
{
    /// <summary>
    /// Generates a short summary of the circuit’s geometry.
    /// Includes geometry type, total point count, and handles fallback cases.
    /// </summary>
    /// <param name="feature">The circuit feature containing geometry and metadata.</param>
    /// <returns>A formatted string describing the circuit outline.</returns>
    public static string GetOutlineSummary(Feature feature)
    {
        var name = feature.Properties?.Name ?? "Unknown";
        var geometry = feature.Geometry;

        // Check for missing or undefined coordinate data
        if (geometry?.Coordinates is null)
            return $"• 🏎️ {name} — ⚠️ No coordinate data found";

        // Determine point summary based on geometry type and structure
        return geometry.Type switch
        {
            // Single continuous line
            "LineString" when geometry.Coordinates is List<List<double>> lineCoords =>
                $"• 🏎️ {name} — {lineCoords.Count} points in outline",

            // Multiple path segments (e.g., overlapping loops or alternative layouts)
            "MultiLineString" when geometry.Coordinates is List<List<List<double>>> multiCoords =>
                $"• 🏎️ {name} — {multiCoords.Sum(seg => seg?.Count ?? 0)} points across {multiCoords.Count} segments",

            // Catch-all for unsupported or misidentified structures
            _ => $"• 🏎️ {name} — ⚠️ Unhandled geometry type ({geometry.Type})"
        };
    }
}
