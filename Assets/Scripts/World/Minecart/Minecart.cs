using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SimpleSpline))]
public class Minecart : MonoBehaviour {
    public float speed = 3f;
    public float reverseSpeed = 2f;
    public SplinePositionData posData;
    [SerializeField] private SimpleSpline startingRail;
    public SimpleSpline rail;
    public bool playerFromTheBack;

    public bool pushingForwardBlocked = false;
    public bool pushingBackwardsBlocked = false;

    void Start() {
        rail = GetComponent<SimpleSpline>();

        rail.anchors = new List<SplineAnchor>(startingRail.anchors);

		(Vector3 position, Quaternion rotation) = rail.GetCurrentPosition(ref posData);
		transform.position = position;
		transform.rotation = rotation;	
	}

	void LateUpdate() {
		(Vector3 position, Quaternion rotation) = rail.GetCurrentPosition(ref posData);
		transform.position = position;
		transform.rotation = rotation;
	}

	// This code is a mess, but a working mess (I hope)
	public void PushForward() {
        if (playerFromTheBack) {
            PushCart(speed, true);
        } else {
            PushCart(speed, false);
        }
    }

    public void PushBackward() {
        if (playerFromTheBack) {
            PushCart(reverseSpeed, false);
        } else {
            PushCart(reverseSpeed, true);
        }
    }
	public void PushCart(float speed, bool direction) {
        Debug.Log(direction);
        if (!direction && pushingForwardBlocked) {
            return;
        }

        if (direction && pushingBackwardsBlocked) {
            return;
        }

        (Vector3 position, Quaternion rotation) = rail.GetNextPosition(ref posData, speed, direction);
		transform.position = position;
		transform.rotation = rotation;
    }
    public void ResetPosition() {
        (Vector3 position, Quaternion rotation) = rail.GetNextPosition(ref posData, 0, true);
		transform.position = position;
		transform.rotation = rotation;
    }

    public void RebuildRail(List<SplineAnchor> anchors, int newIndex) {
        rail.anchors = anchors;
        posData.currentAnchorIndex = newIndex;
        ResetPosition();
    }
}