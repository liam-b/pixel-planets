using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanetFeatures : ScriptableObject {
  public float size = 10;
  public int crustThickness = 5;
  public Color defaultColor = Color.black;
  public NoiseFeatures noiseFeatures;
  public ColorRule[] colorRules;

  // public float GetNoiseValue() {

  // }

  public Color GetTileColorForHeight(int tileHeight, int groundHeight) {
    foreach (ColorRule rule in colorRules) {
      int relativePlane = 0;
      switch (rule.relativeToPlane) {
        case ColorRule.RelativeToPlane.Absolute: {
          relativePlane = (int)size;
          break;
        }
        case ColorRule.RelativeToPlane.Ground: {
          relativePlane = groundHeight + 1;
          break;
        }
        case ColorRule.RelativeToPlane.Bedrock: {
          relativePlane = (int)size - crustThickness;
          break;
        }
        case ColorRule.RelativeToPlane.Max: {
          relativePlane = (int)size + (int)noiseFeatures.strength;
          break;
        }
      }

      // HACK
      tileHeight += Random.Range(-1, 1);

      if (tileHeight >= relativePlane + rule.relativeRangeMin && tileHeight < relativePlane + rule.relativeRangeMax) return rule.color;
    }
    return defaultColor;
  }
}
