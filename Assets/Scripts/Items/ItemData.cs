using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public interface ItemProperty {};

[Serializable]
public class ItemData
{
    public ItemDefinition definition;

    public override string ToString() {
        return definition.displayName;
    }

    private List<ItemProperty> properties = new();
    public ItemData AddProperty(ItemProperty property) {
        properties.Add(property);
        return this;
    }
    public T GetProperty<T>() where T: class, ItemProperty {
        T p = properties.OfType<T>().FirstOrDefault();
        if(p == null) {
            Debug.LogWarning($"Cannot get property of type \"{typeof(T)}\"");
        }
        return p;
    }

    // public bool DeleteProperty<T>() where T: class, ItemProperty {
    //     return properties.OfType<T>().
    // }
}

public class Ammo : ItemProperty {
    public int count;
    public int maxAmmo;
}
