using UnityEngine;
using UnityEngine.Events;

public class PlayerEventBus : MonoBehaviour
{
    public static UnityEvent<ItemData> HoldItem = new();
    public static UnityEvent HideItem = new();
    public static UnityEvent SwitchToWalkState = new();
    public static UnityEvent SwitchToCutsceneState = new();
    public static UnityEvent<Transform, Minecart> PushMinecart = new();
    public static UnityEvent<CharacterDialogue, float> ShowMonologue = new();
    public static bool DialogueIsTakingPlace = false;
    public static UnityEvent<CharacterDialoguePart[]> ShowDialogue = new();
    public static UnityEvent<bool> OpenPause = new();
    public static bool isPauseOpened = false;
}
