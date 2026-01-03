using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Navigation : MonoBehaviour {
    [SerializeField] private INavigationElement[] elements;
    private int currentSettingIndex = 0;
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.UI.Navigate.performed += chooseElement;
    }
    public void EnableNavigation() {
        input.UI.Navigate.Enable();

        currentSettingIndex = 0;
        elements[0].SelectElement();
    }
    public void DisableNavigation() {
        input.UI.Navigate.Disable();
        foreach(var e in elements) {
            e.UnselectElement();
        }
    }
    private void chooseElement(InputAction.CallbackContext context) {
        Vector2 value = context.ReadValue<Vector2>();
        if (value.y < 0) currentSettingIndex++;
        else if (value.y > 0) currentSettingIndex--;
        else return;

        if (currentSettingIndex >= elements.Length) currentSettingIndex = 0;
        else if (currentSettingIndex < 0) currentSettingIndex = elements.Length - 1;

        foreach(var s in elements) {
            s.UnselectElement();
        }

        elements[currentSettingIndex].SelectElement();
    }
}