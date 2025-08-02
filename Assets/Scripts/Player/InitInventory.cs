using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInventory : MonoBehaviour
{
    [SerializeField] private Inventory _holster;
    private void Start() {
        ItemData pistol = new ItemData {
            definition = ItemDefinitionDatabase.Get("ServicePistol")
        };
        pistol.AddProperty(new Ammo {
            count = 5,
            maxAmmo = 5,
        });
        _holster.AddItem(pistol);

        ItemData rifle = new ItemData {
            definition = ItemDefinitionDatabase.Get("ServiceRifle")
        };
        rifle.AddProperty(new Ammo {
            count = 30,
            maxAmmo = 30,
        });
        _holster.AddItem(rifle);
    }
}
