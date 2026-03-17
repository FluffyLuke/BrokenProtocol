using UnityEngine;
using UnityEngine.Events;

public class PushObject : IInteractable
{
    private IPushable pushable; 
    [SerializeField] private Transform playerPushingPosition;
    [SerializeField] private int side;
    void Start() {
        if (TryGetComponent<IPushable>(out var component)) {
            pushable = component;
        } else {
            pushable = GetComponentInParent<IPushable>();
        }

        if (pushable == null) {
            Debug.LogError($"Couldn't find \"IPushable\" component");
        }
    }
    public override void Interact() {
        PlayerEventBus.SwitchToPushObjectState.Invoke(playerPushingPosition, pushable, side);
    }
}
