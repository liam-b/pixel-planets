using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler {
  public List<GameObject> objects = new List<GameObject>();
  public GameObject poolObject;

  public ObjectPooler(GameObject poolObject) {
    this.poolObject = poolObject;
  }

  public GameObject Spawn(Vector2 position, Quaternion rotation, Transform parent) {
    foreach (GameObject obj in objects) {
      if (!obj.activeSelf) {
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.parent = parent;
        return obj;
      }
    }

    GameObject newObject = GameObject.Instantiate(poolObject, position, rotation, parent);
    objects.Add(newObject);
    return newObject;
  }

  public void Despawn(GameObject obj) {
    obj.SetActive(false);
  }
}
