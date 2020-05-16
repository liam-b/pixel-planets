using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorRule {
  public enum RelativeToPlane {Absolute, Ground, Bedrock, Max}

  public Color color = Color.black;
  public RelativeToPlane relativeToPlane = RelativeToPlane.Absolute;
  public int relativeRangeMax = 0;
  public int relativeRangeMin = 0;
  
  public float colorVarience = 0;
}
