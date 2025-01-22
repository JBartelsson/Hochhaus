using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = Unity.Mathematics.Random;

public class BoardFieldVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
        [SerializeField] MeshRenderer faceRenderer;
        [SerializeField] MeshFilter meshFilter;
        [SerializeField] MeshCollider meshCollider;
        private float scale = 4f;


        [Header("Hover Effect")]
        [SerializeField] Material _hoverMaterial;
        private BoardField _boardField;
        private Material _originalMaterial;
        public float Scale
        {
            get => scale;
            set => scale = value;
        }

        public BoardField BoardField => _boardField;
        private void Start()
        {
            _originalMaterial = faceRenderer.material;
        }

        public void Init(BoardField boardField, float scale)
        {
            this.scale = scale;
            this._boardField = boardField;
            CreateMeshFromFace(_boardField.DCELFace);
            _boardField.OnUpdate += BoardFieldOnOnUpdate;
        }

        private void BoardFieldOnOnUpdate(object sender, EventArgs e)
        {
            if (_boardField.paintStack.Count == 0) return;
            faceRenderer.material.color = _boardField.paintStack.Last().CardCopy.ColorReference.RGBColor;
        }
        
        public void CreateMeshFromFace(Face face, Material material = null)
        {
            // Step 1: Get the vertices of the face
            List<Vertex> vertices = new List<Vertex>();
            List<int> triangles = new List<int>();

            vertices = face.GetVerticesFromFace();
            // Step 2: Triangulate the face
            // Use a simple fan triangulation method (works for convex polygons)
            for (int i = 1; i < vertices.Count - 1; i++)
            {
                triangles.Add(0);     // First vertex (center of the fan)
                triangles.Add(i);     // Current vertex
                triangles.Add(i + 1); // Next vertex
            }

            // Step 3: Create a new GameObject

            // Add MeshFilter and MeshRenderer components

            // Step 4: Create the mesh
            Mesh mesh = new Mesh();
            mesh.vertices = vertices.Select(v => new Vector3(v.Position.x, 0, v.Position.y) * scale).ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals(); // Calculate normals for lighting
            meshCollider.sharedMesh = mesh;
            // Assign the mesh to the MeshFilter
            meshFilter.mesh = mesh;
        }

        public void AddCardColorToFace(Card card)
        {
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // faceRenderer.material = _hoverMaterial;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // faceRenderer.material = _originalMaterial;
        }

        
}