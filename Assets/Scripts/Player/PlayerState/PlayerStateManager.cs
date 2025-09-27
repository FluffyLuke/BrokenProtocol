using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    [Serializable]
    public struct StateOption {
        public PlayerState.PossibleStates StateEnum;
        public PlayerState State;
    } 
    public StateOption[] StateOptions;
    private PlayerState currentState;

    void Awake() {
        PlayerEventBus.ChangeState.AddListener(changeState);
    }
    void Start() {
        currentState = StateOptions[0].State;
    }

    private void changeState(PlayerState.PossibleStates state) {
        foreach(StateOption s in StateOptions) {
            if (s.StateEnum != state) continue;

            currentState.enabled = false;
            currentState = s.State;
            currentState.enabled = true;
            break;
        }
    }
}