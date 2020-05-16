using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderTest : MonoBehaviour {
  public GameObject partPrefab;
  public float stackSnapDistance;
  public float surfaceSnapDistance;

  PartController currentPart;

  void Update() {
    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePosition.z = 0;

    if (Input.GetKeyDown(KeyCode.Alpha1) && !currentPart) {
      SetAllPartModes(PartMode.Build);
      currentPart = Instantiate(partPrefab, mousePosition, Quaternion.identity).GetComponent<PartController>();
      currentPart.SetMode(PartMode.BuildGhost);
    }

    if (Input.GetKeyDown(KeyCode.R) && currentPart) {
      currentPart.transform.Rotate(Vector3.forward * -90);
    }

    if (Input.GetMouseButtonDown(0) && currentPart) {
      SetAllPartModes(PartMode.Default);
      currentPart = null;
    }

    if (Input.GetKeyDown(KeyCode.Escape) && currentPart) {
      SetAllPartModes(PartMode.Default);
      Destroy(currentPart.gameObject);
      currentPart = null;
    }

    if (currentPart) {
      currentPart.transform.position = mousePosition;

      // var attachment = currentPart.attachmentPoints[0];
      // Vector2 rayOrigin = mousePosition + attachment.transform.localPosition;
      // var hit = Physics2D.Raycast(rayOrigin, currentPart.transform.rotation * attachment.normal, surfaceSnapDistance);

      // var currentPartRotation = currentPart.transform.rotation * Vector2.right;
      // if (hit && Vector2.Dot(hit.normal, currentPartRotation) > 0) {
      //   currentPart.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, hit.normal));
      //   currentPart.transform.position = (Vector3)hit.point + currentPart.transform.rotation * attachment.offset;
      // }
    }

    // if (currentPart) {
    //   currentPart.transform.position = mousePosition;
    //   currentPart.transform.rotation = Quaternion.identity;

    //   var attachment = currentPart.attachmentPoints[0];
    //   Vector2 rayOrigin = mousePosition + attachment.transform.localPosition;
    //   var overlap = Physics2D.OverlapCircle(rayOrigin, surfaceSnapDistance);
    //   if (overlap) {
    //     var hit = Physics2D.Raycast(rayOrigin, overlap.ClosestPoint(rayOrigin) - rayOrigin, surfaceSnapDistance * 2);
    //     if (hit && Vector2.Dot(hit.normal, currentPart.transform.rotation * Vector2.right) > 0) {
    //       var attachmentPointNormal = Quaternion.AngleAxis(attachment.partDirection + 90, Vector3.forward);
    //       currentPart.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, hit.normal));
    //       currentPart.transform.position = (Vector3)hit.point + currentPart.transform.rotation * attachment.offset;
    //     }
    //   }
    // }
  }

  //   if (Input.GetKeyDown(KeyCode.Alpha1) && !currentPart) {
  //     SetAllPartModes(PartMode.Build);
  //     currentPart = Instantiate(partPrefab, mousePosition, Quaternion.identity).GetComponent<PartController>();
  //     currentPart.SetMode(PartMode.BuildGhost);
  //   }

  //   if (Input.GetKeyDown(KeyCode.R) && currentPart) {
  //     currentPart.transform.Rotate(Vector3.forward * -90);
  //   }

  //   if (Input.GetMouseButtonDown(0) && currentPart) {
  //     var closestAttachmentPointPair = FindClosestAttachmentPointPair();
  //     if (closestAttachmentPointPair.Item3 <= 0.25f) {
  //       closestAttachmentPointPair.Item1.AttachToPart(closestAttachmentPointPair.Item2.parentPart);
  //       closestAttachmentPointPair.Item2.AttachToPart(currentPart);
  //     }
      
  //     SetAllPartModes(PartMode.Default);
  //     currentPart = null;
  //   }

  //   if (Input.GetKeyDown(KeyCode.Escape) && currentPart) {
  //     SetAllPartModes(PartMode.Default);
  //     Destroy(currentPart.gameObject);
  //     currentPart = null;
  //   }

  //   if (currentPart) {
  //     currentPart.transform.position = mousePosition;

  //     var closestAttachmentPointPair = FindClosestAttachmentPointPair();
  //     if (closestAttachmentPointPair.Item3 <= 0.25f) {
  //       var attachmentPointOffset = closestAttachmentPointPair.Item1.transform.localPosition + (Vector3)closestAttachmentPointPair.Item1.offset;
  //       currentPart.transform.position = closestAttachmentPointPair.Item2.transform.position - closestAttachmentPointPair.Item1.transform.rotation * attachmentPointOffset;
  //     }
  //   }

  //   if (Input.GetKeyDown(KeyCode.Space)) {
  //     var parts = GetAllParts();
  //     foreach (var part in parts) {
  //       part.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
  //     }
  //   }
  // }

  void SetAllPartModes(PartMode mode) {
    PartController[] parts = FindObjectsOfType<PartController>();
    foreach (var part in parts) {
      part.SetMode(mode);
    }
  }

  // List<PartController> GetAllParts() {
  //   var partList = FindObjectsOfType<PartController>();
  //   var parts = new List<PartController>(partList);
  //   parts.Remove(currentPart);
  //   return parts;
  // }

  // Vector2 GetAttachmentPointOffsetFromMouse(AttachmentPointController attachmentPoint) {
  //   var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  //   return mousePosition + attachmentPoint.transform.rotation * attachmentPoint.transform.localPosition;
  // }

  // (AttachmentPointController, AttachmentPointController, float) FindClosestAttachmentPointPair() {
  //   var parts = GetAllParts();
  //   var minDistance = float.MaxValue;
  //   AttachmentPointController minSourceAttachmentPoint = null;
  //   AttachmentPointController minDestinationAttachmentPoint = null;
  //   foreach (var part in parts) {
  //     foreach (var destinationAttachmentPoint in part.attachmentPoints) {
  //       foreach (var sourceAttachmentPoint in currentPart.attachmentPoints) {
  //         var distance = Vector2.Distance(GetAttachmentPointOffsetFromMouse(sourceAttachmentPoint), destinationAttachmentPoint.transform.position);
  //         // if (distance < minDistance && sourceAttachmentPoint.CanAttachToPoint(destinationAttachmentPoint)) {
  //         if (distance < minDistance) {
  //           minDistance = distance;
  //           minSourceAttachmentPoint = sourceAttachmentPoint;
  //           minDestinationAttachmentPoint = destinationAttachmentPoint;
  //         }
  //       }
  //     }
  //   }

  //   return (minSourceAttachmentPoint, minDestinationAttachmentPoint, minDistance);
  // }
}
