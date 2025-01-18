using UnityEngine;

public class Vertex
{
    public Vector2 Position;          // Coordinate of the point
    public HalfEdge IncidentEdge;     // One of the outgoing half-edges

    public override string ToString()
    {
        return Position.ToString();
    }
}