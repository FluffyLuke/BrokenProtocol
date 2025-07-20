using UnityEngine;
using System;

[Serializable]
public struct Item
{
    public enum ItemType {
        TestType,
        Weapon,
    }
    public ItemType Type;
    public ItemDatabase.ID ID; // ID is used to get the game object
}
