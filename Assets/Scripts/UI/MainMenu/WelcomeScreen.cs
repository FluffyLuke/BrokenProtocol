using UnityEngine;
using UnityEngine.InputSystem;

public class WelcomeScreen : MonoBehaviour
{
    public string NextScreen = "MainScreen";
    private InputSystem_Actions _input;
    void Awake()
    {
        _input = new InputSystem_Actions();
        _input.UI.StartGame.performed += OnGameStart;
        _input.UI.Enable();
    }

    void OnGameStart(InputAction.CallbackContext context) {
        CanvasManager.Instance.ChangeCurrentCanvas(NextScreen);
    }

    private void OnEnable() {
        _input.UI.Enable();
    }

    private void OnDisable() {
        _input.UI.Disable();
    }
}
