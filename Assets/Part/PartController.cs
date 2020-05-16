using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartMode {
  Default, Build, BuildGhost
}

public class PartController : MonoBehaviour {
  public List<AttachmentPointController> attachmentPoints;
  [HideInInspector] public bool isStackAttachable;

  PartMode mode;
  new Rigidbody2D rigidbody;
  new Collider2D collider;
  SpriteRenderer spriteRenderer;

  public static List<PartController> GetAllParts() {
    var partList = FindObjectsOfType<PartController>();
    return new List<PartController>(partList);
  }

  void Awake() {
    rigidbody = GetComponent<Rigidbody2D>();
    collider = GetComponent<Collider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    isStackAttachable = attachmentPoints.Find(point => point is StackAttachmentPointController);
  }

  void Start() {
    if (mode != PartMode.BuildGhost) SetMode(PartMode.Default);
  }

  public void SetMode(PartMode mode) {
    this.mode = mode;

    switch (mode) {
      case PartMode.Build:
        foreach (var attachmentPoint in attachmentPoints) attachmentPoint.SetMode(mode);
        break;

      case PartMode.BuildGhost:
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.4f);
        spriteRenderer.sortingLayerName = "Ghost";
        collider.enabled = false;
        foreach (var attachmentPoint in attachmentPoints) attachmentPoint.SetMode(mode);
        break;

      default:
        spriteRenderer.color = Color.white;
        spriteRenderer.sortingLayerName = "Default";
        collider.enabled = true;
        foreach (var attachmentPoint in attachmentPoints) attachmentPoint.SetMode(mode);
        break;
    }
  }

  public List<T> GetAttachmentPoints<T>() where T : AttachmentPointController {
    var points = new List<T>();
    foreach (var attachmentPoint in attachmentPoints) {
      if (attachmentPoint is T) points.Add(attachmentPoint as T);
    }
    return points;
  }
}
