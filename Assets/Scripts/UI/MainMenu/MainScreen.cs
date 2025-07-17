using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    private InputSystem_Actions _input;
    void Awake()
    {
        _input = new InputSystem_Actions();
        _input.UI.Cancel.performed += ctx => {
            CanvasManager.Instance.ChangeCurrentCanvas("WelcomeScreen");
        };
        _input.UI.Enable();
    }

    private void OnEnable() {
        _input.UI.Enable();
    }

    private void OnDisable() {
        _input.UI.Disable();
    }

}
