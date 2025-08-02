using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private WeaponPanelOption _slotPrefab;
    private List<WeaponPanelOption> _slots = new();
    private GameObject player;
    private InputSystem_Actions _input;
    private int _slotIndex = 0; 
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.QuickItemAccess.performed += selectSlot;
        _input.Player.QuickItemAccessApprove.performed += approveSelection;
        _input.Player.Enable();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PlayerTag);
        Inventory[] inventories = player.transform.GetComponentsInChildren<Inventory>();

        int index = 1;
        foreach(var i in inventories) {
            if(!i.quickAccess) continue;

            WeaponPanelOption s = Instantiate(_slotPrefab, transform).GetComponent<WeaponPanelOption>();
            s.inventory = i;
            s.SetNumber(index);

            _slots.Add(s);
            index++;
        }
    }

    private void selectSlot(InputAction.CallbackContext ctx) {
        _slotIndex = (int)ctx.ReadValue<float>();
        _slotIndex -= 1;

        if (_slotIndex >= _slots.Count || _slotIndex < 0) return;
        
        for(int i = 0; i < _slots.Count; i++) {
            WeaponPanelOption currentSlot = _slots[i];
            if(i == _slotIndex) {
                currentSlot.Select();
            } else {
                currentSlot.Deselect();
            }
        }
    }

    private void hideSlots() {
        foreach(var s in _slots) {
            s.Deselect();
        }
    }

    private void approveSelection(InputAction.CallbackContext ctx) {
        foreach(var s in _slots) {
            if(s.panelSelected) {
                s.Approve();
                hideSlots();
                return;
            }
        }
    }
}
