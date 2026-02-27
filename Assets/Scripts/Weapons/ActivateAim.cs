using UnityEngine;
using UnityEngine.Events;

public class ActivateWeapon : MonoBehaviour {
    private void Start() {
        PlayerEventBus.ShowAim.Invoke(true);
    }
    private void OnDestroy() {
        PlayerEventBus.ShowAim.Invoke(false);
    }
}
