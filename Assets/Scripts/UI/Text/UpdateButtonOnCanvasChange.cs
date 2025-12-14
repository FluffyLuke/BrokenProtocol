using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Button can change color of it's background, but not text.
// This utility class sloves the issue.

[RequireComponent(typeof(Button))]
// [RequireComponent(typeof(TextColorChanger))]
public class UpdateButtonOnCanvasChange : MonoBehaviour
{
    private TextColorChanger textColor;
    [SerializeField] private IManagedCanvas canvas; 
    [SerializeField] private EventSystem events;
    void Start() {
        textColor = GetComponent<TextColorChanger>();

        canvas.disabledCanvas.AddListener(() => {
            events.SetSelectedGameObject(null);
            textColor.isSelected = false;
            textColor.UpdateColor();
        });
    }
}
