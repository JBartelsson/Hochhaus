using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DCEL
{
    public List<Vertex> Vertices = new List<Vertex>();
    public List<HalfEdge> Edges = new List<HalfEdge>();
    public List<Face> Faces = new List<Face>();

    public float MinX;
    public float MinY;
    public float MaxX;
    public float MaxY;

    public void SetBoundaries()
    {
        // Find min/max X and Y coordinates
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var face in this.Faces)
        {
            foreach (var edge in face.GetEdgesFromFace())
            {
                Vector2 pos = edge.Origin.Position;
                minX = Mathf.Min(minX, pos.x);
                maxX = Mathf.Max(maxX, pos.x);
                minY = Mathf.Min(minY, pos.y);
                maxY = Mathf.Max(maxY, pos.y);
            }
        }
        
        MinX = minX;
        MinY = minY;
        MaxX = maxX;
        MaxY = maxY;
    }

    public Dictionary<AreaPosition, List<Face>> GetHalfs()
    {
        List<List<DCEL>> result = new List<List<DCEL>>();
        HashSet<Face> leftFaces = new ();
        HashSet<Face> rightFaces = new ();
        HashSet<Face> topFaces = new ();
        HashSet<Face> bottomFaces = new ();
        
        float halfY = (MaxY - MinY) / 2.0f;
        float halfX = (MaxX - MinX) / 2.0f;
        foreach (var face in Faces)
        {
            foreach (var vertex in face.GetVerticesFromFace())
            {
                if (vertex.Position.x > halfX)
                {
                    rightFaces.Add(face);
                }
                else
                {
                    leftFaces.Add(face);
                }

                if (vertex.Position.y > halfY)
                {
                    topFaces.Add(face);
                }
                else
                {
                    bottomFaces.Add(face);
                }
            }
        }

        return new Dictionary<AreaPosition, List<Face>>()
        {
            { AreaPosition.TopArea, topFaces.ToList() },
            { AreaPosition.BottomArea, bottomFaces.ToList() },
            { AreaPosition.LeftArea, leftFaces.ToList() },
            { AreaPosition.RightArea, rightFaces.ToList() },
        };
    }
    public List<List<Face>> FindConnectedFaceGroups(Func<Face, bool> filter)
    {
        List<List<Face>> groups = new List<List<Face>>();

        foreach (var face in Faces)
        {
            if (face.IsVisited || !filter(face))
                continue;

            // Start a new group
            List<Face> group = new List<Face>();
            Queue<Face> queue = new Queue<Face>();

            queue.Enqueue(face);
            face.IsVisited = true;

            while (queue.Count > 0)
            {
                Face current = queue.Dequeue();
                group.Add(current);

                foreach (var edge in current.GetEdgesFromFace())
                {
                    HalfEdge twin = edge.Twin;
                    if (twin != null && twin.Face != null)
                    {
                        Face neighbor = twin.Face;

                        if (!neighbor.IsVisited && filter(neighbor))
                        {
                            neighbor.IsVisited = true;
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            groups.Add(group);
        }

        // Reset visited state (important if you call the function multiple times)
        foreach (var face in Faces)
            face.IsVisited = false;

        return groups;
    }
    
    public Dictionary<CanvasEdge, List<Face>> GetBoundaryFaces()
    {
        HashSet<Face> topFaces = new HashSet<Face>();
        HashSet<Face> bottomFaces = new HashSet<Face>();
        HashSet<Face> leftFaces = new HashSet<Face>();
        HashSet<Face> rightFaces = new HashSet<Face>();

        

        // Identify boundary faces
        foreach (var face in this.Faces)
        {
            foreach (var edge in face.GetEdgesFromFace())
            {
                Vector2 pos = edge.Origin.Position;

                if (Mathf.Approximately(pos.y, MaxY)) topFaces.Add(face);
                if (Mathf.Approximately(pos.y, MinY)) bottomFaces.Add(face);
                if (Mathf.Approximately(pos.x, MinX)) leftFaces.Add(face);
                if (Mathf.Approximately(pos.x, MaxX)) rightFaces.Add(face);
            }
        }

        return new Dictionary<CanvasEdge, List<Face>>()
        {
            { CanvasEdge.Top, topFaces.ToList() },
            { CanvasEdge.Bottom, bottomFaces.ToList() },
            { CanvasEdge.Left, leftFaces.ToList() },
            { CanvasEdge.Right, rightFaces.ToList() }
        };
    }

    public enum CanvasEdge
    {
        Top, Bottom, Left, Right
    }
    
    public enum AreaPosition
    {
        TopArea, BottomArea, LeftArea, RightArea
    }
}