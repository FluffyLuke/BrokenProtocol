using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public enum ElevatorState {
    Down,
	Moving,
	Up,
}
public class Elevator : MonoBehaviour {
	[SerializeField] private SimpleSpline elevatorSpline;
	[SerializeField] private SimpleSpline pathDown;
	[SerializeField] private SimpleSpline pathUp;
	[SerializeField] private Transform elevatorDown;
	[SerializeField] private Transform elevatorUp;
	[SerializeField] private float moveSpeed;
	public List<Minecart> carts = new();
	[HideInInspector] public RailConnection railConnection;
	[SerializeField] public ElevatorState elevatorState {
		get;
		private set;
    } = ElevatorState.Down;
	void Start() {
		railConnection = GetComponent<RailConnection>();
		railConnection.connectionAnchors = new RailConnectionField[1];
		resetConnectionAnchors();
	}
	public void SetState(bool state) {
		if (elevatorState == ElevatorState.Moving) {
            Debug.LogError("Tried to move elevator, but it is already moving!");
			return;
        }

		ElevatorState nextElevatorState = state ? ElevatorState.Up : ElevatorState.Down;
		elevatorState = ElevatorState.Moving;

		resetConnectionAnchors();

		foreach(var m in carts) {
			(bool _, SplineAnchor currentAnchor) = m.rail.GetCurrentAnchor(ref m.posData);
			if (elevatorSpline.ContainsAnchor(currentAnchor)) {
                m.RebuildRail(elevatorSpline.anchors, 0);
            } else {
                reconstructPath(m);
            }
        }

		Vector3 currentPosition = transform.position;
		Vector3 moveTo = state ? elevatorUp.transform.position : elevatorDown.transform.position;
		float distance = Vector3.Distance(currentPosition, moveTo);

		transform
			.DOMoveY(moveTo.y, moveSpeed)
			.OnComplete(() => {
				elevatorState = nextElevatorState;
				resetConnectionAnchors();
				carts.ForEach(m => reconstructPath(m));
			});
    }

	public void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);
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
			//reconstructPath(minecart);
		}
	}
	public void reconstructPath(Minecart minecart) {
		List<SplineAnchor> newRail = new();
		SplineAnchor currentAnchor = minecart.rail.GetCurrentAnchor(ref minecart.posData).Item2;

		if (elevatorState == ElevatorState.Down && pathDown.anchors.Count > 0) {
			newRail.AddRange(pathDown.anchors);
			newRail.Add(railConnection.connectionAnchors[0].anchor);
			newRail.Add(elevatorSpline.anchors[0]);
		}

		if (elevatorState == ElevatorState.Up && pathUp.anchors.Count > 0) {
			newRail.Add(elevatorSpline.anchors[0]);
			newRail.Add(railConnection.connectionAnchors[0].anchor);
			newRail.AddRange(pathUp.anchors);
		}

		int index = SimpleSpline.GetAnchorIndex(newRail, currentAnchor);
		minecart.RebuildRail(newRail, index);
    }

	private void resetConnectionAnchors() {
		railConnection.connectionAnchors[0] = RailConnectionField.Empty();

		// Elevator is at the bottom
        if (pathDown.anchors.Count > 0 && elevatorState == ElevatorState.Down) {
			SplineAnchor connectionAnchor = new SplineAnchor (
				pathDown.anchors.Last().positionB,
				elevatorSpline.anchors[0].positionA
			);
			railConnection.connectionAnchors[0].Set(connectionAnchor);
		}

		// Elevator is at the peak
		if (pathUp.anchors.Count > 0 && elevatorState == ElevatorState.Up) {
			SplineAnchor connectionAnchor = new SplineAnchor
			(
				elevatorSpline.anchors[0].positionB,
				pathUp.anchors[0].positionA
			);
			railConnection.connectionAnchors[0].Set(connectionAnchor);
		}
    }
}