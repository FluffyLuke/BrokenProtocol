using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] private CanvasManager canvas;
    public string NextScreen = "MainScreen";
    private InputSystem_Actions input;
    void Awake()
    {
        input = new InputSystem_Actions();
        input.UI.StartGame.performed += OnGameStart;
        input.UI.Enable();
    }

    void OnGameStart(InputAction.CallbackContext context) {
        canvas.ChangeCurrentCanvas(NextScreen);
    }

    private void OnEnable() {
        input.UI.Enable();
    }

    private void OnDisable() {
        input.UI.Disable();
    }
}
