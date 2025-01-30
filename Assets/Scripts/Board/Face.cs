using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

public class Face
{
    public HalfEdge OuterEdge; // One of the half-edges on the boundary
    public List<List<Vector2>> triangles;
    public BoardField BoardField;
    
    //used for traversal
    public bool IsVisited;

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

    public bool IsPointInFace(Vector2 point)
    {
        foreach (var triangle in triangles)
        {
            if (IsPointInTriangle(point, triangle[0], triangle[1], triangle[2]))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsPointInTriangle(Vector2 point, Vector2 v1, Vector2 v2, Vector2 v3)
    {
        
        // Compute vectors
        Vector2 v0 = v3 - v1;
        Vector2 v1to2 = v2 - v1;
        Vector2 v1toPoint = point - v1;

        // Compute dot products
        float dot00 = Vector2.Dot(v0, v0);
        float dot01 = Vector2.Dot(v0, v1to2);
        float dot02 = Vector2.Dot(v0, v1toPoint);
        float dot11 = Vector2.Dot(v1to2, v1to2);
        float dot12 = Vector2.Dot(v1to2, v1toPoint);

        // Compute barycentric coordinates
        float denom = dot00 * dot11 - dot01 * dot01;
        if (Mathf.Abs(denom) < Mathf.Epsilon)
        {
            // Triangle is degenerate (area is zero)
            return false;
        }

        float invDenom = 1.0f / denom;
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        // Check if point is in triangle
        return (u >= 0) && (v >= 0) && (u + v <= 1);
    }

    public void Triangulate()
    {
        triangles = new List<List<Vector2>>();
        List<Vector2> vertices = GetOriginsFromFace();
        Debug.Log(vertices.ToFormattedString());
        int escape = 1000;
        int i = 0;
        while (vertices.Count >= 3 && escape > 0)
        {
            Vector2 currentVertex = vertices[(int)MathUtility.nfmod((i), vertices.Count)];
            Vector2 prevVertex = vertices[(int)MathUtility.nfmod((i - 1), vertices.Count)];
            Vector2 nextVertex = vertices[(int)MathUtility.nfmod((i + 1), vertices.Count)];
            float angle = Vector2.SignedAngle(currentVertex - prevVertex, nextVertex - currentVertex);
            //Check if current vertex is reflex vertex, meaning it's going inside
            escape--;

            if (angle < 0)
            {
                i++;
                Debug.Log($"Point is Reflex Point");
                continue;
            }

            bool pointInTriangle = false;
            foreach (var vector2 in vertices)
            {
                if (vector2 == prevVertex || vector2 == currentVertex || vector2 == nextVertex) continue;
                if (IsPointInTriangle(vector2, prevVertex, currentVertex, nextVertex))
                {
                    pointInTriangle = true;
                    Debug.Log($"{vector2} is inside {prevVertex}, {currentVertex}, {nextVertex}");

                }
            }

            if (pointInTriangle)
            {
                i++;
                continue;
            }

            triangles.Add(new List<Vector2>() { prevVertex, currentVertex, nextVertex });
            Debug.Log($"Found Triangle {triangles.Last().ToFormattedString()}");
            vertices.Remove(currentVertex);


        }
        
        if (vertices.Count == 3)
        {
            triangles.Add(new List<Vector2>() { vertices[0], vertices[1], vertices[2] });
            Debug.Log($"Found Triangle {triangles.Last().ToFormattedString()}");

        }
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

    public override string ToString()
    {
        if (BoardField.TopPaintedCard == null) return "Canvas White";
        return $"Canvas {BoardField.TopPaintedCard.CardCopy.ColorReference.ToString()}";
    }
}