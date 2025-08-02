using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemDefinition")]
public class ItemDefinition : ScriptableObject
{
    public enum ItemType {
        TestType,
        Weapon,
    }
    public ItemType type;
    public string displayName;
    public GameObject prefab;
    public Sprite itemIcon;
}
