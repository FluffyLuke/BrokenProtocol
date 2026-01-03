using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsScreenSetting : INavigationElement {
    [SerializeField] private Slider slider;
    [SerializeField] private Image image;
    public Color activateColor;

    void Start() {
        input.UI.Navigate.performed += updateSlider;
    }
    public override void SelectElement() {
        image.color = activateColor;
        input.UI.Navigate.Enable();
    }
    public override void UnselectElement() {
        image.color = new Color(0,0,0,0);
        input.UI.Navigate.Disable();
    }
    private void updateSlider(InputAction.CallbackContext context) {
        Vector2 value = context.ReadValue<Vector2>();
        if (value.x > 0) slider.IncreaseValue();
        else if (value.x < 0) slider.DecreaseValue();
    }
}