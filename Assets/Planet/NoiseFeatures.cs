using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseFeatures {
  public float strength = 1;
  public float strengthPersistence = 1;
	public float roughness = 1;
  public float roughnessPersistence = 1;
  public int octaves = 1;

	public Vector2 seed = Vector2.zero;
}
