using UnityEngine;
using UnityEngine.Events;

public class HitDetector : MonoBehaviour {
    public UnityEvent gotHit = new();

    public void OnHit() {
        gotHit.Invoke();
    }
}
