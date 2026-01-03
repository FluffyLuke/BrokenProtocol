using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : IManagedCanvas
{
    [SerializeField] private CanvasManager canvas;
    [SerializeField] private Navigation[] panels;
    private InputSystem_Actions input;
    void Awake()
    {
        input = new InputSystem_Actions();
        input.UI.Cancel.performed += ctx => {
            canvas.Return();
        };
        
    }

    public override void OnCanvasEnable() {
        input.UI.Enable();
        panels[0].EnableNavigation();
    }

    public override void OnCanvasDisable() {
        input.UI.Disable();
        panels[0].DisableNavigation();
    }
}
