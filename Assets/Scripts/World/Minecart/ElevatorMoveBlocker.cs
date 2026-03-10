using UnityEngine;
public class ElevatorMoveBlocker : MonoBehaviour, InteractableRequirement {
    [SerializeField] private Elevator elevator;
    public bool CheckRequirement() {
        // foreach (var c in elevator.carts) {
            // (bool _, SplineAnchor currentAnchor) = c.rail.GetCurrentAnchor(ref c.posData);
            // foreach (var ca in elevator.railConnection.connectionAnchors) {
            //     if (ca.anchor == currentAnchor) {
            //         return false;
            //     }
            // }
        // }

        return true;
    }

    public string GetRequirementName()
    {
        return "Elevator not blocking";
    }
}