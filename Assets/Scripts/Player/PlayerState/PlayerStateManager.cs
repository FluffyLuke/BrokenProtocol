using System;
using UnityEngine;

[RequireComponent(typeof(PlayerWalkingState))]
[RequireComponent(typeof(PlayerPushingState))]
[RequireComponent(typeof(PlayerCutsceneState))]
public class PlayerStateManager : MonoBehaviour {
    private PlayerWalkingState walkingState;
    private PlayerPushingState useState;
    private PlayerCutsceneState cutsceneState;
    private IPlayerState currentState;
    void Awake() {
        PlayerEventBus.PushMinecart.AddListener(switchToPushingState);
        PlayerEventBus.SwitchToWalkState.AddListener(switchToWalkState);
        PlayerEventBus.SwitchToCutsceneState.AddListener(switchToCutsceneState);

        walkingState = GetComponent<PlayerWalkingState>();
        useState = GetComponent<PlayerPushingState>();
        cutsceneState = GetComponent<PlayerCutsceneState>();

        IPlayerState[] states = {
            walkingState,
            useState,
            cutsceneState,
        };

        int statesEnabled = 0;
        foreach (var state in states) {
            if (state.enabled) statesEnabled += 1;
            currentState = state;
        }

        if (statesEnabled == 0) {
            Debug.LogError("No default state was enabled - defaulting to \"Walk\" state");
            currentState = walkingState;
        }

        if (statesEnabled > 1) {
            Debug.LogError("To many states enabled - defaulting to \"Walk\" state");
            foreach (var state in states) {
                state.enabled = false;
            }
            currentState = walkingState;
        }
    }
    void Start() {
        currentState.EnterState();
        setCursor();
    }

    private void setCursor() {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
    }
    private void switchToPushingState(Transform target, Minecart targetMinecart) {
        Debug.Log("Player is switching to \"Cart pushing\" state");
        currentState.ExitState();
        currentState = useState;
        useState.SetData(target, targetMinecart);
        currentState.EnterState();
    }
    private void switchToWalkState() {
        Debug.Log("Player is switching to \"Walk\" state");
        currentState.ExitState();
        currentState = walkingState;
        currentState.EnterState();
    }

    private void switchToCutsceneState() {
        Debug.Log("Player is switching to \"Cutscene\" state");
        currentState.ExitState();
        currentState = cutsceneState;
        currentState.EnterState();
    }
}