using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour {
    public enum ID
    {
        ServicePistol,
        ServiceRifle,
        ServiceBaton,
    };

    [Serializable]
    public struct WeaponSlot {
        public ID id;
        public GameObject weapon;
    }

    [SerializeField] private WeaponSlot[] _weapons;
    [HideInInspector] public static WeaponDatabase Instance;
    void Awake() {
        if(Instance != null) {
            Debug.LogError("Two weapon managers found in the scene!");
            return;
        }

        Instance = this;
    }

    public GameObject GetWeapon(ID name) {
        foreach(var w in _weapons) {
            if(w.id == name) return w.weapon;
        }
        Debug.LogWarning($"Cannot find weapon: {name}");
        return null;
    }
}
