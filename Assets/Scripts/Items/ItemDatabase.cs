using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    [Serializable]
    public struct ItemSlot {
        public ID id; // ID is below
        public GameObject item;
    }

    [SerializeField] private ItemSlot[] _weapons;
    [HideInInspector] public static ItemDatabase Instance;
    void Awake() {
        if(Instance != null) {
            Debug.LogError("Two weapon managers found in the scene!");
            return;
        }

        Instance = this;
    }

    public GameObject GetItem(ID name) {
        foreach(var w in _weapons) {
            if(w.id == name) return w.item;
        }
        Debug.LogWarning($"Cannot find weapon: {name}");
        return null;
    }
    public enum ID
    {
        ServicePistol,
        ServiceRifle,
        ServiceBaton,
    };
}
