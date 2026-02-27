using Unity.Mathematics;
using UnityEngine;

public class BodyShake : MonoBehaviour
{
    [Header("Intensivity")]
    [Range(0.1f, 5f)]
    public float WalkingMultiplier = 1.0f;
    [Range(0.1f, 5f)]
    public float RunningMultiplier = 1.3f;
    [Header("Values")]
    public float Frequency = 10.0f;
    public float weaponWalkingBobMaxX = 0.015f;
    public float weaponWalkingBobMaxY = 0.015f;
    public float weaponRunningBobMaxX = 0.015f;
    public float weaponRunningBobMaxY = 0.015f;
    private float weaponBobT = 0;
    private MovementState currentMovementState = MovementState.Standing;
    private Animator playerAnimator;
    private Vector3 originalBobPivotPosition;
    void Start() {
        originalBobPivotPosition = transform.localPosition;
        playerAnimator = GameObject.FindGameObjectWithTag(Tags.PlayerTag).GetComponent<Animator>();
    }
    void Update() {
        AnimatorStateInfo info = playerAnimator.GetCurrentAnimatorStateInfo(0);

        float currentMultiplier = 1;
        float currentMaxX = 0.015f;
        float currentMaxY = 0.015f;

        if(info.IsName("Standing")) {
            currentMovementState = MovementState.Standing;
            currentMultiplier = 1;
            currentMaxX = 0.015f;
            currentMaxY = 0.015f;
        } else if(info.IsName("Walking")) {
            currentMovementState = MovementState.Walking;
            currentMultiplier = WalkingMultiplier;
            currentMaxX = weaponWalkingBobMaxX;
            currentMaxY = weaponWalkingBobMaxY;
        } else if(info.IsName("Running")) {
            currentMovementState = MovementState.Running;
            currentMultiplier = RunningMultiplier;
            currentMaxX = weaponRunningBobMaxX;
            currentMaxY = weaponRunningBobMaxY;
        }

        shake(currentMultiplier, currentMaxX, currentMaxY);
    }

    private void shake(float multiplier, float maxX, float maxY) {
        float previousT = weaponBobT;

        if (currentMovementState == MovementState.Standing) {
            // if (previousT % 1 > 0.25) {
            //     if (previousT + Time.deltaTime > Mathf.Ceil(previousT)) {
            //     weaponBobT = Mathf.Ceil(previousT);
            //     } else {
            //         weaponBobT += Time.deltaTime * multiplier;
            //     }
            // }
            if (previousT + Time.deltaTime > Mathf.Ceil(previousT)) {
                weaponBobT = Mathf.Ceil(previousT);
            } else {
                weaponBobT += Time.deltaTime * multiplier;
            }
        } else {
            weaponBobT += Time.deltaTime * multiplier;
        }
        weaponBobT %= 2;

        float x = Mathf.Sin(weaponBobT * Mathf.PI) * maxX;
        float y = Mathf.Cos(weaponBobT * Mathf.PI * 2) * maxY;

        transform.localPosition = new Vector3 (x, y, 0) + originalBobPivotPosition;
    }

    // private void stopShake() {
    //     if(transform.localPosition == startingPos) return;
    //     transform.localPosition = Vector3.Lerp(transform.localPosition, startingPos, Time.deltaTime * Frequency);
    // }
}
