using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAttachmentPointController : AttachmentPointController {
  public override void SetMode(PartMode mode) {
    this.mode = mode;

    switch (mode) {
      case PartMode.Build:
        spriteRenderer.enabled = false;
        break;

      case PartMode.BuildGhost:
        // spriteRenderer.color = new Color(1f, 1f, 1f, 0.4f);
        spriteRenderer.sortingLayerName = "Ghost Attachment Points";
        if (!attachedPart) spriteRenderer.enabled = true;
        break;

      default:
        spriteRenderer.color = Color.white;
        spriteRenderer.sortingLayerName = "Attachment Points";
        spriteRenderer.enabled = false;
        break;
    }
  }
}
