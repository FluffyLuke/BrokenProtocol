using UnityEngine;
public class ChangePlayerStateWrapper : MonoBehaviour {
    public static void SwitchToCutsceneState() {
        PlayerEventBus.SwitchToCutsceneState.Invoke();
    }

    public static void SwitchToWalkState() {
        PlayerEventBus.SwitchToWalkState.Invoke();
    }
}