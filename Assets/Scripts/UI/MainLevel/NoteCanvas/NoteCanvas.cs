using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Navigation))]
public class NoteCanvas : IManagedCanvas {
    [SerializeField] private GameObject noteNavElementPrefab;
    [SerializeField] private GameObject noteNavElementContainer;
    [SerializeField] private TMPWrapper noteContent;
    private Navigation nav;
    private InputSystem_Actions input;
    private Dictionary<string, NoteData> collectedNotes = new Dictionary<string, NoteData>();
    void Awake() {
        input = new InputSystem_Actions();
        nav = GetComponent<Navigation>();
        PlayerEventBus.ShowNote.AddListener(addNote);
    }
    void Start() {
        nav.DisableNavigation();
    }
    public override void OnCanvasEnable() {
        input.UI.Enable();
        gameObject.SetActive(true);

        setNotes();
        nav.EnableNavigation();
    }

    public override void OnCanvasDisable() {
        input.UI.Disable();
        gameObject.SetActive(false);
        nav.DisableNavigation();
    }

    private void addNote(NoteData data) {
        if (collectedNotes.ContainsKey(data.noteNameID)) {
            return;
        }

        UIText? result_name = LocalizationManager.instance.GetUIText(data.noteNameID);
        UIText? result_content = LocalizationManager.instance.GetUIText(data.noteContentID);

        if (result_name == null || result_content == null) {
            Debug.LogError($"Cannot find note content or name of id \"{data.noteNameID}\" \\ \"{data.noteContentID}\"");
            return;
        }

        UIText name = (UIText)result_name;
        collectedNotes.Add(data.noteNameID, data);

        Debug.Log($"Saving new note: \"{name.text}\"");
    }


    private void setNotes() {
        foreach (Transform child in noteNavElementContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }
        nav.ClearNavigation();

        if (collectedNotes.Count <= 0) {
            noteContent.SetText("No notes found...");
            return;
        }

        foreach (var p in collectedNotes) {
            string key = p.Key;
            NoteData value = p.Value;

            UIText result_name = (UIText)LocalizationManager.instance.GetUIText(value.noteNameID);
            UIText result_content = (UIText)LocalizationManager.instance.GetUIText(value.noteContentID);

            NoteNavElement noteElement = Instantiate(noteNavElementPrefab, noteNavElementContainer.transform).GetComponent<NoteNavElement>();
            noteElement.SetData(noteContent, result_name.text, result_content.text);

            nav.AddNavigationElement(noteElement);
        }
    }
}