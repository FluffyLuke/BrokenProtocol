using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable : MonoBehaviour {
	void Awake() {
        if (gameObject.layer != 6) {
            Debug.LogError($"Interactable \"{gameObject.name}\" is not on interactable layer. Is this on purpose?");
        }

        requirements = GetComponents<InteractableRequirement>();
	}
	public bool canInteract = true;
    public float fireInteractDelay = 0.0f;
    public abstract void Interact();
    public UnityEvent interactedWith;
    // This is ugly
    public InteractionRequirement[] playerRequirements = new InteractionRequirement[0];
    public InteractableRequirement[] requirements = new InteractableRequirement[0];
}

[Serializable]
public enum InteractionRequirement {
    HolsterWeapon,
}