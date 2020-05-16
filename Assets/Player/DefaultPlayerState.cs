using UnityEngine;

public class DefaultPlayerState : StateMachineBehaviour {
  void OnStateUpdate(Animator animator) {
    if (Input.GetKeyDown(KeyCode.Tab)) animator.SetTrigger("EnterBuilding");
  }
}
