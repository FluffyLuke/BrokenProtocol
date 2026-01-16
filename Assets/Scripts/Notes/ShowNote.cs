using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ShowNote : MonoBehaviour {
    [SerializeField] private GameObject hide;
    [SerializeField] private TMPWrapper text;
    [SerializeField] private Image noteImage;
    private InputSystem_Actions input;
    void Awake() {
        input = new InputSystem_Actions();
        PlayerEventBus.ShowNote.AddListener(showNote);
    }
    private void showNote(NoteData data) {
        UIText? result = LocalizationManager.instance.GetUIText(data.noteContentID);

        if (result == null) {
            Debug.LogError($"Cannot find note part of id \"{data.noteContentID}\"");
            return;
        }

        hide.SetActive(true);

        PlayerEventBus.EnableCursor.Invoke(false);
        PlayerEventBus.PauseGame.Invoke(true);
        PlayerEventBus.SwitchToCutsceneState.Invoke();

        input.UI.Enable();

        UIText part = (UIText)result;

        noteImage.sprite = data.note;
        text.ShowText(part);
        input.UI.NextNotePart.performed += hideNote;
    }

    private void hideNote(InputAction.CallbackContext context) {
        PlayerEventBus.PauseGame.Invoke(false);
        PlayerEventBus.SwitchToWalkState.Invoke();

        input.UI.Enable();

        hide.SetActive(false);

        input.UI.NextNotePart.performed -= hideNote;
    }
}