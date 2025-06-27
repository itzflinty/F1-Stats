using services;
using models;
using System.Diagnostics;

/// <summary>
/// Entry point for the F1 Circuit Viewer.
/// Prompts the user for a search term, loads the circuit GeoJSON data,
/// finds matching circuits, and generates SVG previews for each.
/// </summary>
class Program
{
    static void Main()
    {
        // Prompt the user for a circuit search term
        Console.WriteLine("🏁 Enter a circuit name to search (e.g. Albert, Silverstone):");
        var input = Console.ReadLine()?.Trim();
        Console.WriteLine($"📝 User input: '{input}'");

        // Validate input
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("⚠️ Please enter a valid location.");
            return;
        }

        // Determine the project root and construct the path to the GeoJSON file
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName!;
        string geoJsonPath = Path.Combine(projectRoot, "data", "f1-circuits", "f1-circuits.geojson");

        // Parse the GeoJSON file and find relevant circuits
        var parser = new GeoJsonParser(geoJsonPath);
        List<Feature> results = parser.FindCircuitsByName(input);

        // Handle case with no matching circuits
        if (results.Count == 0)
        {
            Console.WriteLine($"❌ No circuits found for '{input}'.");
            return;
        }

        // Generate SVG and auto-launch for each match
        foreach (var feature in results)
        {
            // Render circuit as SVG markup
            var svg = SvgCircuitRenderer.RenderSvg(feature);

            // Sanitize file name for safe file system output
            var safeName = feature.Properties?.Name?
                .Replace(" ", "_")
                .Replace("/", "-") ?? "circuit";

            // Save SVG to disk
            File.WriteAllText($"{safeName}.svg", svg);

            // Get absolute path and open in default system viewer
            var fullPath = Path.GetFullPath($"{safeName}.svg");
            Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
        }
    }
}
