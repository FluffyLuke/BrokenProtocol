using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IEnumerable<ItemData>
{
    public bool quickAccess;
    public int maxItems;
    public List<ItemDefinition.ItemType> allowedTypes; // If non selected, all of them are allowed
    public List<ItemDefinition.ItemType> unallowed;
    [HideInInspector] public UnityEvent<ItemData> itemAdded = new UnityEvent<ItemData>();
    [HideInInspector] public UnityEvent<ItemData> itemRemoved = new UnityEvent<ItemData>();
    [SerializeField] private List<ItemData> _items = new();
    public ReadOnlyCollection<ItemData> items => _items.AsReadOnly();
    public bool AddItem(ItemData item) {
        if(maxItems <= _items.Count) {
            Debug.LogWarning("Cannot add item. Inventory too small");
            return false;
        }

        if(!allowedTypes.Contains(item.definition.type) && allowedTypes.Count > 0) {
            Debug.LogWarning("Cannot add item. Item type is now allowed");
            return false;
        }

        if(unallowed.Contains(item.definition.type)) {
            Debug.LogWarning("Cannot add item. Item type is now allowed");
            return false;
        }

        _items.Add(item);
        itemAdded.Invoke(item);

        return true;
    }

    public bool RemoveItem(ItemData item) {
        if(_items.Remove(item)) {
            itemRemoved.Invoke(item);
            return true;
        }
        Debug.LogWarning($"Cannot remove item of display name: {item.definition.displayName}");
        return false;
    }
    public bool RemoveItem(int index) {
        if (index < 0 || index >= _items.Count) {
            Debug.LogWarning($"Cannot remove item of index: {index}. Inventory too small");
            return false;
        }

        ItemData item = _items[index];
        _items.RemoveAt(index);
        itemRemoved.Invoke(item);
        return true;
    }

    public IEnumerator<ItemData> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
