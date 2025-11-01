using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerPushingState : IPlayerState {
	private Minecart minecart;
    private Transform follow;
	public float moveToPositionTime = 1f;
	void Start() {
        input.Player.Interact.performed += interact;
    }
	public void SetData(Transform target, Minecart targetMinecart) {
        follow = target;
		minecart = targetMinecart;
    }
	public override void EnterState() {
		input.Player.Disable();
        base.EnterState();
		transform.DOMove(follow.position, moveToPositionTime);
		transform
			.DORotate(follow.rotation.eulerAngles, moveToPositionTime)
			.OnComplete(() => {
                input.Player.Enable();
            });
    }

	public override void ExitState() {
		base.ExitState();
		input.Player.Disable();
	}

	void Update() {
		Vector2 direction = input.Player.Move.ReadValue<Vector2>();

		if (direction.y > 0) {
            minecart.PushForward();
			transform.position = follow.position;
			transform.rotation = follow.rotation;
        } else if (direction.y < 0) {
            minecart.PushBackward();
			transform.position = follow.position;
			transform.rotation = follow.rotation;
        } else if (input.Player.enabled) {
            transform.position = follow.position;
			transform.rotation = follow.rotation;
        }
    }

	private void interact(InputAction.CallbackContext context) {
        PlayerEventBus.SwitchToWalkState.Invoke();
    }
}