using System.Data.SqlTypes;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Minecart : MonoBehaviour {
    public float speed = 3f;
    public float reverseSpeed = 2f;
    [SerializeField] private SplinePositionData posData;
    public SimpleSpline rail;
    public bool playerFromTheBack;
    void Start() {
		(Vector3 position, Quaternion rotation) = rail.GetNextPosition(ref posData, 0);
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
        (Vector3 position, Quaternion rotation) = rail.GetNextPosition(ref posData, speed, direction);
		transform.position = position;
		transform.rotation = rotation;
    }
}