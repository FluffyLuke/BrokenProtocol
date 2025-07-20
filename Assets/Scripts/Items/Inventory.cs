using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool quickAccess;
    public int maxItems;
    public List<Item.ItemType> allowedTypes; // If non selected, all of them are allowed
    public List<Item.ItemType> unallowed;
    public List<Item> items;
}
