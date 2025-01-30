using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardLayout", menuName = "BoardSystem/BoardLayout", order = 1)]
public class BoardLayout : ScriptableObject
{
    [SerializeField] private string JSONUrl;
    [Serializable]
    public class Edge
    {
        public Vector2 point1;
        public Vector2 point2;
        
        public override bool Equals(object obj)
        {
            if (obj is Edge other)
            {
                // Check if edges are identical or reversed (undirected check)
                return (point1.Equals(other.point1) && point2.Equals(other.point2)) ||
                       (point1.Equals(other.point2) && point2.Equals(other.point1));
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Hash code that works for undirected edges
            int hash1 = point1.GetHashCode() ^ point2.GetHashCode();
            int hash2 = point2.GetHashCode() ^ point1.GetHashCode();
            return Mathf.Min(hash1, hash2); // Use the smaller hash for consistency
        }
    }

    public class EdgeList
    {
        [SerializeField] List<Edge> edges;

        public EdgeList(List<Edge> edges)
        {
            this.edges = edges;
        }

        public List<Edge> Edges
        {
            get => edges;
            set => edges = value;
        }
    }

    [SerializeField] List<Edge> edges;

    public List<Edge> Edges
    {
        get => edges;
        set => edges = value;
    }
    
    void Awake()
    {
        // Set file path (StreamingAssets ensures it's accessible)
        // string filePath = Path.Combine(Application.streamingAssetsPath, $"{this.name}.json");
        //
        // EdgeList edgeList = new EdgeList(edges);
        // // Convert the list to a JSON string
        // string json = JsonUtility.ToJson(edgeList, true); // Pretty print for readability
        //
        // // Save the JSON string to a file
        // SaveJsonToFile(json , filePath);
        //
        // Debug.Log($"JSON saved to: {filePath}");
    }

    void SaveJsonToFile(string json, string filePath)
    {
        // Ensure the directory exists
        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Write the JSON to the file
        File.WriteAllText(filePath, json);
    }

    void LoadFromJsonFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        Debug.Log(json);
        EdgeList edgeList = JsonUtility.FromJson<EdgeList>(json);
        this.edges = edgeList.Edges;
    }

    public void RemoveDuplicates()
    {
        // Use a HashSet to remove duplicates efficiently
        HashSet<Edge> uniqueEdges = new HashSet<Edge>(edges);
        edges = new List<Edge>(uniqueEdges);
    }
    public DCEL ConvertToDCEL()
    {
        LoadFromJsonFile(Path.Combine(Application.streamingAssetsPath, $"{this.name}.json"));
        DCEL dcel = new DCEL();
        RemoveDuplicates();
        Dictionary<Vector2, Vertex> vertexLookup = CreateVerticesFromEdges(edges);
        List<HalfEdge> halfEdges = CreateHalfEdgesFromEdges(edges, vertexLookup);
        LinkHalfEdges(halfEdges);

        AssignFaces(halfEdges, dcel);

        // Add everything to the DCEL
        dcel.Vertices = vertexLookup.Values.ToList();
        dcel.Edges = halfEdges;
        Face outsideFace = DetermineOutsideFace(dcel.Faces);
        foreach (var dcelEdge in dcel.Edges)
        {
            if (dcelEdge.Face == outsideFace)
                dcelEdge.Face = null;
        }
        dcel.Faces.Remove(outsideFace);
        foreach (var dcelFace in dcel.Faces)
        {
            dcelFace.Triangulate();
        }
        dcel.SetBoundaries();
        return dcel;
    }

    private Dictionary<Vector2, Vertex> CreateVerticesFromEdges(List<Edge> edges)
    {
        Dictionary<Vector2, Vertex> vertexLookup = new Dictionary<Vector2, Vertex>();

        foreach (Edge edge in edges)
        {
            if (!vertexLookup.ContainsKey(edge.point1))
            {
                vertexLookup[edge.point1] = new Vertex { Position = edge.point1 };
            }

            if (!vertexLookup.ContainsKey(edge.point2))
            {
                vertexLookup[edge.point2] = new Vertex { Position = edge.point2 };
            }
        }

        return vertexLookup;
    }

