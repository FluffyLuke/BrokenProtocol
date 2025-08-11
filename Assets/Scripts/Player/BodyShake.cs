using Unity.Mathematics;
using UnityEngine;

public class BodyShake : MonoBehaviour
{
    [Header("Intensivity")]
    [Range(0.1f, 2f)]
    public float WalkMultiplier = 1.0f;
    [Range(0.1f, 2f)]
    public float RunningMultiplier = 1.3f;
    [Header("Values")]
    public float Frequency = 10.0f;
    public float Amount = 0.015f;
    private Animator _playerAnimator;
    private Vector3 _startingPos;
    void Start() {
        _startingPos = transform.localPosition;
        _playerAnimator = GameObject.FindGameObjectWithTag(Tags.PlayerTag).GetComponent<Animator>();
    }
    void Update() {
        AnimatorStateInfo info = _playerAnimator.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("Standing")) {
            stopShake();
        } else if(info.IsName("Walking")) {
            shake(WalkMultiplier);
        } else if(info.IsName("Running")) {
            shake(RunningMultiplier);
        }
    }

    private void shake(float multiplier) {
        float speedMultiplier = (_playerAnimator.GetFloat("Speed") / 2000f) + 1;


        Vector3 pos = Vector3.zero;

        pos.y += Mathf.Cos(Time.time * Frequency * multiplier) * Amount;
        pos.x += Mathf.Sin(Time.time * Frequency * multiplier/ 2) * Amount * 2;

        // pos.y += Mathf.Lerp(transform.localPosition.x, Mathf.Sin(Time.time * Frequency * multiplier) * Amount, Time.deltaTime * Frequency);
        // pos.x += Mathf.Lerp(transform.localPosition.x, Mathf.Sin(Time.time * Frequency * multiplier / 2) * Amount * 2, Time.deltaTime * Frequency);

        transform.localPosition = pos;
    }

    private void stopShake() {
        if(transform.localPosition == _startingPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, _startingPos, Time.deltaTime * Frequency);
    }
}
