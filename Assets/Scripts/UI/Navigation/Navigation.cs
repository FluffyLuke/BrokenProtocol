using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Navigation : MonoBehaviour {
    public List<INavigationElement> elements = new();
    private int currentSettingIndex = 0;
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.UI.Navigate.performed += chooseElement;
    }

    void OnDestroy() {
        input.Dispose();
    }
    public void EnableNavigation() {
        input.UI.Navigate.Enable();

        if (elements.Count <= 0) {
            return;
        }

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
        if (elements.Count == 0) return;

        Vector2 value = context.ReadValue<Vector2>();
        if (value.y < 0) currentSettingIndex++;
        else if (value.y > 0) currentSettingIndex--;
        else return;

        if (currentSettingIndex >= elements.Count) currentSettingIndex = 0;
        else if (currentSettingIndex < 0) currentSettingIndex = elements.Count - 1;

        foreach(var s in elements) {
            s.UnselectElement();
        }

        elements[currentSettingIndex].SelectElement();
    }

    public void ClearNavigation() {
        elements.Clear();
    }
    public void AddNavigationElement(INavigationElement element) {
        elements.Add(element);

        if (elements.Count == 1) {
            element.SelectElement();
            currentSettingIndex = 0;
        }
    }
}