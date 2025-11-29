using Unity.Cinemachine;
using UnityEngine;

public class PlayerCinemachineInputWrapper : MonoBehaviour {
    public static CinemachineInputAxisController Controller;
    [SerializeField] private CinemachineInputAxisController _controller;

    void Awake() {
        Controller = _controller;
    }
}