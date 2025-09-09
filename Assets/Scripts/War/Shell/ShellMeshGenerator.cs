using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ShellMeshGenerator : MonoBehaviour
{
    [SerializeField] private float _randomnessFactor = 0.1f;

    private static readonly Vector3[] _baseVertices = {
        new(0,0,0), new(1,0,0), new(1,1,0), new(0,1,0),
        new(0,1,1), new(1,1,1), new(1,0,1), new(0,0,1)
    };

    private static readonly int[] _baseTriangles = {
        0,2,1, 0,3,2, 2,3,4, 2,4,5,
        1,2,5, 1,5,6, 0,7,4, 0,4,3,
        5,4,7, 5,7,6, 0,6,7, 0,1,6
    };

    private void Start() => 
        Generate();

    private void Generate()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = GenerateVertices();
        mesh.triangles = _baseTriangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private Vector3[] GenerateVertices()
    {
        var vertices = new Vector3[_baseVertices.Length];
        
        for (var i = 0; i < vertices.Length; i++)
            vertices[i] = _baseVertices[i] + Random.insideUnitSphere * _randomnessFactor;
        
        return vertices;
    }
}