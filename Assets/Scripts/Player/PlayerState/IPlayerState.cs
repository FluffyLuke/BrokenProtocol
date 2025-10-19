using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
}