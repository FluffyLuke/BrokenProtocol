using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hands : MonoBehaviour
{
    [SerializeField] private GameObject hands;
    [SerializeField] private Inventory[] quickAccessInventories;
    private InputSystem_Actions input;
    private IItem currentItem;
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.HideHeldItem.performed += hideHeldItem;
        input.Player.Enable();

        PlayerEventBus
            .HoldItem
            .AddListener(holdItem);
    }

    void Start() {
        getInventories();
    }
    private void holdItem(ItemData item) {
        if(currentItem != null) {
            currentItem.Hide();
            currentItem = null;
        }

        Debug.Log($"Equiped item: {item}");
        currentItem = Instantiate(item.definition.prefab, hands.transform).GetComponent<IItem>();
        currentItem.Grab(item);
    }
    private void hideHeldItem(InputAction.CallbackContext ctx) {
        currentItem.Hide();
        currentItem = null;
    }
    private void getInventories() {
        Inventory[] allInventories = GetComponentsInChildren<Inventory>();
        int count = 0;
        for (int i = 0; i < allInventories.Length; i++) {
            count = allInventories[i].quickAccess ? count + 1 : count;
        }

        quickAccessInventories = new Inventory[count];

        int index = 0;
        for (int i = 0; i < allInventories.Length; i++) {
            if (allInventories[i].quickAccess)
            {
                quickAccessInventories[index] = allInventories[i];
                index++;
            }
        }
    }
}
