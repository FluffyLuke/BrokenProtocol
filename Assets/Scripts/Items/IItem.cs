using UnityEngine;

public abstract class IItem : MonoBehaviour
{
    public abstract void Grab(ItemData itemData);
    public abstract void Hide();
}
