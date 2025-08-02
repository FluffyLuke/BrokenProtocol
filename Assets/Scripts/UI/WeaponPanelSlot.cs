using TMPro;
using UnityEngine;

public class WeaponPanelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _slotNumber;
    [SerializeField] private GameObject _itemDisplayPrefab;
    [SerializeField] private GameObject _itemList;
    [HideInInspector] public Inventory inventory;
    public bool panelSelected {
        get;
        private set;
    }
    private int _itemIndex = -1;

    public void SetNumber(int number) {
        _slotNumber.text = number.ToString();
    }
    public void Select() {
        panelSelected = true;
        foreach(Transform c in _itemList.transform) {
            Destroy(c.gameObject);
        }

        _itemIndex += 1;
        if(inventory.items.Count == 0) return;
        if(inventory.items.Count <= _itemIndex) _itemIndex = 0;

        int index = 0;
        foreach(ItemData i in inventory) {
            ItemDisplay display = Instantiate(_itemDisplayPrefab, _itemList.transform).GetComponent<ItemDisplay>();
            
            bool selected = index == _itemIndex;

            display.Init(i.definition.itemIcon, selected);
            index++;
        }
    }
    public void Approve() {
        ItemData item = inventory.items[_itemIndex];
        if(item == null) {
            Debug.LogError("Cannot put item in hands");
            return;
        }
        PlayerEventBus.holdItem.Invoke(item);
    }
    public void Deselect() {
        panelSelected = false;
        _itemIndex = -1;
        foreach(Transform c in _itemList.transform) {
            Destroy(c.gameObject);
        }
    }
}
