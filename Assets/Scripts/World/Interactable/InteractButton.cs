using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractButton : IInteractable
{
    [SerializeField] private bool state = false;
    public UnityEvent<bool> earlyStateChange = new();
    public float statechangeCooldown = 0f; 
    public UnityEvent<bool> stateChange = new();
    public UnityEvent failedToInteract = new();
    public UnityEvent turnedOn = new();
    public UnityEvent turnedOff = new();
    private Coroutine stateCooldownCoroutine = null;
    public override void Interact() {
        foreach (var r in requirements) {
            if (r.CheckRequirement() == false) {
                Debug.Log($"Requirement \"{r.GetRequirementName()}\" was not satisfied, not interacting...");
                failedToInteract.Invoke();
                return;
            }
        }

        interactedWith.Invoke();

        ChangeState();
    }
    public bool IsTurnedOn() {
        return state;
    }
    public void ChangeState(bool newState) {
        if(newState == state) return;

        earlyStateChange.Invoke(newState);

        if(stateCooldownCoroutine != null) {
            StopCoroutine(stateCooldownCoroutine);
            return;
        }
        
        StartCoroutine(changeStateCooldown(newState, statechangeCooldown));
    }
    private IEnumerator changeStateCooldown(bool newState, float cooldown) {
        yield return new WaitForSeconds(cooldown);
        state = newState;
        if (newState == true) turnedOn.Invoke();
        else turnedOff.Invoke();
        stateChange.Invoke(newState);
        stateCooldownCoroutine = null;
    }
    #region Helper functions
    public void ChangeState() {
        ChangeState(!state);
    }
    public void TurnOff() {
        ChangeState(false);
    }
    public void TurnOn() {
        ChangeState(true);
    }

    #endregion
}