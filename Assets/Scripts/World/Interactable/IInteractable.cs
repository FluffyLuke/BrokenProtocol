using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class IInteractable : MonoBehaviour {
    public bool canInteract = true;
    public float fireInteractDelay = 0.0f;
    public abstract void Interact();
    public UnityEvent interactedWith;
    [SerializeField] public InteractionRequirement[] requirements = new InteractionRequirement[0];
}

[Serializable]
public enum InteractionRequirement {
    HolsterWeapon,
}