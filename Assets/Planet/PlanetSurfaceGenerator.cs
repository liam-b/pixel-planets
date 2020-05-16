using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlanetSurfaceGenerator : MonoBehaviour {
  // static float SQRT_2 = Mathf.Sqrt(2);
  // const float radius = 50f;
  // const float noiseRadius = 5f;
  // // const float innerRadius = 40f;

  // List<Vector3> vertices = new List<Vector3>();
  // List<int> triangles = new List<int>();
  // Mesh mesh;

  // void Start() {
  //   mesh = new Mesh();
  //   GetComponent<MeshFilter>().mesh = mesh;

  //   int circumference = (int)(Mathf.PI * 2f * radius);
  //   Debug.Log((Mathf.PI * 2f / (float)circumference));
  //   for (float a = 0; a < Mathf.PI * 2f; a += Mathf.PI * 2f / (float)circumference) {
  //     float distance = NoiseFilter.Evaluate(features, new Vector2(Mathf.Cos(a) * noiseRadius + 100, Mathf.Sin(a) * noiseRadius + 100));
  //     float noise = Mathf.PerlinNoise(Mathf.Cos(a), Mathf.Sin(a));
  //     Vector2 position = new Vector2(Mathf.Cos(a) * (radius + noise), Mathf.Sin(a) * (radius + noise));
  //     AddQuad(position, a);
  //   }

  //   mesh.Clear();
  //   mesh.SetVertices(vertices);
  //   mesh.SetTriangles(triangles, 0);
  // }

  // void AddQuad(Vector2 position, float rotation) {
  //   int index = vertices.Count;
  //   vertices.AddRange(new Vector3[]{
  //     RotateAroundPoint(position, rotation - Mathf.PI / 4f, SQRT_2 / 2f),
  //     RotateAroundPoint(position, rotation + Mathf.PI * (1f/2f) - Mathf.PI / 4f, SQRT_2 / 2f),
  //     RotateAroundPoint(position, rotation + Mathf.PI * (2f/2f) - Mathf.PI / 4f, SQRT_2 / 2f),
  //     RotateAroundPoint(position, rotation + Mathf.PI * (3f/2f) - Mathf.PI / 4f, SQRT_2 / 2f)
  //   });

  //   triangles.AddRange(new int[]{
  //     index, index + 2, index + 1,
  //     index, index + 3, index + 2
  //   });
  // }

  // Vector2 RotateAroundPoint(Vector2 point, float angle, float distance) {
  //   return new Vector2((float)point.x + Mathf.Cos(angle) * distance, (float)point.y + Mathf.Sin(angle) * distance);
  // }
}