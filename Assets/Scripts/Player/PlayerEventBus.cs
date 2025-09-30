using UnityEngine;
using UnityEngine.Events;

public class PlayerEventBus : MonoBehaviour
{
    public static UnityEvent<ItemData> HoldItem = new();
    public static UnityEvent<PlayerState.PossibleStates> ChangeState = new();
    public static UnityEvent<CharacterDialogue> showMonologue = new();
}
