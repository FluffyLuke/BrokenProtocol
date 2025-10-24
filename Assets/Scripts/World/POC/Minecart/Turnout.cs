using System.Collections.Generic;
using UnityEngine;

public class Turnout : MonoBehaviour {
    [SerializeField] private SimpleSpline splineA1;
	[SerializeField] private SimpleSpline splineA2;
	[SerializeField] private SimpleSpline splineB1;
	[SerializeField] private SimpleSpline splineB2;
	[SerializeField] private SimpleSpline turnoutSpline;
	private List<Minecart> carts = new();
	private bool currentState = false;
	public void SetState(bool state) {
		currentState = state;
    }

	public void OnTriggerEnter(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			if (minecart == null) return;
        }

		if (!carts.Contains(minecart)) {
			Debug.Log("Connecting minecart!");
            carts.Add(minecart);
			SplineAnchor anchor = (SplineAnchor)minecart.rail.GetCurrentAnchor(ref minecart.posData);
			
			if (splineA1.ContainsAnchor(anchor) && !currentState) {
				Debug.Log($"Rail {splineA2.name} was connected");
                minecart.rail.ConnectSpline(turnoutSpline.anchors, true);
				minecart.rail.anchors.Add(turnoutSpline.anchors[0]);
				minecart.rail.ConnectSpline(splineA2.anchors, true);
            } else if (splineA2.ContainsAnchor(anchor) && !currentState) {
				Debug.Log($"Rail {splineA1.name} was connected");
                minecart.rail.ConnectSpline(turnoutSpline.anchors, false);
				minecart.rail.ConnectSpline(splineA1.anchors, false);
            } else if (splineB1.ContainsAnchor(anchor) && currentState) {
				Debug.Log($"Rail {splineB2.name} was connected");
                minecart.rail.ConnectSpline(turnoutSpline.anchors, true);
				minecart.rail.ConnectSpline(splineB2.anchors, true);
			} else if (splineB2.ContainsAnchor(anchor) && currentState) {
				Debug.Log($"Rail {splineB1.name} was connected");
                minecart.rail.ConnectSpline(turnoutSpline.anchors, false);
				minecart.rail.ConnectSpline(splineB1.anchors, false);
            } else {
                Debug.LogError("Cannot connect minecart to any of the rails!");
            }
        } else {
            Debug.LogWarning("Minecarts has entered the turnout twice?");
        }
	}

	public void OnTriggerExit(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			if (minecart == null) return;
        }

		if (carts.Contains(minecart)) {
			Debug.Log("Disconnecting minecart!");
            carts.Remove(minecart);

			SplineAnchor anchor = (SplineAnchor)minecart.rail.GetCurrentAnchor(ref minecart.posData);
			
			if (!splineA1.ContainsAnchor(anchor)) {
                minecart.rail.DisconnectSpline(splineA1.anchors);
            }
			if (!splineA2.ContainsAnchor(anchor)) {
                minecart.rail.DisconnectSpline(splineA2.anchors);
            }
			if (!splineB1.ContainsAnchor(anchor)) {
                minecart.rail.DisconnectSpline(splineB1.anchors);
            }
			if (!splineB2.ContainsAnchor(anchor)) {
                minecart.rail.DisconnectSpline(splineB2.anchors);
            }

			minecart.rail.DisconnectSpline(turnoutSpline.anchors);

        } else {
            Debug.LogWarning("Minecarts has exited the turnout, yet never have entered it?");
        }
	}
}