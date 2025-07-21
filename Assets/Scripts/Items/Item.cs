using UnityEngine;
using System;

[Serializable]
public struct Item
{
    public ItemDefinition definition;

    public override string ToString() {
        return definition.displayName;
    }
}
