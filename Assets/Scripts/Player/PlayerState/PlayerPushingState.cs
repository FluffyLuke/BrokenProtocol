using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerPushingState : IPlayerState {
    public Transform Follow;
    private IPushable pushable;
    private int side;
	public float FollowPositionTime = 1f;
	void Start() {
        input.Player.Interact.performed += interact;
    }
	public void SetData(Transform target, IPushable pushable, int side) {
		Follow = target;
		this.pushable = pushable;
		this.side = side;
	}
	public override void EnterState() {
		input.Player.Disable();
        base.EnterState();
		transform.DOMove(Follow.position, FollowPositionTime);
		transform
			.DORotate(Follow.rotation.eulerAngles, FollowPositionTime)
			.OnComplete(() => {
                input.Player.Enable();
                pushable.side = side;
                pushable.EnablePushable();
            });
    }

	public override void ExitState() {
		pushable.DisablePushable();
		base.ExitState();
		input.Player.Disable();
	}
	
	private void interact(InputAction.CallbackContext context) {
        PlayerEventBus.SwitchToWalkState.Invoke();
    }
}