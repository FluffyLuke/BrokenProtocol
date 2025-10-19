using System;
using UnityEngine;

[RequireComponent(typeof(PlayerWalkingState))]
[RequireComponent(typeof(PlayerPushingState))]
public class PlayerStateManager : MonoBehaviour {
    private PlayerWalkingState walkingState;
    private PlayerPushingState useState;
    private IPlayerState currentState;
    void Awake() {
        PlayerEventBus.PushMinecart.AddListener(switchToPushingState);
        PlayerEventBus.SwitchToWalkState.AddListener(switchToWalkState);

        walkingState = GetComponent<PlayerWalkingState>();
        useState = GetComponent<PlayerPushingState>();
    }
    void Start() {
        currentState = walkingState;
        currentState.EnterState();
    }
    private void switchToPushingState(Transform target) {
        Debug.Log("Player is switching to \"Cart pushing\" state");
        currentState.ExitState();
        currentState = useState;
        useState.SetData(target);
        currentState.EnterState();
    }
    private void switchToWalkState() {
        Debug.Log("Player is switching to \"Walk\" state");
        currentState.ExitState();
        currentState = walkingState;
        currentState.EnterState();
    }
}