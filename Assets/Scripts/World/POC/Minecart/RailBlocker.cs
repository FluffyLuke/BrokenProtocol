using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
public class RailBlocker : MonoBehaviour, InteractableRequirement {
    [SerializeField] private Turnout turnout;
    public bool CheckRequirement() {
        foreach (var c in turnout.carts) {
            Debug.LogWarning("Dupa");
            (bool _, SplineAnchor currentAnchor) = c.rail.GetCurrentAnchor(ref c.posData);
            foreach (var ca in turnout.railConnection.connectionAnchors) {
                if (ca.anchor == currentAnchor) {
                    Debug.LogWarning("Cycki");
                    return false;
                }
            }
        }

        Debug.LogWarning("A");

        return true;
    }

    public string GetRequirementName()
    {
        return "Cart not blocking";
    }
}