using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hands : MonoBehaviour
{
    [SerializeField] private GameObject _hands;
    [SerializeField] private Inventory[] _quickAccessInventories;
    private InputSystem_Actions _input;
    private IItem _currentItem;
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.HideHeldItem.performed += hideHeldItem;
        _input.Player.Enable();

        PlayerEventBus
            .holdItem
            .AddListener(holdItem);
    }

    void Start() {
        getInventories();
    }
    private void holdItem(ItemData item) {
        Debug.Log($"Equiped item: {item}");
        _currentItem = Instantiate(item.definition.prefab, _hands.transform).GetComponent<IItem>();
        _currentItem.Grab(item);
    }
    private void hideHeldItem(InputAction.CallbackContext ctx) {
        _currentItem.Hide();
        _currentItem = null;
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
