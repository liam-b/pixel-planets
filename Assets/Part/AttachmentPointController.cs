using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPointController : MonoBehaviour {
  public Vector2 offset;

  [HideInInspector] public SpriteRenderer spriteRenderer;
  [HideInInspector] public PartController attachedPart;
  [HideInInspector] public PartController parentPart;
  [HideInInspector] public PartMode mode;
  FixedJoint2D joint;

  void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    parentPart = transform.parent.GetComponent<PartController>();
  }

  public virtual void SetMode(PartMode mode) {
    this.mode = mode;

    switch (mode) {
      case PartMode.Build:
        spriteRenderer.enabled = true;
        break;

      default:
        spriteRenderer.enabled = false;
        break;
    }
  }

  public void AttachToPoint(AttachmentPointController attachmentPoint) {
    attachmentPoint.attachedPart = parentPart;
    AttachToPart(attachmentPoint.parentPart);
  }

  public void AttachToPart(PartController part) {
    attachedPart = part;
    var joint = parentPart.gameObject.AddComponent<FixedJoint2D>();
    joint.connectedBody = part.GetComponent<Rigidbody2D>();
  }

  public Vector2 LocalOffsetFromMouse() {
    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    return mousePosition + transform.parent.rotation * transform.localPosition;
  }

  public Vector2 Normal() {
    return transform.rotation * Vector2.up; 
  }
}
