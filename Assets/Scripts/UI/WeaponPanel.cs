using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private WeaponPanelOption slotPrefab;
    private List<WeaponPanelOption> slots = new();
    private GameObject player;
    private InputSystem_Actions input;
    private int slotIndex = 0; 
    void Awake() {
        input = new InputSystem_Actions();
        input.Player.QuickItemAccess.performed += selectSlot;
        input.Player.QuickItemAccessApprove.performed += approveSelection;
        input.Player.Enable();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.PlayerTag);
        Inventory[] inventories = player.transform.GetComponentsInChildren<Inventory>();

        int index = 1;
        foreach(var i in inventories) {
            if(!i.quickAccess) continue;

            WeaponPanelOption s = Instantiate(slotPrefab, transform).GetComponent<WeaponPanelOption>();
            s.inventory = i;
            s.SetNumber(index);

            slots.Add(s);
            index++;
        }
    }

    private void selectSlot(InputAction.CallbackContext ctx) {
        slotIndex = (int)ctx.ReadValue<float>();
        slotIndex -= 1;

        if (slotIndex >= slots.Count || slotIndex < 0) return;
        
        for(int i = 0; i < slots.Count; i++) {
            WeaponPanelOption currentSlot = slots[i];
            if(i == slotIndex) {
                currentSlot.Select();
            } else {
                currentSlot.Deselect();
            }
        }
    }

    private void hideSlots() {
        foreach(var s in slots) {
            s.Deselect();
        }
    }

    private void approveSelection(InputAction.CallbackContext ctx) {
        foreach(var s in slots) {
            if(s.panelSelected) {
                s.Approve();
                hideSlots();
                return;
            }
        }
    }
}
