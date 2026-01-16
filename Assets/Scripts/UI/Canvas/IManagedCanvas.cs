using UnityEngine;
using UnityEngine.Events;

public abstract class IManagedCanvas : MonoBehaviour {
    public GameObject content;
    public UnityEvent enabledCanvas = new();
    public UnityEvent disabledCanvas = new();
    public abstract void OnCanvasEnable();
    public abstract void OnCanvasDisable();
} 