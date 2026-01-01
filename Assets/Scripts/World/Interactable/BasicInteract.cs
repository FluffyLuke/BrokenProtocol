using UnityEngine;
using UnityEngine.Events;

public class BasicInteract : IInteractable
{
    public override void Interact() {
        interactedWith.Invoke();
    }
}