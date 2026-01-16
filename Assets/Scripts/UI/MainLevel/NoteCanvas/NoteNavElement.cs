using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NoteNavElement : INavigationElement {
    private string noteName;
    private string noteContent;
    private TMPWrapper noteContentPanel;
    [Header("Config")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI noteLabel;
    public Color normalColor;
    public Color selectedColor;
    void Awake() {
        background.color = normalColor;
    }

    public override void SelectElement() {
        background.color = selectedColor;
        noteContentPanel.SetText(noteContent);
        //input.UI.Enable();
    }

    public override void UnselectElement() {
        background.color = normalColor;
        //input.UI.Disable();
    }

    public void SetData(TMPWrapper noteContentPanel, string noteName, string noteContent) {
        this.noteName = noteName;
        this.noteContent = noteContent;
        this.noteContentPanel = noteContentPanel;

        noteLabel.SetText(noteName);
    }
}