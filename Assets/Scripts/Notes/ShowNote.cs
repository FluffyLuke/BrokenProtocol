using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ShowNote : MonoBehaviour {
    [SerializeField] private GameObject hide;
    [SerializeField] private TMPWrapper text;
    [SerializeField] private Image background;
    private InputSystem_Actions input;
    private int index = 0;
    private List<UIText> noteParts = new();
    void Awake() {
        input = new InputSystem_Actions();
        input.UI.NextNotePart.performed += nextPart;
        PlayerEventBus.ShowNote.AddListener(showNote);
    }
    private void showNote(NoteData data) {
        if (data.textIDs.Length == 0) {
            Debug.LogError("There is no text in the note, skipping...");
            return;
        }

        hide.SetActive(true);

        PlayerEventBus.EnableCursor.Invoke(false);
        PlayerEventBus.PauseGame.Invoke(true);
        PlayerEventBus.SwitchToCutsceneState.Invoke();

        index = 0;
        input.UI.Enable();

        foreach(string notePartID in data.textIDs) {
            UIText? result = LocalizationManager.instance.GetUIText(notePartID);

            if (result == null) {
                Debug.LogError($"Cannot find note part of id \"{notePartID}\"");
            }

            UIText part = (UIText)result;
            noteParts.Add(part);
        }

        background.sprite = data.background;
        text.ShowText(noteParts[0]);
    }

    private void hideNote() {
        PlayerEventBus.EnableCursor.Invoke(true);
        PlayerEventBus.PauseGame.Invoke(false);
        PlayerEventBus.SwitchToWalkState.Invoke();

        input.UI.Enable();

        hide.SetActive(false);
    }

    private void nextPart(InputAction.CallbackContext context) {
        index++;
        if (index >= noteParts.Count) {
            hideNote();
            return;
        }

        UIText part = noteParts[index]; 
        text.ShowText(part);
    }
}