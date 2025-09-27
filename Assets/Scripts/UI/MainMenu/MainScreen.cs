using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    private InputSystem_Actions input;
    void Awake()
    {
        input = new InputSystem_Actions();
        input.UI.Cancel.performed += ctx => {
            CanvasManager.Instance.ChangeCurrentCanvas("WelcomeScreen");
        };
        input.UI.Enable();
    }

    private void OnEnable() {
        input.UI.Enable();
    }

    private void OnDisable() {
        input.UI.Disable();
    }

}
