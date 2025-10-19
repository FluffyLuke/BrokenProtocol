using UnityEngine;
using UnityEngine.Events;

public class MinecartPush : IInteractable
{
    private Minecart minecart; 
    [SerializeField] private Transform playerPushingPosition;
    [SerializeField] private bool isThisABackSide;
    void Start() {
        minecart = GetComponentInParent<Minecart>();
    }
    public override void Interact() {
        minecart.playerFromTheBack = isThisABackSide;
        PlayerEventBus.PushMinecart.Invoke(playerPushingPosition);
    }
}
