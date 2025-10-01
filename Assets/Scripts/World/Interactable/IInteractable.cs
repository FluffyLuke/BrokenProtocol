using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable : MonoBehaviour {
    public abstract void Interact();
    public UnityEvent interactedWith;
}