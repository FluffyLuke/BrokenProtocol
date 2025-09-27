using UnityEngine;
using UnityEngine.InputSystem;

public class WelcomeScreen : MonoBehaviour
{
    public string NextScreen = "MainScreen";
    private InputSystem_Actions input;
    void Awake()
    {
        input = new InputSystem_Actions();
        input.UI.StartGame.performed += OnGameStart;
        input.UI.Enable();
    }

    void OnGameStart(InputAction.CallbackContext context) {
        CanvasManager.Instance.ChangeCurrentCanvas(NextScreen);
    }

    private void OnEnable() {
        input.UI.Enable();
    }

    private void OnDisable() {
        input.UI.Disable();
    }
}
