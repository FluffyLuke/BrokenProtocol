using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct ItemDatabaseSlot {
    public string key;
    public ItemDefinition definition;
}

public class ItemDefinitionDatabase : MonoBehaviour
{
    private static Dictionary<string, ItemDefinition> _itemDefinitions;
    [SerializeField] private ItemDatabaseSlot[] _startingItems; // Workaround for inspector
    void Awake()
    {
        _itemDefinitions = new Dictionary<string, ItemDefinition>();

        foreach(var s in _startingItems) {
            _itemDefinitions.Add(s.key, s.definition);
        }
    }

    public static ItemDefinition Get(string name) {
        if(_itemDefinitions.ContainsKey(name)) {
            return _itemDefinitions[name];
        }
        Debug.LogError($"Cannot find item definition of name {name} in database");
        return null;
    }
}
