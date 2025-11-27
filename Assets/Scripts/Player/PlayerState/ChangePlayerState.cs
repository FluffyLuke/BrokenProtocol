using UnityEngine;

public class ChangePlayerState : MonoBehaviour {

    // Cart pushing is not covered!
    public void ChangeToWalkState() {
        PlayerEventBus.SwitchToWalkState.Invoke();
    }

    public void ChangeToCutsceneState() {
        PlayerEventBus.SwitchToCutsceneState.Invoke();
    }
}