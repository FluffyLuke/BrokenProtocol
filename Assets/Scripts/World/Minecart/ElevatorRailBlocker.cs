using UnityEngine;
public class ElevatorRailBlocker : MonoBehaviour, InteractableRequirement {
    [SerializeField] private Elevator elevator;
    public bool CheckRequirement() {
        return elevator.elevatorState != ElevatorState.Moving;
    }

    public string GetRequirementName()
    {
        return "Cart not blocking";
    }
}