using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAttachmentPointController : AttachmentPointController {
  public bool CanStackOnAttachmentPoint(StackAttachmentPointController attachmentPoint) {
    return Vector2.Dot(Normal(), attachmentPoint.Normal()) <= -0.5f && !attachedPart && !attachmentPoint.attachedPart;
  }
}
