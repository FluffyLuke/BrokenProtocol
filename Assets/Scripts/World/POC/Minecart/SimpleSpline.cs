using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct SplinePositionData {
    public float t;
    public int currentAnchorIndex;
}

public class SimpleSpline : MonoBehaviour {
	public List<SplineAnchor> anchors;
	public (Vector3, Quaternion) GetNextPosition(ref SplinePositionData data, float speed, bool direction = true) {
	
		// TODO: change this dirty workaround in the future
		if(anchors.Count <= data.currentAnchorIndex) {
            data.currentAnchorIndex = anchors.Count - 1;
        }

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
            if (anchors.Count - 1 > data.currentAnchorIndex) {
                data.t -= 1;
				data.currentAnchorIndex += 1;
            } else {
                data.t = 1;
            }
        }

		if (data.t < 0) {
            if (data.currentAnchorIndex != 0) {
                data.t += 1;
				data.currentAnchorIndex -= 1;
            } else {
                data.t = 0;
            }
        }

		SplineAnchor newAnchor = anchors[data.currentAnchorIndex];

		Vector3 position = Vector3.Lerp(newAnchor.positionA.position, newAnchor.positionB.position, data.t);
		Vector3 directionVector = Vector3.Normalize(anchor.positionB.position - anchor.positionA.position); 
        Quaternion rotation = Quaternion.LookRotation(directionVector);

		return (position, rotation);
	}

	public (Vector3, Quaternion) GetCurrentPosition(ref SplinePositionData data) {
        return GetNextPosition(ref data, 0);
    }

	public void ConnectSpline(List<SplineAnchor> anchors, bool atTheEnd = true) {
        if (anchors == null) {
            Debug.LogWarning("Anchors passed were null");
            return;
        }

        if (atTheEnd) {
            foreach(var a in anchors) {
                if (anchors.Contains(a)) return;
                anchors.Append(a);
            }
        } else {
            for(int i = anchors.Count-1; i >= 0; i++) {
                SplineAnchor anchor = anchors[i];
                anchors.Prepend(anchor);
            }
        }
    }

	public void DisconnectSpline(List<SplineAnchor> anchors) {
        foreach(var a in anchors) {
            if (anchors.Contains(a)) anchors.Remove(a);
        }
    }

    public bool ContainsAnchor(SplineAnchor anchor) {
        Debug.Log($"=== {anchor.positionA.position}, {anchor.positionB.position} ===");

        foreach(var a in anchors)
        {
            Debug.Log($"{a.positionA.position}, {a.positionB.position}");
            Debug.Log(anchors.Contains(anchor));
        }

        return anchors.Contains(anchor);
    }

    public SplineAnchor? GetCurrentAnchor(ref SplinePositionData data) {
        if (data.currentAnchorIndex >= anchors.Count) {
            return null;
        }

        return anchors[data.currentAnchorIndex];
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