    private List<HalfEdge> CreateHalfEdgesFromEdges(List<Edge> edges, Dictionary<Vector2, Vertex> vertexLookup)
    {
        List<HalfEdge> halfEdges = new List<HalfEdge>();
        Dictionary<(Vector2, Vector2), HalfEdge> edgeLookup = new Dictionary<(Vector2, Vector2), HalfEdge>();

        foreach (Edge edge in edges)
        {
            Vertex v1 = vertexLookup[edge.point1];
            Vertex v2 = vertexLookup[edge.point2];

            // Create two half-edges
            HalfEdge halfEdge1 = new HalfEdge { Origin = v1 };
            HalfEdge halfEdge2 = new HalfEdge { Origin = v2 };

            // Set twins
            halfEdge1.Twin = halfEdge2;
            halfEdge2.Twin = halfEdge1;

            // Add to edge lookup for later twin assignment
            edgeLookup[(v1.Position, v2.Position)] = halfEdge1;
            edgeLookup[(v2.Position, v1.Position)] = halfEdge2;

            halfEdges.Add(halfEdge1);
            halfEdges.Add(halfEdge2);
        }

        return halfEdges;
    }

    private void LinkHalfEdges(List<HalfEdge> halfEdges)
    {
        // Group edges by origin vertex
        var edgesByOrigin = halfEdges.GroupBy(e => e.Origin).ToDictionary(g => g.Key, g => g.ToList());

        foreach (var group in edgesByOrigin)
        {
            Vertex origin = group.Key;
            List<HalfEdge> outgoingEdges = group.Value;

            // Sort edges in counterclockwise order
            outgoingEdges.Sort((e1, e2) =>
            {
                Vector2 dir1 = e1.Twin.Origin.Position - origin.Position;
                Vector2 dir2 = e2.Twin.Origin.Position - origin.Position;
                float angle1 = Mathf.Atan2(dir1.y, dir1.x);
                float angle2 = Mathf.Atan2(dir2.y, dir2.x);
                return angle1.CompareTo(angle2);
            });

            // Link the sorted edges
            for (int i = 0; i < outgoingEdges.Count; i++)
            {
                HalfEdge current = outgoingEdges[i];
                HalfEdge previous = outgoingEdges[(i + 1) % outgoingEdges.Count]; // Wraps around to form a loop

                current.Prev = previous.Twin;
                previous.Twin.Next = current;
            }
        }
    }

    private void AssignFaces(List<HalfEdge> halfEdges, DCEL dcel)
    {
        HashSet<HalfEdge> visitedEdges = new HashSet<HalfEdge>();

        foreach (HalfEdge edge in halfEdges)
        {
            // If the edge already has a face, skip it
            if (visitedEdges.Contains(edge) || edge.Face != null)
                continue;

            // Create a new face
            Face face = new Face();
            dcel.Faces.Add(face);

            // Traverse the loop of edges to assign the face
            HalfEdge current = edge;
            do
            {
                current.Face = face;
                face.OuterEdge = current;
                visitedEdges.Add(current);

                current = current.Next;
            } while (current != edge);
        }
    }

    public Face DetermineOutsideFace(List<Face> faces)
    {
        Face outsideFace = null;
        float maxBoundingArea = float.MinValue;

        // Iterate through all faces
        foreach (var face in faces)
        {
            if (face.OuterEdge == null)
            {
                // Skip faces without outer components (shouldn't happen in a valid DCEL)
                continue;
            }

            // Step 1: Compute the bounding area of the face
            float boundingArea = face.ComputeFaceBoundingArea();

            // Step 2: Check if this is the largest bounding area
            if (boundingArea > maxBoundingArea)
            {
                maxBoundingArea = boundingArea;
                outsideFace = face;
            }
        }


        return outsideFace;
    }
}