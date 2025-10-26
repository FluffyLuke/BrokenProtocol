using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RailPath {
    public SimpleSpline splineA;
	public SimpleSpline splineB;
}
public class Turnout : MonoBehaviour {
    
	[SerializeField] private SimpleSpline turnoutSpline;
	[SerializeField] private RailPath pathA;
	[SerializeField] private RailPath pathB;
	private List<Minecart> carts = new();
	private bool currentState = false;
	public void SetState(bool state) {
		currentState = state;
		foreach(var m in carts) {
            reconstructPath(m);
        }
    }

	public void OnTriggerEnter(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			Debug.Log(other.gameObject.name);
			if (minecart == null) return;
        }

		if (!carts.Contains(minecart)) {
			carts.Add(minecart);
			reconstructPath(minecart);
		}
	}

	public void OnTriggerExit(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			if (minecart == null) return;
        }

		if (carts.Contains(minecart)) {
			carts.Remove(minecart);
			reconstructPath(minecart);
		}
	}

	private void reconstructPath(Minecart minecart) {
		RailPath currentRail;
		SimpleSpline currentSpline;

		Debug.Log($"Reconstructing spline for {minecart.gameObject.name}");

		(bool ifFound, SplineAnchor currentAnchor) = minecart.rail.GetCurrentAnchor(ref minecart.posData);

		if (ifFound) {
			if (pathA.splineA.ContainsAnchor(currentAnchor)) {
				currentSpline = pathA.splineA;
				currentRail = pathA;
			} else if (pathA.splineB.ContainsAnchor(currentAnchor)) {
				currentSpline = pathA.splineB;
				currentRail = pathA;
			} else if (pathB.splineA.ContainsAnchor(currentAnchor)) {
				currentSpline = pathB.splineA;
				currentRail = pathB;
			} else if (pathB.splineB.ContainsAnchor(currentAnchor)) {
				currentSpline = pathB.splineB;
				currentRail = pathB;
			} else {
                Debug.LogError("Cart is not on any of the known rails!");
				return;
            }
		} else {
            Debug.LogError("Cannot get current anchor of minecart!");
			return;
        }

		// We need to add rails (anchors) in ascending order
		List<SplineAnchor> newRail = new();

		// If Minecart is on the rail "behind" the turnout
		if (currentSpline == pathA.splineA || currentSpline == pathB.splineA) {
			// We can safely add the current rail, since it is the first one to go
            newRail.AddRange(currentSpline.anchors);
			// We need to check if passage is open. If so: add turnout anchors and the following rail
			if ((currentRail == pathA && !currentState) || (currentRail == pathB && currentState)) {
				newRail.AddRange(turnoutSpline.anchors);
				newRail.AddRange(currentRail.splineB.anchors);
			}
		// If Minecart is on the rail "after" the turnout
        } else {
			// We must check, if passage is open. If so, add rails (anchors) before the current rail
			if ((currentRail == pathA && !currentState) || (currentRail == pathB && currentState)) {
				newRail.AddRange(currentRail.splineA.anchors);
				newRail.AddRange(turnoutSpline.anchors);
			}
			// Add current rail after adding (if neccessary) previous rails
			newRail.AddRange(currentSpline.anchors);
        }

		int index = SimpleSpline.GetAnchorIndex(newRail, currentAnchor);
		
		if (index < 0) {
            Debug.LogError("Cannot get index for new rail?");
			return;
        }

		minecart.RebuildRail(newRail, index);
    }
}