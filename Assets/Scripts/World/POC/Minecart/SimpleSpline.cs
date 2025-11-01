using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

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

    public static int GetAnchorIndex(List<SplineAnchor> otherAnchors, SplineAnchor targetAnchor) {
        int index = 0;
        foreach(var a in otherAnchors) {
            if (a == targetAnchor) {
                return index;
            }
            index++;
        }
        return -1;
    }

    public bool ContainsAnchor(SplineAnchor otherAnchor) {
        return anchors.Contains(otherAnchor);
    }

    public (bool, SplineAnchor) GetCurrentAnchor(ref SplinePositionData data) {
        if (data.currentAnchorIndex >= anchors.Count) {
            return (false, default);
        }

        return (true, anchors[data.currentAnchorIndex]);
    }
}

[Serializable]
public struct SplineAnchor {
    public Transform positionA;
	public Transform positionB;

    public SplineAnchor(Transform positionA, Transform positionB)
    {
        this.positionA = positionA;
        this.positionB = positionB;
    }

    public float GetLength() {
        return Vector3.Distance(positionA.position, positionB.position);
    }

    public static bool operator ==(SplineAnchor a1, SplineAnchor a2) {
        if (a1.positionA == a2.positionB && a1.positionB == a2.positionA) {
            Debug.LogWarning("Anchor has the same points, but in reverse order. This should not have happened!");
        }
        if (a1.positionA == a2.positionA && a1.positionB == a2.positionB) {
            return true;
        }
        return false;
    }
    public static bool operator !=(SplineAnchor a1, SplineAnchor a2) {
        if (a1.positionA != a2.positionA || a1.positionB != a2.positionB) {
            return true;
        }
        return false;
    }

    public override bool Equals(System.Object obj) {
        if (obj == null || !(obj is SplineAnchor))
            return false;
        SplineAnchor other = (SplineAnchor) obj;
        if (this.positionA == other.positionB && this.positionB == other.positionA) {
            Debug.LogWarning("Anchor has the same points, but in reverse order. This should not have happened!");
        }
        if (this.positionA == other.positionA && this.positionB == other.positionB) {
            return true;
        }
        return false;
    }
}