using DG.Tweening;
using UnityEngine;
public class PositionFollow : MonoBehaviour {
    public Transform Follow = null;
    public string FindByTag = null;
    public Vector3 Delta = Vector3.zero;
    void Start() {
        if (Follow == null && FindByTag == null) {
            Debug.LogError("No target to follow!");
            return;
        }

        if (Follow != null && FindByTag != null) {
            Debug.LogWarning("Target to follow is set, but also tag to find. Using the current target.");
            return;
        }

        Follow = GameObject.FindWithTag(Tags.PlayerTag).transform;
    }

    void Update() {
        if (Follow == null) return;

        Vector3 newPos = Follow.position + Delta;
        transform.position = newPos;
    }
}