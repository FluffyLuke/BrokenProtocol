using UnityEngine;
using UnityEngine.Events;

public class PlayerEventBus : MonoBehaviour
{
    public static UnityEvent<ItemData> HoldItem = new();
    public static UnityEvent HideItem = new();
    public static UnityEvent SwitchToWalkState = new();
    public static UnityEvent SwitchToCutsceneState = new();
    public static UnityEvent<Transform, Minecart> PushMinecart = new();
    public static UnityEvent<CharacterDialogue, float> showMonologue = new();
    public static UnityEvent<CharacterDialogue, float> showDialogue = new();
}
