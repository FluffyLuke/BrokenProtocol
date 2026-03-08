using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class MinecartBlock : MonoBehaviour
{
    private Minecart minecart; 
    [SerializeField] private bool isThisABackSide;
    void Start() {
        minecart = GetComponentInParent<Minecart>();
    }

    void OnTriggerEnter(Collider other) {
        bool condition1 = other.GetComponentInParent<Minecart>() == null;
        bool condition2 = !other.CompareTag(Tags.BumperTag);

        if (condition1 || condition2) return;

        if (isThisABackSide) {
            minecart.pushingForwardsBlocked = true;
        } else {
            minecart.pushingBackwardsBlocked = true;
        }
    }

    void OnTriggerExit(Collider other) {
        bool condition1 = other.GetComponentInParent<Minecart>() == null;
        bool condition2 = !other.CompareTag(Tags.BumperTag);

        if (condition1 || condition2) return;

        if (isThisABackSide) {
            minecart.pushingForwardsBlocked = false;
        } else {
            minecart.pushingBackwardsBlocked = false;
        }
    }
}
