using System.Globalization;
using System.Text;
using models;

namespace services;

/// <summary>
/// Generates SVG markup from a circuit feature's geometry.
/// Scales and centers the path(s) in a configurable viewport for visual rendering.
/// </summary>
public static class SvgCircuitRenderer
{
    /// <summary>
    /// Converts the geometry of a circuit feature into a full SVG string.
    /// Supports LineString and MultiLineString types.
    /// </summary>
    /// <param name="feature">The circuit feature to render.</param>
    /// <param name="width">Canvas width in pixels.</param>
    /// <param name="height">Canvas height in pixels.</param>
    /// <returns>An SVG string representing the circuit outline.</returns>
    public static string RenderSvg(Feature feature, int width = 800, int height = 600)
    {
        var geometry = feature.Geometry;
        var name = feature.Properties?.Name ?? "Unknown";

        // Return an empty SVG comment if the circuit has no geometry
        if (geometry?.Coordinates == null)
            return $"<!-- ⚠️ No geometry for {name} -->";

        // Convert geometry into one or more SVG path strings depending on type
        var paths = geometry.Type switch
        {
            // Single continuous track
            "LineString" when geometry.Coordinates is List<List<double>> line =>
                new[] { ConvertToPath(line) },

            // Multiple disconnected segments or alternate layouts
            "MultiLineString" when geometry.Coordinates is List<List<List<double>>> multi =>
                multi.Select(ConvertToPath).ToArray(),

            // Unsupported or unrecognized geometry type
            _ => Array.Empty<string>()
        };

        if (paths.Length == 0)
            return $"<!-- ⚠️ Unsupported geometry for {name} -->";

        var svg = new StringBuilder();
        svg.AppendLine($"<svg xmlns='http://www.w3.org/2000/svg' width='{width}' height='{height}' viewBox='0 0 {width} {height}'>");

        // Render each path in the outline
        foreach (var d in paths)
        {
            svg.AppendLine($"  <path d='{d}' fill='none' stroke='lime' stroke-width='2' />");
        }

        // Optional label for the circuit
        svg.AppendLine($"  <text x='10' y='{height - 10}' fill='white' font-size='16'>{name}</text>");
        svg.AppendLine("</svg>");

        return svg.ToString();
    }

    /// <summary>
    /// Converts a list of geographic coordinate pairs into an SVG path string.
    /// Scales and translates the points to fit within the canvas dimensions.
    /// </summary>
    /// <param name="coordinates">A sequence of [lon, lat] points defining the track.</param>
    /// <returns>An SVG path string using MoveTo and LineTo commands.</returns>
    private static string ConvertToPath(List<List<double>> coordinates)
    {
        // 1. Compute original bounds
        var minX = coordinates.Min(p => p[0]);
        var maxX = coordinates.Max(p => p[0]);
        var minY = coordinates.Min(p => p[1]);
        var maxY = coordinates.Max(p => p[1]);

        // 2. Determine scale based on canvas size
        const double canvasWidth = 800;
        const double canvasHeight = 600;
        var scaleX = canvasWidth * 0.9 / (maxX - minX); // 90% of canvas with margin
        var scaleY = canvasHeight * 0.9 / (maxY - minY);
        var scale = Math.Min(scaleX, scaleY);

        // 3. Scale and collect transformed points
        var transformed = coordinates
            .Select(p => new
            {
                X = (p[0] - minX) * scale,
                Y = (maxY - p[1]) * scale // invert Y
            })
            .ToList();

        // 4. Compute centroid of scaled shape
        var boundsMinX = transformed.Min(p => p.X);
        var boundsMaxX = transformed.Max(p => p.X);
        var boundsMinY = transformed.Min(p => p.Y);
        var boundsMaxY = transformed.Max(p => p.Y);

        var offsetX = (canvasWidth - (boundsMaxX + boundsMinX)) / 2;
        var offsetY = (canvasHeight - (boundsMaxY + boundsMinY)) / 2;

        // 5. Build SVG path
        var sb = new StringBuilder();
        for (int i = 0; i < transformed.Count; i++)
        {
            var x = transformed[i].X + offsetX;
            var y = transformed[i].Y + offsetY;
            sb.Append(i == 0 ? $"M{x:F1},{y:F1} " : $"L{x:F1},{y:F1} ");
        }

        return sb.ToString().Trim();
    }

}
