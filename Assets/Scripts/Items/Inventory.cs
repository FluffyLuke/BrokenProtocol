using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool quickAccess;
    public int maxItems;
    public List<ItemDefinition.ItemType> allowedTypes; // If non selected, all of them are allowed
    public List<ItemDefinition.ItemType> unallowed;
    public List<ItemData> items;
}
