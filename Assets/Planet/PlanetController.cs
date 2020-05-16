using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {
  private static int MAX_MESH_VERTICES = 65534;
  private static float chunkLoadDistance = 50f;

  public PlanetFeatures features;
  public GameObject chunkPrefab;
  public ChunkController[] chunks;
  public ObjectPooler chunkObjectPool;

  [HideInInspector] public int tileColumns;
  [HideInInspector] public int columnsPerChunk;
  [HideInInspector] public float radius;

  void Start() {
    int maxTilesPerChunk = MAX_MESH_VERTICES / 4;
    columnsPerChunk = maxTilesPerChunk / (features.crustThickness + Mathf.CeilToInt(features.noiseFeatures.strength));
    tileColumns = Mathf.FloorToInt(Mathf.PI * 2f * features.size);
    radius = tileColumns / (Mathf.PI * 2f);

    int numChunks = Mathf.CeilToInt((float)tileColumns / (float)columnsPerChunk);
    chunks = new ChunkController[numChunks];
    chunkObjectPool = new ObjectPooler(chunkPrefab);

    // Transform body = transform.Find("Body");
		// body.transform.localScale = new Vector2(radius - features.crustThickness, radius - features.crustThickness) * 2f;
    // body.GetComponent<SpriteRenderer>().color = features.defaultColor;
  }

  void Update() {
    for (int chunkIndex = 0; chunkIndex < chunks.Length; chunkIndex++) {
      bool shouldChunkBeLoaded = IsChunkInLoadDistance(chunkIndex);

      if (!chunks[chunkIndex] && shouldChunkBeLoaded) {
        LoadChunk(chunkIndex);
      }

      if (chunks[chunkIndex] && !shouldChunkBeLoaded) {
        UnloadChunk(chunkIndex);
      }
    }
  }

  bool IsChunkInLoadDistance(int chunkIndex) {
    float firstTileAngle = (float)chunkIndex / (float)chunks.Length * Mathf.PI * 2f;
    Vector2 firstTilePosition = ChunkController.RotateAroundPoint(transform.position, firstTileAngle, radius);
    return Vector2.Distance(transform.TransformPoint(firstTilePosition), Camera.main.transform.position) <= chunkLoadDistance;
  }

  void LoadChunk(int chunkIndex) {
    ChunkController newChunk = chunkObjectPool.Spawn(transform.position, Quaternion.identity, transform).GetComponent<ChunkController>();
    newChunk.Init(chunkIndex);
    chunks[chunkIndex] = newChunk;
  }

  void UnloadChunk(int chunkIndex) {
    chunkObjectPool.Despawn(chunks[chunkIndex].gameObject);
    chunks[chunkIndex] = null;
  }
}
