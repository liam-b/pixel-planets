using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour {
  Mesh mesh;
  List<Vector3> vertices;
  List<int> triangles;
  List<Color> colors;

  PlanetController planet;
  float tileColumn;

  public void Init(int index) {
    ResetMesh();
    
    int startColumn = planet.columnsPerChunk * index;
    int endColumn = planet.columnsPerChunk * (index + 1);
    endColumn = Mathf.Min(endColumn, planet.tileColumns);

    for (float i = startColumn; i < endColumn; i++) {
      float angle = (i / planet.tileColumns) * Mathf.PI * 2f;
      float noise = NoiseFilter.Evaluate(planet.features.noiseFeatures, new Vector2(Mathf.Cos(angle) * planet.radius, Mathf.Sin(angle) * planet.radius));
      int distance = Mathf.RoundToInt(planet.radius + noise);
      
      for (int j = distance; j > planet.radius - planet.features.crustThickness; j--) {
        Vector2 position = new Vector2(Mathf.Cos(angle) * j, Mathf.Sin(angle) * j);
        Color color = planet.features.GetTileColorForHeight(j, distance);
        AddQuadToMesh(position, angle, color);
      }
    }

    UpdateMesh();
  }

  void ResetMesh() {
    mesh = new Mesh();
    GetComponent<MeshFilter>().mesh = mesh;
    planet = transform.parent.GetComponent<PlanetController>();

    vertices = new List<Vector3>();
    triangles = new List<int>();
    colors = new List<Color>();
  }

  void UpdateMesh() {
    mesh.Clear();
    mesh.SetVertices(vertices);
    mesh.SetTriangles(triangles, 0);
    mesh.SetColors(colors);
  }

  void AddQuadToMesh(Vector2 position, float rotation, Color color) {
    float distance = Mathf.Sqrt(2) / 2f;
    float rotationOffset = -Mathf.PI / 4f;
    int index = vertices.Count;

    vertices.AddRange(new Vector3[]{
      RotateAroundPoint(position, rotation + rotationOffset, distance),
      RotateAroundPoint(position, rotation + Mathf.PI * (1f/2f) + rotationOffset, distance),
      RotateAroundPoint(position, rotation + Mathf.PI * (2f/2f) + rotationOffset, distance),
      RotateAroundPoint(position, rotation + Mathf.PI * (3f/2f) + rotationOffset, distance)
    });

    triangles.AddRange(new int[]{
      index, index + 2, index + 1,
      index, index + 3, index + 2
    });

    for (int i = index; i < vertices.Count; i++) {
      colors.Add(color);
    }
  }

  public static Vector2 RotateAroundPoint(Vector2 point, float angle, float distance) {
    return new Vector2((float)point.x + Mathf.Cos(angle) * distance, (float)point.y + Mathf.Sin(angle) * distance);
  }
}
