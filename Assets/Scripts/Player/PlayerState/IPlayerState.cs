using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerStateManager))]
public abstract class IPlayerState : MonoBehaviour {
    protected InputSystem_Actions input = null;
    void Awake() {
        input = new InputSystem_Actions();
    }
	public virtual void EnterState() {
        enabled = true;
    }
    public virtual void ExitState() {
        enabled = false;
    }

    protected void CinemachineInput(bool enable) {
        PlayerCinemachineInputWrapper.Controller.enabled = enable;
    }
}