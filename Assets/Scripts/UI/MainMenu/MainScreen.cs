using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour, IManagedCanvas
{
    [SerializeField] private CanvasManager canvas;
    private InputSystem_Actions input;
    void Awake()
    {
        input = new InputSystem_Actions();
        input.UI.Cancel.performed += ctx => {
            canvas.ChangeCurrentCanvas("WelcomeScreen");
        };
        input.UI.Enable();
    }

    public void StartGame() {
        input.UI.Disable();
        MainMenuManager.StartGame.Invoke();
    }

    public void QuitGame() {
        input.UI.Disable();
        MainMenuManager.QuitGame.Invoke();
    }

    public void OnCanvasEnable() {
        input.UI.Enable();
    }

    public void OnCanvasDisable() {
        input.UI.Disable();
    }
}
