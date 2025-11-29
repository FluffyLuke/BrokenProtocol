using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

[RequireComponent(typeof(ResizeScreen))]
public class ResizeScreenHelper : MonoBehaviour
{
    public ResizeScreen screen;
    public void Awake() {
        screen = GetComponent<ResizeScreen>();
    }

    public void Open() {
        screen.Open();
    }

    public void Close() {
        screen.Close();
    }
}
