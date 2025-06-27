# F1 Circuit Viewer

**Last updated:** 27 June 2025

F1 Circuit Viewer is a C#-based application that allows users to explore, analyze, and visualize Formula 1 circuit data from a GeoJSON source. The tool supports circuit search by name, renders SVG outlines of circuit geometries, and generates local previews for reference or integration into other platforms.

## Features

- **Circuit Search:** Accepts partial or full names to identify matching F1 circuits from the dataset.
- **GeoJSON Parsing:** Supports both `LineString` and `MultiLineString` types via a custom `GeometryConverter`.
- **SVG Rendering:**
  - Generates scalable vector representations of track layouts.
  - Tracks are centered and scaled into a configurable viewport.
  - Transparent background with optional text labeling.
- **Modular Architecture:**
  - `GeoJsonParser`: Handles file loading and circuit search.
  - `CircuitOutlineAnalyzer`: Summarizes geometry type and point structure.
  - `SvgCircuitRenderer`: Converts coordinate data into a viewable SVG.
- **Output Automation:**
  - Automatically exports `.svg` files for each matching circuit.
  - Launches rendered previews using the system's default viewer.
- **Fully Commented Codebase:** All classes are documented for clarity and extensibility.

## Data Source

Circuit geometry and metadata are sourced from the [f1-circuits](https://github.com/f1mv/f1-circuits) repository on GitHub, which provides open GeoJSON data for Formula 1 tracks worldwide.

## Recent Development (27 June 2025)

- Integrated `GeometryConverter` to support dynamic shape deserialization.
- Refactored circuit processing into reusable components.
- Implemented SVG export with customizable viewport and styling.
- Enabled automatic preview of rendered circuits.
- Removed background styling from SVG for native use in transparent overlays.
- Documented all core components with XML-style comments for maintainability.

## Future Enhancements

- Integrate into mobile applications using platform-native widgets or rendering pipelines.
- Implement fuzzy matching or suggestion fallback for improved search UX.
- Extend metadata presentation with circuit stats or map overlays.
- Optimize export for theme-specific outlines and resolution scaling.
