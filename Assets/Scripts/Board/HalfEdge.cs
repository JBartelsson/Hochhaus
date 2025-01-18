public class HalfEdge
{
    public Vertex Origin;             // Starting vertex of the edge
    public HalfEdge Twin;             // Opposite half-edge
    public HalfEdge Next;             // Next half-edge in the face
    public HalfEdge Prev;             // Previous half-edge in the face
    public Face Face;                 // Face to which this half-edge belongs

    public override string ToString()
    {
        return $"{Origin}, {Twin?.ReturnVertexString()}, {Prev?.ReturnVertexString()}, {Next?.ReturnVertexString()} {Face}";
    }

    public string ReturnVertexString()
    {
        return $"{Origin}, {Twin.Origin}";
    }
} 