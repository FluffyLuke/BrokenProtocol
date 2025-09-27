using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerStateManager))]
public abstract class PlayerState : MonoBehaviour {
    public enum PossibleStates {
        Walk,
        Fall,
    }
}