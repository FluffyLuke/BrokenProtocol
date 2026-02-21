using UnityEngine;

public abstract class IPlayerFeature : MonoBehaviour {
    public PlayerFeature featureName;
    public abstract void Disable();
    public abstract void Enable();
}

public enum PlayerFeature {
    Interact,
    OpenPauseMenu,
    Zoom,
}