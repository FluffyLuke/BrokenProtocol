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
// [RequireComponent(typeof(Rigidbody))]
public class Elevator : MonoBehaviour {
	[SerializeField] private Spline elevatorSpline;
	[SerializeField] private Spline pathDown;
	[SerializeField] private Spline pathUp;
	[SerializeField] private Transform elevatorDown;
	[SerializeField] private Transform elevatorUp;
	[SerializeField] private float moveSpeed;
	public List<Minecart> carts = new();
	private Rigidbody rb;
	[SerializeField] public ElevatorState elevatorState {
		get;
		private set;
    } = ElevatorState.Down;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	public void SetState(bool _) {
		if (elevatorState == ElevatorState.Moving) {
            Debug.LogError("Tried to move elevator, but it is already moving!");
			return;
        }

		ElevatorState currentState = elevatorState;
		ElevatorState nextElevatorState = currentState == ElevatorState.Down ? ElevatorState.Up : ElevatorState.Down;
		elevatorState = ElevatorState.Moving;

		Vector3 moveTo = currentState == ElevatorState.Down ? elevatorUp.transform.position : elevatorDown.transform.position;
		float distance = Vector3.Distance(elevatorDown.position, elevatorUp.position);
		
		rb
			.DOMoveY(moveTo.y, distance / moveSpeed)
			.SetEase(Ease.Linear)
			.OnComplete(() => {
				elevatorState = nextElevatorState;
			});
    }

	public void OnTriggerEnter(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (other.CompareTag(Tags.PlayerTag)) {
			
			return;
		}

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			Debug.Log(other.gameObject.name);
			if (minecart == null) return;
        }

		if (!carts.Contains(minecart)) {
			carts.Add(minecart);
			//reconstructPath(minecart);
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
}