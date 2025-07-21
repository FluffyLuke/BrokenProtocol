using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hands : MonoBehaviour
{
    [SerializeField] private GameObject _hands;
    [SerializeField] private Inventory[] _quickAccessInventories;
    private InputSystem_Actions _input;
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.QuickItemAccess.performed += getItem;
        _input.Player.HideHeldItem.performed += hideHeldItem;
        _input.Player.Enable();
    }

    void Start() {
        getInventories();
    }

    // Update is called once per frame
    private void getItem(InputAction.CallbackContext ctx) {
        int slot = (int)ctx.ReadValue<float>();
        slot -= 1;
        if (slot >= _quickAccessInventories.Length || slot < 0) return;
        if(_quickAccessInventories.Length == 0) {
            Debug.Log("Cannot get any quickslot inventory");
            return;
        }

        Inventory currentInventory = _quickAccessInventories[slot];

        if(currentInventory.items.Count <= 0) {
            Debug.Log("No items in quickslot inventory");
            return;
        };

        Item item = currentInventory.items[0];
        Debug.Log($"Got item: {item}");
        Instantiate(item.definition.prefab, _hands.transform);
    }
    private void hideHeldItem(InputAction.CallbackContext ctx) {
        foreach(Transform c in _hands.transform) {
            Destroy(c.gameObject);
        }
    }
    private void getInventories() {
        Inventory[] allInventories = GetComponentsInChildren<Inventory>();
        int count = 0;
        for (int i = 0; i < allInventories.Length; i++) {
            count = allInventories[i].quickAccess ? count + 1 : count;
        }

        _quickAccessInventories = new Inventory[count];

        int index = 0;
        for (int i = 0; i < allInventories.Length; i++) {
            if (allInventories[i].quickAccess)
            {
                _quickAccessInventories[index] = allInventories[i];
                index++;
            }
        }
    }
}
