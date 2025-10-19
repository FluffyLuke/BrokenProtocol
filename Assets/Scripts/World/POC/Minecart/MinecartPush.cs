using UnityEngine;
using UnityEngine.Events;

public class MinecartPush : IInteractable
{
    private Minecart minecart; 
    [SerializeField] private Transform playerPushingPosition;
    void Start() {
        minecart = GetComponentInParent<Minecart>();
    }
    public override void Interact() {
        PlayerEventBus.PushMinecart.Invoke(playerPushingPosition);
    }
}
