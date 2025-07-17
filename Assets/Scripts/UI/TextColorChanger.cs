using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
public class TextColorChanger : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI targetText;
    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;

    private bool isHighlighted = false;
    private bool isSelected = false;
    private Button _button;
    void Start() {
        _button = GetComponent<Button>();
    }

    private void updateColor() {
        if (isSelected)
            targetText.color = selectedColor;
        else
            targetText.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!isSelected && EventSystem.current.currentSelectedGameObject != gameObject) {
            _button.Select();
        }
    }

    public void OnSelect(BaseEventData eventData) {
        isSelected = true;
        updateColor();
    }

    public void OnDeselect(BaseEventData eventData) {
        isSelected = false;
        updateColor();
    }
}
