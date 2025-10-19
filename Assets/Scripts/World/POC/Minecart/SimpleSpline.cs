using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public struct SplinePositionData {
    public float t;
    public int currentAnchorIndex;
}

public class SimpleSpline : MonoBehaviour {
	public SplineAnchor[] anchors;

	public (Vector3, Quaternion) GetNextPosition(ref SplinePositionData data, float speed, bool direction = true) {
		SplineAnchor anchor = anchors[data.currentAnchorIndex];

		if (speed == 0) {
            Vector3 currentPosition = Vector3.Lerp(anchor.positionA.position, anchor.positionB.position, data.t);
			Vector3 currentDirectionVector = Vector3.Normalize(anchor.positionB.position - anchor.positionA.position); 
			Quaternion currentRotation = Quaternion.LookRotation(currentDirectionVector);
			return (currentPosition, currentRotation);
		}

        float delta = speed / anchor.GetLength() * Time.deltaTime;
		if (!direction) {
            delta *= -1;
        }

		data.t += delta;

		if (data.t > 1) {
            if (anchors.Length - 1 > data.t) {
                data.t -= 1;
				data.currentAnchorIndex += 1;
            }

			data.t = 1;
        }

		if (data.t < 0) {
            if (data.currentAnchorIndex != 0) {
                data.t += 1;
				data.currentAnchorIndex -= 1;
            }

			data.t = 0;
        }

		SplineAnchor newAnchor = anchors[data.currentAnchorIndex];

		Vector3 position = Vector3.Lerp(newAnchor.positionA.position, newAnchor.positionB.position, data.t);
		Vector3 directionVector = Vector3.Normalize(anchor.positionB.position - anchor.positionA.position); 
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