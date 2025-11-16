using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
public class RotationFollow : MonoBehaviour {
    public Transform Follow = null;
    public string FindByTag = null;
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
        transform.rotation = Follow.rotation;
    }
}