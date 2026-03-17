using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public enum ElevatorState {
    Down,
	Moving,
	Up,
}
// [RequireComponent(typeof(Rigidbody))]
public class Elevator : MonoBehaviour {
	[SerializeField] private SplineContainer elevatorSpline;
	[SerializeField] private SplineContainer pathDown;
	[SerializeField] private SplineContainer pathUp;
	[SerializeField] private Transform elevatorDown;
	[SerializeField] private Transform elevatorUp;
	[SerializeField] private float moveSpeed;
	public List<Minecart> allCarts = new();
	private List<Minecart> currentCarts = new();
	private List<BezierKnot> newKnots = new();
	private Rigidbody rb;
	[SerializeField] public ElevatorState elevatorState {
		get;
		private set;
    } = ElevatorState.Down;
	void Start() {
		rb = GetComponent<Rigidbody>();
		rebuildSplines(elevatorState);
	}
	public void SetState(bool _) {
		if (elevatorState == ElevatorState.Moving) {
            Debug.LogError("Tried to move elevator, but it is already moving!");
			return;
        }

		ElevatorState currentState = elevatorState;
		ElevatorState nextElevatorState = currentState == ElevatorState.Down ? ElevatorState.Up : ElevatorState.Down;
		elevatorState = ElevatorState.Moving;
		rebuildSplines(ElevatorState.Moving);

		Vector3 moveTo = currentState == ElevatorState.Down ? elevatorUp.transform.position : elevatorDown.transform.position;
		float distance = Vector3.Distance(elevatorDown.position, elevatorUp.position);

		currentCarts.ForEach(c => c.EnablePushable(false));
		
		float previousY = transform.position.y;
		rb
			.DOMoveY(moveTo.y, distance / moveSpeed)
			.SetEase(Ease.Linear)
			.OnUpdate(() => {
				float difference = transform.position.y - previousY;
				currentCarts.ForEach(c => {
					Vector3 newPosition = c.transform.position;
					newPosition.y += difference;
					c.transform.position = newPosition;
				});
				previousY = transform.position.y;
			})
			.OnComplete(() => {
				elevatorState = nextElevatorState;
				rebuildSplines(elevatorState);
				currentCarts.ForEach(c => {
					c.EnablePushable(true);
				});
			});
    }
	
	// This method assumes, that bottom of the elevator is connected from behind
	// and top of the elevetor from the front
	private void rebuildSplines(ElevatorState newState) {
		Debug.Log("Rebuilding splines");
		switch (newState) {
			case ElevatorState.Down:
				foreach (BezierKnot knot in elevatorSpline[0].Reverse()) {
					BezierKnot newKnot = knot;
					newKnot.Position += (float3)transform.position;
					newKnot.Rotation = knot.Rotation;
					pathDown[0].Add(newKnot);
					newKnots.Add(newKnot);
				}

				foreach (var cart in currentCarts) {
					cart.rail = pathDown;
				}
				
				foreach (Minecart c in allCarts) {
					Debug.Log($"Cart {c.name} moving to a new spline");
					if (currentCarts.Contains(c)) continue;
					c.MoveToNewSpline();
				}
 
				break;
			
			case ElevatorState.Moving:
				foreach (var knot in newKnots) {
					pathDown[0].Remove(knot);
					pathUp[0].Remove(knot);
				}
				newKnots.Clear();
				foreach (var cart in currentCarts) {
					// cart.rail = elevatorSpline;
					//  cart.MoveToNewSpline();
				}
				foreach (Minecart c in allCarts) {
					Debug.Log($"Cart {c.name} moving to a new spline");
					if (currentCarts.Contains(c)) continue;
					c.MoveToNewSpline();
				}
				break;
			
			case ElevatorState.Up:
				foreach (var knot in elevatorSpline[0]) {
					BezierKnot newKnot = knot;
					newKnot.Position += (float3)transform.position;
					newKnot.Rotation = knot.Rotation;
					pathUp[0].Insert(0, newKnot);
					newKnots.Add(newKnot);
				}
				
				foreach (var cart in currentCarts) {
					cart.rail = pathUp;
				}
				
				foreach (Minecart c in allCarts) {
					Debug.Log($"Cart {c.name} moving to a new spline");
					c.MoveToNewSpline();
				}

				break;
		}
	}
	public void OnTriggerEnter(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (other.CompareTag(Tags.PlayerTag)) {
			return;
		}

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			if (minecart == null) return;
        }

		if (!currentCarts.Contains(minecart)) {
			Debug.Log("Adding new cart");
			currentCarts.Add(minecart);
		}
	}
	public void OnTriggerExit(Collider other) {
		Minecart minecart = other.GetComponent<Minecart>();

		if (minecart == null) {
            minecart = other.GetComponentInParent<Minecart>();
			if (minecart == null) return;
        }

		if (currentCarts.Contains(minecart)) {
			Debug.Log("Removing cart");
			currentCarts.Remove(minecart);
		}
	}
}