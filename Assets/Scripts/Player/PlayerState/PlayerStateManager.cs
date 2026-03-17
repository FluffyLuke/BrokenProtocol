using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerWalkingState))]
[RequireComponent(typeof(PlayerPushingState))]
[RequireComponent(typeof(PlayerCutsceneState))]
public class PlayerStateManager : MonoBehaviour {
    private PlayerWalkingState walkingState;
    private PlayerPushingState useState;
    private PlayerCutsceneState cutsceneState;
    private IPlayerState currentState;
    public IPlayerFeature[] features;
    void Start() {
        PlayerEventBus.SwitchToPushObjectState.AddListener(switchToPushingState);
        PlayerEventBus.SwitchToWalkState.AddListener(switchToWalkState);
        PlayerEventBus.SwitchToCutsceneState.AddListener(switchToCutsceneState);

        features = GetComponents<IPlayerFeature>();
        turnOffFeatures();

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
        currentState.EnterState();
    }

    private void turnOffFeatures() {
        foreach (var f in features) {
            f.Disable();
        }
    }

    private void turnOnRequiredFeatures(IPlayerState state) {
        foreach (var rf in state.requiredFeatures) {
            foreach (var f in features) {
                if (rf == f.featureName) f.Enable();
            }
        }
    }

    private void switchToPushingState(Transform target, IPushable pushable, int side) {
        //Debug.Log("Player is switching to \"Cart pushing\" state");

        turnOffFeatures();

        currentState.ExitState();
        currentState = useState;
        useState.SetData(target, pushable, side);
        turnOnRequiredFeatures(currentState);
        currentState.EnterState();
    }
    private void switchToWalkState() {
        //Debug.Log("Player is switching to \"Walk\" state");

        turnOffFeatures();

        currentState.ExitState();
        currentState = walkingState;
        turnOnRequiredFeatures(currentState);
        currentState.EnterState();
    }

    private void switchToCutsceneState() {
        //Debug.Log("Player is switching to \"Cutscene\" state");
        
        turnOffFeatures();

        currentState.ExitState();
        currentState = cutsceneState;
        turnOnRequiredFeatures(currentState);
        currentState.EnterState();
    }
}