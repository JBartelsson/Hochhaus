using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Face
{
    public HalfEdge OuterEdge;  // One of the half-edges on the boundary
    
    public List<HalfEdge> GetEdgesFromFace()
    {
        List<HalfEdge> edges = new List<HalfEdge>();

        if (this.OuterEdge == null)
            return edges; // Empty face (no edges)

        HalfEdge startEdge = this.OuterEdge;
        HalfEdge currentEdge = startEdge;

        do
        {
            edges.Add(currentEdge);
            currentEdge = currentEdge.Next; // Move to the next edge in the loop
        } while (currentEdge != startEdge); // Stop when we complete the loop

        return edges;
    }
    
    public List<Vertex> GetVerticesFromFace()
    {
        List<Vertex> vertices = new List<Vertex>();

        if (this.OuterEdge == null)
            return vertices; // Empty face (no vertices)

        HalfEdge startEdge = this.OuterEdge;
        HalfEdge currentEdge = startEdge;

        do
        {
            vertices.Add(currentEdge.Origin); // Collect the vertex position
            currentEdge = currentEdge.Next; // Move to the next edge in the loop
        } while (currentEdge != startEdge); // Stop when we complete the loop

        return vertices;
    }

    public List<Vector2> GetOriginsFromFace()
    {
        return GetVerticesFromFace().Select(v => v.Position).ToList();
    }
    
    // Helper function to compute the bounding area of a face
    public float ComputeFaceBoundingArea()
    {
        List<Vector2> vertices = GetOriginsFromFace();

        // Use a simple shoelace formula for polygon area
        float area = 0f;
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector2 current = vertices[i];
            Vector2 next = vertices[(i + 1) % vertices.Count]; // Wrap around to the first vertex

            area += (current.x * next.y) - (next.x * current.y);
        }

        return Mathf.Abs(area) * 0.5f; // Return the absolute value of the area
    }
}