using TMPro;
using UnityEngine;

public class WeaponPanelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slotNumber;
    [SerializeField] private GameObject itemDisplayPrefab;
    [SerializeField] private GameObject itemList;
    [HideInInspector] public Inventory inventory;
    public bool panelSelected {
        get;
        private set;
    }
    private int itemIndex = -1;

    public void SetNumber(int number) {
        slotNumber.text = number.ToString();
    }
    public void Select() {
        panelSelected = true;
        foreach(Transform c in itemList.transform) {
            Destroy(c.gameObject);
        }

        itemIndex += 1;
        if(inventory.items.Count == 0) return;
        if(inventory.items.Count <= itemIndex) itemIndex = 0;

        int index = 0;
        foreach(ItemData i in inventory) {
            ItemDisplay display = Instantiate(itemDisplayPrefab, itemList.transform).GetComponent<ItemDisplay>();
            
            bool selected = index == itemIndex;

            display.Init(i.definition.itemIcon, selected);
            index++;
        }
    }
    public void Approve() {
        ItemData item = inventory.items[itemIndex];
        if(item == null) {
            Debug.LogError("Cannot put item in hands");
            return;
        }
        PlayerEventBus.HoldItem.Invoke(item);
    }
    public void Deselect() {
        panelSelected = false;
        itemIndex = -1;
        foreach(Transform c in itemList.transform) {
            Destroy(c.gameObject);
        }
    }
}
