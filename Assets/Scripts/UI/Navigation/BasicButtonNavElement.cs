using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BasicButtonNavElement : INavigationElement {
    public UnityEvent buttonPressed = new();
    [Header("Config")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    public Color selectedColorBackground;
    public Color selectedColorText;
    private Color defaultColorBackground;
    private Color defaultColorText;
    void Start() {
        defaultColorBackground = image.color;
        defaultColorText = text.color;

        input.UI.ButtonPressed.performed += pressButton;
    }
    public override void SelectElement() {
        image.color = selectedColorBackground;
        text.color = selectedColorText;
        input.UI.Navigate.Enable();
        input.UI.ButtonPressed.Enable();
    }
    public override void UnselectElement() {
        image.color = defaultColorBackground;
        text.color = defaultColorText;
        input.UI.Navigate.Disable();
        input.UI.ButtonPressed.Disable();
    }

    private void pressButton(InputAction.CallbackContext context) {
        buttonPressed.Invoke();
    }
}