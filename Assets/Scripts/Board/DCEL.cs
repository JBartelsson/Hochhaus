using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCEL
{
    public List<Vertex> Vertices = new List<Vertex>();
    public List<HalfEdge> Edges = new List<HalfEdge>();
    public List<Face> Faces = new List<Face>();
}