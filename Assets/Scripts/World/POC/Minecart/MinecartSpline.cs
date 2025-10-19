using System;
using Unity.Mathematics;
using UnityEngine;
public class MinecartSpline : MonoBehaviour {
    public SimpleSpline container;
	public float t = 0;

	void Start() {
		(Vector3 position, Quaternion rotation) = container.GetNextPosition(0, true);
		transform.position = position;
		transform.rotation = rotation;	
	}
	public void PushCart(float speed, bool direction) {
        (Vector3 position, Quaternion rotation) = container.GetNextPosition(speed, direction);
		transform.position = position;
		transform.rotation = rotation;
    }
}

[Serializable]
public struct SimpleSpline {
	public SplineAnchor[] anchors;
    public int currentAnchorIndex;
	public float t;

	public (Vector3, Quaternion) GetNextPosition(float speed, bool direction = true) {
		SplineAnchor anchor = anchors[currentAnchorIndex];

		if (speed == 0) {
            Vector3 currentPosition = Vector3.Lerp(anchor.positionA.position, anchor.positionB.position, t);
			Vector3 currentDirectionVector = Vector3.Normalize(anchor.positionA.position - anchor.positionB.position); 
			Quaternion currentRotation = Quaternion.LookRotation(currentDirectionVector);
			return (currentPosition, currentRotation);
		}

        float delta = speed / anchor.GetLength() * Time.deltaTime;
		if (!direction) {
            delta *= -1;
        }

		t += delta;

		if (t > 1) {
            if (anchors.Length - 1 > currentAnchorIndex) {
                t -= 1;
				currentAnchorIndex += 1;
            }

			t = 1;
        }

		if (t < 0) {
            if (currentAnchorIndex != 0) {
                t += 1;
				currentAnchorIndex -= 1;
            }

			t = 0;
        }

		SplineAnchor newAnchor = anchors[currentAnchorIndex];

		Vector3 position = Vector3.Lerp(newAnchor.positionA.position, newAnchor.positionB.position, t);
        Vector3 directionVector = Vector3.Normalize(newAnchor.positionA.position - newAnchor.positionB.position); 
        Quaternion rotation = Quaternion.LookRotation(directionVector);

		return (position, rotation);
	}
}


[Serializable]
public struct SplineAnchor {
    public Transform positionA;
	public Transform positionB;

	public float GetLength() {
        return Vector3.Distance(positionA.position, positionB.position);
    }
}