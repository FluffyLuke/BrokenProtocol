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

        _holster.items.Add(pistol);
    }
}
