using UnityEngine;
using UnityEngine.InputSystem;

public class SkipIntro: MonoBehaviour {
    public DoSomethingAfter[] whatToSkip;
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        input.Debug.SkipCutscene.performed += Skip;
        input.Debug.Enable();
    }
    void Skip(InputAction.CallbackContext context) {
        Debug.Log("Skipping cutscene...");
        foreach(var s in whatToSkip) {
            s.defaultDelay = 0;
            s.Skip();
        }
        input.Debug.Disable();
    }
}