using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasManager))]
public class PauseCanvas : MonoBehaviour {
    [SerializeField] private IManagedCanvas[] pauseWindows;
    [SerializeField] private int currentIndex = 0;
    private CanvasManager manager;
    private InputSystem_Actions input;
    void Awake() {
        manager = GetComponent<CanvasManager>();

        input = new InputSystem_Actions();
        input.UI.NextPauseWindow.performed += nextWindow;
        input.UI.PreviousPauseWindow.performed += previousWindow;
        input.UI.Disable();
    }

    private void previousWindow(InputAction.CallbackContext context) {
        currentIndex++;
        if (currentIndex >= pauseWindows.Length) currentIndex = 0;

        manager.ChangeCurrentCanvas(pauseWindows[currentIndex].name);
    }
    private void nextWindow(InputAction.CallbackContext context) {
        currentIndex--;
        if (currentIndex < 0) currentIndex = pauseWindows.Length - 1;
        
        manager.ChangeCurrentCanvas(pauseWindows[currentIndex].name);
    }
    public void EnablePauseCanvas() {
        input.UI.Enable();
        gameObject.SetActive(true);
    }

    public void DisablePauseCanvas() {
        input.UI.Disable();
        gameObject.SetActive(false);
    }
}