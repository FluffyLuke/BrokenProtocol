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
            (bool _, SplineAnchor currentAnchor) = c.rail.GetCurrentAnchor(ref c.posData);
            foreach (var ca in turnout.railConnection.connectionAnchors) {
                if (ca.anchor == currentAnchor) {
                    return false;
                }
            }
        }

        return true;
    }

    public string GetRequirementName()
    {
        return "Cart not blocking";
    }
}