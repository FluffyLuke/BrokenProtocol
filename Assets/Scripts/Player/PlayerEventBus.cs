using UnityEngine;
using UnityEngine.Events;

public class PlayerEventBus : MonoBehaviour
{
    public static UnityEvent<ItemData> holdItem = new();
}
