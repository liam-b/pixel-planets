using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPartPlayerState : StateMachineBehaviour {
  public float stackSnappingDistance;
  public float surfaceSnappingDistance;

  BuildingPlayerState buildingState;

  void OnStateEnter(Animator animator) {
    if (!buildingState) buildingState = animator.GetBehaviour<BuildingPlayerState>();

    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    buildingState.placingPart = Instantiate(buildingState.selectedPart, mousePosition, buildingState.placementRotation).GetComponent<PartController>();

    PartController.GetAllParts().ForEach(part => part.SetMode(PartMode.Build));
    buildingState.placingPart.SetMode(PartMode.BuildGhost);
  }

  void OnStateUpdate(Animator animator) {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    if (buildingState.placingPart) {
      if (Input.GetKeyDown(KeyCode.R)) buildingState.placementRotation *= Quaternion.Euler(0, 0, -90);
      buildingState.placingPart.transform.position = mousePosition;
      buildingState.placingPart.transform.rotation = buildingState.placementRotation;

      var (attachmentPoint, attachmentPart) = buildingState.placingPart.isStackAttachable ? SnapPartToStack() : SnapPartToSurface();

      if (attachmentPart && Input.GetMouseButtonDown(0)) {
        attachmentPoint.AttachToPart(attachmentPart);

        buildingState.placingPart.SetMode(PartMode.Default);
        buildingState.placingPart = null;
        animator.SetTrigger("DeselectPart");
      }
    }

    if (Input.GetKeyDown(KeyCode.Escape)) animator.SetTrigger("DeselectPart");
    if (Input.GetKeyDown(KeyCode.Tab)) animator.SetTrigger("ExitBuilding");
  }

  void OnStateExit(Animator animator) {
    PartController.GetAllParts().ForEach(part => part.SetMode(PartMode.Default));
    if (buildingState.placingPart) Destroy(buildingState.placingPart.gameObject);
    buildingState.placingPart = null;
  }

  (AttachmentPointController, PartController) SnapPartToStack() {
    var (sourcePoint, destinationPoint, distance) = FindClosestAttachmentPointPair();
    if (distance > stackSnappingDistance) return (null, null);

    var attachmentPointOffset = Abs(sourcePoint.transform.localPosition + (Vector3)sourcePoint.offset);
    buildingState.placingPart.transform.position = destinationPoint.transform.position + destinationPoint.transform.rotation * attachmentPointOffset;

    var currentRotation = buildingState.placingPart.transform.rotation.eulerAngles.z;
    var targetRotation = destinationPoint.transform.parent.rotation.eulerAngles.z;
    var rotationDifference = (targetRotation - currentRotation) % 180;
    buildingState.placingPart.transform.rotation *= Quaternion.Euler(0, 0, rotationDifference);

    return (destinationPoint, buildingState.placingPart);
  }

  (AttachmentPointController, PartController) SnapPartToSurface() {
    var surfaceAttachmentPoints = buildingState.placingPart.GetAttachmentPoints<SurfaceAttachmentPointController>();

    float closestAttachmentDistance = float.MaxValue;
    RaycastHit2D closestRaycastHit = new RaycastHit2D();
    AttachmentPointController closestAttachmentPoint = null;
    foreach (var attachmentPoint in surfaceAttachmentPoints) {
      Vector2 rayOrigin = attachmentPoint.LocalOffsetFromMouse();
      var hit = Physics2D.Raycast(rayOrigin, attachmentPoint.Normal(), surfaceSnappingDistance);

      if (hit && hit.distance < closestAttachmentDistance) {
        closestAttachmentDistance = hit.distance;
        closestRaycastHit = hit;
        closestAttachmentPoint = attachmentPoint;
      }
    }

    var currentPartRotation = buildingState.placingPart.transform.rotation * Vector2.right;
    if (!closestRaycastHit || Vector2.Dot(closestRaycastHit.normal, currentPartRotation) < 0) return (null, null);

    buildingState.placingPart.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, closestRaycastHit.normal));
    buildingState.placingPart.transform.position = (Vector3)closestRaycastHit.point + buildingState.placingPart.transform.rotation * closestAttachmentPoint.offset;

    return (closestAttachmentPoint, closestRaycastHit.collider.GetComponent<PartController>());
  }

  (AttachmentPointController, AttachmentPointController, float) FindClosestAttachmentPointPair() {
    var parts = PartController.GetAllParts();
    parts.Remove(buildingState.placingPart);

    var minDistance = float.MaxValue;
    AttachmentPointController minSourceAttachmentPoint = null;
    AttachmentPointController minDestinationAttachmentPoint = null;
    foreach (var part in parts) {
      foreach (var destinationAttachmentPoint in part.GetAttachmentPoints<StackAttachmentPointController>()) {
        foreach (var sourceAttachmentPoint in buildingState.placingPart.GetAttachmentPoints<StackAttachmentPointController>()) {
          var distance = Vector2.Distance(sourceAttachmentPoint.LocalOffsetFromMouse(), destinationAttachmentPoint.transform.position);
          if (distance < minDistance && sourceAttachmentPoint.CanStackOnAttachmentPoint(destinationAttachmentPoint)) {
            minDistance = distance;
            minSourceAttachmentPoint = sourceAttachmentPoint;
            minDestinationAttachmentPoint = destinationAttachmentPoint;
          }
        }
      }
    }

    return (minSourceAttachmentPoint, minDestinationAttachmentPoint, minDistance);
  }

  Vector2 Abs(Vector2 vector) {
    return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
  }
}
