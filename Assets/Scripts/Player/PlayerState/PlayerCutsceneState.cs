using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerCutsceneState : IPlayerState
{
    [Header("Rotation")]
    [SerializeField] private CinemachinePanTilt cameraTilt;

    public override void EnterState() {
        base.EnterState();
        CinemachineInput(false);
    }

    public override void ExitState() {
        base.ExitState();
    }
    
    public void SetRotation(float x, float y) {
        cameraTilt.TiltAxis.Value = x;

        transform.eulerAngles = new Vector3(
            transform.rotation.x, 
            y, 
            transform.rotation.z
        );
    }
}
