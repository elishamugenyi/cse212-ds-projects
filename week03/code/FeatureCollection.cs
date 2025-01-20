public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    public string Type { get; set; }
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public string Type { get; set; }
    public Properties Properties { get; set; }
    public Geometry Geometry { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double? Mag { get; set; }
}

public class Geometry
{
    public string Type { get; set; }
    public double[] Coordinates { get; set; }
}