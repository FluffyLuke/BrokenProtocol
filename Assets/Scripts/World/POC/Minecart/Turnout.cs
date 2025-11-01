using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

[Serializable]
public class RailPath {
    public SimpleSpline splineA;
	public SimpleSpline splineB;
}
public class Turnout : MonoBehaviour {
    
	[SerializeField] private SimpleSpline turnoutSpline;
	[SerializeField] private RailPath pathA;
	[SerializeField] private RailPath pathB;
	[SerializeField] private float cartPathRebuildCooldown = 5;
	Coroutine rebuildCartsCoroutine = null;
	private List<Minecart> carts = new();
	private (bool, SplineAnchor)[] connectionAnchors = new (bool, SplineAnchor)[2];
	private bool currentState = false;
	public void SetState(bool state) {
		currentState = state;
		resetConnectionAnchors(state ? pathB : pathA);

		foreach(var m in carts) {
			(bool _, SplineAnchor currentAnchor) = m.rail.GetCurrentAnchor(ref m.posData);
			if(turnoutSpline.ContainsAnchor(currentAnchor)) {
                m.RebuildRail(turnoutSpline.anchors, 0);

				if (rebuildCartsCoroutine != null) {
                    StopCoroutine(rebuildCartsCoroutine);
                }

				rebuildCartsCoroutine = StartCoroutine(TurnEnd());
            } else {
                reconstructPath(m);
            }
        }
    }

	public IEnumerator TurnEnd() {
		yield return new WaitForSeconds(cartPathRebuildCooldown);
        foreach(var m in carts) {
			(bool _, SplineAnchor currentAnchor) = m.rail.GetCurrentAnchor(ref m.posData);
			if(turnoutSpline.ContainsAnchor(currentAnchor)) {
                reconstructPathOnTurnout(m);
            }
        }
		rebuildCartsCoroutine = null;
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

		// How do I explain this at the pearly gates?

		// If Minecart is on the rail "behind" the turnout
		if (currentSpline == pathA.splineA || currentSpline == pathB.splineA) {
			// We can safely add the current rail, since it is the first one to go
            newRail.AddRange(currentSpline.anchors);
			// We need to check if passage is open. If so: add turnout anchors and the following rail
			if ((currentRail == pathA && !currentState) || (currentRail == pathB && currentState)) {
				

				newRail.Add(connectionAnchors[0].Item2);
				newRail.AddRange(turnoutSpline.anchors);
				newRail.AddRange(currentRail.splineB.anchors);

				if (connectionAnchors[1].Item1) {
					newRail.Add(connectionAnchors[1].Item2);
                }
			}
		// If Minecart is on the rail "after" the turnout
        } else {
			// We must check, if passage is open. If so, add rails (anchors) before the current rail
			if ((currentRail == pathA && !currentState) || (currentRail == pathB && currentState)) {
				if (connectionAnchors[0].Item1) {
					newRail.Add(connectionAnchors[0].Item2);
                }
				
				newRail.AddRange(currentRail.splineA.anchors);
				newRail.AddRange(turnoutSpline.anchors);

				newRail.Add(connectionAnchors[1].Item2);
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

	public void reconstructPathOnTurnout(Minecart minecart) {
        RailPath rail = currentState ? pathB : pathA;
		SplineAnchor currentAnchor = turnoutSpline.anchors[0];

		List<SplineAnchor> newRail = new();

		newRail.AddRange(rail.splineA.anchors);

		if (rail.splineA.anchors.Count > 0) {
			SplineAnchor connectionAnchor = new SplineAnchor (
				rail.splineA.anchors.Last().positionB,
				turnoutSpline.anchors[0].positionA
			);
			newRail.Add(connectionAnchor);
		}

		newRail.AddRange(turnoutSpline.anchors);

		if (rail.splineB.anchors.Count > 0) {
			SplineAnchor connectionAnchor = new SplineAnchor
			(
				turnoutSpline.anchors[0].positionB,
				rail.splineB.anchors[0].positionA
			);
			newRail.Add(connectionAnchor);
		}

		newRail.AddRange(rail.splineB.anchors);

		int index = SimpleSpline.GetAnchorIndex(newRail, currentAnchor);
		minecart.RebuildRail(newRail, index);
    }

	private void resetConnectionAnchors(RailPath rail) {
		connectionAnchors[0] = (false, default);
		connectionAnchors[1] = (false, default);

        if (rail.splineA.anchors.Count > 0) {
			SplineAnchor connectionAnchor = new SplineAnchor (
				rail.splineA.anchors.Last().positionB,
				turnoutSpline.anchors[0].positionA
			);
			connectionAnchors[0] = (true, connectionAnchor);
		}

		if (rail.splineB.anchors.Count > 0) {
			SplineAnchor connectionAnchor = new SplineAnchor
			(
				turnoutSpline.anchors[0].positionB,
				rail.splineB.anchors[0].positionA
			);
			connectionAnchors[1] = (true, connectionAnchor);
		}
    }
}