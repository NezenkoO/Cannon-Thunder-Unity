using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ShellMeshGenerator : MonoBehaviour
{
    [SerializeField] private float _randomnessFactor = 0.1f;

    private readonly Vector3[] _baseVertices = {
        new Vector3 (0, 0, 0),
        new Vector3 (1, 0, 0),
        new Vector3 (1, 1, 0),
        new Vector3 (0, 1, 0),
        new Vector3 (0, 1, 1),
        new Vector3 (1, 1, 1),
        new Vector3 (1, 0, 1),
        new Vector3 (0, 0, 1),
    };

    private readonly int[] _baseTriangles = {
        0, 2, 1, 0, 3, 2,
        2, 3, 4, 2, 4, 5,
        1, 2, 5, 1, 5, 6,
        0, 7, 4, 0, 4, 3,   
        5, 4, 7, 5, 7, 6,   
        0, 6, 7, 0, 1, 6
    };

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Vector3[] vertices = GenerateVertices();
        int[] triangles = _baseTriangles;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    private Vector3[] GenerateVertices()
    {
        Vector3[] vertices = new Vector3[_baseVertices.Length];
        for (int i = 0; i < _baseVertices.Length; i++)
        {
            vertices[i] = _baseVertices[i] + Random.insideUnitSphere * _randomnessFactor;
        }
        return vertices;
    }
}
