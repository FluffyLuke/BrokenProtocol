using UnityEngine;

public class KillSomething : MonoBehaviour {
    [SerializeField] private GameObject whatToKill;
    void Start() {
        if (whatToKill == null) {
            whatToKill = gameObject;
        }
    }

    public void Kill(float timeOut) {
        Destroy(whatToKill, timeOut);
    }
}