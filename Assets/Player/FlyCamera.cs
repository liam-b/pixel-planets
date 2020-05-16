using UnityEngine;
using System.Collections;
 
public class FlyCamera : MonoBehaviour {
  public float movementSpeed = 0.05f;
  public float zoomSensitivity = 0.5f;

  void LateUpdate() {
    float zoom = Camera.main.orthographicSize;

    Vector2 movementVector = GetKeyboardMovement();
    transform.Translate(movementVector * movementSpeed * Mathf.Pow(zoom, 0.2f) * Camera.main.orthographicSize);

    float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
    Camera.main.orthographicSize -= zoomDelta * zoom * zoomSensitivity;
  }
     
  private Vector2 GetKeyboardMovement() {
    Vector2 movementVector = new Vector2();
    if (Input.GetKey(KeyCode.W)) {
      movementVector += new Vector2(0, 1);
    }
    if (Input.GetKey(KeyCode.S)) {
      movementVector += new Vector2(0, -1);
    }
    if (Input.GetKey(KeyCode.A)) {
      movementVector += new Vector2(-1, 0);
    }
    if (Input.GetKey(KeyCode.D)) {
      movementVector += new Vector2(1, 0);
    }
    return movementVector;
  }
}
