using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter {
  public static float Evaluate(NoiseFeatures features, Vector2 position) {
    float noise = 0;
    float amplitude = features.strength;
    float frequency = features.roughness;

    for (int i = 0; i < features.octaves; i++) {
      float octaveNoise = Mathf.PerlinNoise(position.x * frequency + features.seed.x, position.y * frequency + features.seed.y);
      noise += octaveNoise * amplitude;
      amplitude *= features.strengthPersistence;
      frequency *= features.roughnessPersistence;
    }

    return noise - features.strength / 1f;
  }
}
