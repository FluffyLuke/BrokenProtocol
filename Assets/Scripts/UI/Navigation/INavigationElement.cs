using System;
using UnityEngine;
public abstract class INavigationElement : MonoBehaviour {
    public abstract void SelectElement();
    public abstract void UnselectElement();
    protected InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
    }
}