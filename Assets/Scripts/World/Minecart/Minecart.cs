using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Minecart : IPushable {
    public float forwardspeed = 3f;
    public float reverseSpeed = 2f;
    [Range(0f, 1f)]
    public float positionT = 0f;
    
    public bool pushingForwardsBlocked = false;
    public bool pushingBackwardsBlocked = false;
    
    public SplineContainer rail;
    [HideInInspector] public bool playerFromTheBack;

    private PlayerPushingState player = null;
    private PushObject[] pushHandles;

    private void OnValidate() {
        if (rail != null)
            ResetPosition();
    }

    void Start() {
        player = GameObject.FindWithTag(Tags.PlayerTag).GetComponent<PlayerPushingState>();
        ResetPosition();

        pushHandles = GetComponentsInChildren<PushObject>();
    }

    void LateUpdate() {
        CheckMovement();
    }

    private void CheckMovement() {
        if (!input.Player.enabled) return;
        
        Vector2 move = input.Player.Move.ReadValue<Vector2>();

        if (move.y == 0) return;
        
        bool forward = move.y > 0;
        
        // Reverse if player is set backwards
        forward = side == 0 ? forward : !forward;
        float currentSpeed = move.y > 0 ? forwardspeed : reverseSpeed;
        
        PushCart(currentSpeed, forward);
    }
	public void PushCart(float speed, bool forwards) {
        // Debug.Log(direction);
        if (!forwards && pushingForwardsBlocked) {
            return;
        }

        if (forwards && pushingBackwardsBlocked) {
            return;
        }

        float moveBy = (speed * Time.deltaTime) / rail[0].GetLength();
        positionT += forwards ? moveBy : -moveBy;
        
        positionT = Mathf.Max(positionT, 0);
        positionT = Mathf.Min(positionT, 1);

        ResetPosition();
        SetPlayerPosition();
    }
    public void ResetPosition() {
        Debug.Log("Reset position");
        // https://stackoverflow.com/questions/78315618/change-the-direction-of-rotation-of-the-object
        float3 currentPosition = rail[0].EvaluatePosition(Mathf.Min(positionT, 0.999f));
        float3 nextPosition = rail[0].EvaluatePosition(Mathf.Min(positionT + 0.05f, 1f));
        transform.position = (Vector3)currentPosition + rail.transform.position;
        
        Vector3 direction = nextPosition - currentPosition;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }
    
    // Spline can be rebuild. Reset T in this case
    public void MoveToNewSpline() {
        Debug.Log("Moving to the new spline");
        // https://stackoverflow.com/questions/78315618/change-the-direction-of-rotation-of-the-object
        
        // Debug.Log($"Position: {transform.position}");
        // foreach (BezierKnot k in rail[0]) {
        //     Debug.Log($"BK Position: {k.Position}");
        // }
        SplineUtility.GetNearestPoint(rail[0], 
            transform.localPosition, 
            out float3 currentPosition, 
            out float newT);
        // Debug.Log($"New T: {newT}");
        positionT = newT;
        ResetPosition();
    }

    public void SetPlayerPosition() {
        player.transform.position = player.Follow.transform.position;
        player.transform.rotation = player.Follow.transform.rotation;
    }

    public override void EnterPushingState() {
        input.Player.Enable();
    }

    public override void ExitPushingState() {
        input.Player.Disable();
    }

    public override void EnablePushable(bool enable) {
        foreach (var handle in pushHandles) {
            handle.gameObject.GetComponent<Collider>().enabled = enable;
        }
    }
}