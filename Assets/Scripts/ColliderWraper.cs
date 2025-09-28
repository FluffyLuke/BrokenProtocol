using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderWraper : MonoBehaviour 
{
    public bool IsColliding {
        get;
        private set;
    }
    public bool IsTriggering {
        get;
        private set;
    }
    public UnityEvent<Collision> OnCollisionEnterEvent = new();
    public UnityEvent<Collision> OnCollisionExitEvent = new();
    public UnityEvent<Collider> OnTriggerEnterEvent = new();
    public UnityEvent<Collider> OnTriggerExitEvent = new();
    void Start()
    {
        GetComponent<Collider>();
    }
    void OnCollisionEnter(Collision collision) {
        IsColliding = true;
        OnCollisionEnterEvent.Invoke(collision);
    }
    void OnCollisionExit(Collision collision) {
        IsColliding = false;
        OnCollisionExitEvent.Invoke(collision);
    }
    void OnTriggerEnter(Collider other) {
        IsTriggering = true;
        OnTriggerEnterEvent.Invoke(other);
    }
    void OnTriggerExit(Collider other) {
        IsTriggering = false;
        OnTriggerExitEvent.Invoke(other);
    }
}