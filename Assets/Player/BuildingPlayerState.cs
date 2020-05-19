using UnityEngine;

public class BuildingPlayerState : StateMachineBehaviour {
  public GameObject[] partPrefabs;
  [HideInInspector] public GameObject selectedPart;
  [HideInInspector] public PartController placingPart;
  [HideInInspector] public Quaternion placementRotation;

  void Awake() {
    placementRotation = Quaternion.identity;
    partPrefabs = Resources.LoadAll<GameObject>("Parts");
  }

  void OnStateUpdate(Animator animator) {
    if (Input.GetKeyDown(KeyCode.Tab)) {
      PartController.GetAllParts().ForEach(part => part.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic);
      animator.SetTrigger("ExitBuilding");
    }

    // a little sketchy
    for (var i = 0; i < 9; i++) {
      if (Input.GetKeyDown(i.ToString())) {
        selectedPart = partPrefabs[i - 1];
        animator.SetTrigger("SelectPart");
      }
    }
  }
}
