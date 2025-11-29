using System;
using System.Collections;
using UnityEngine;

public class ShowDialogueWrapper : MonoBehaviour {
    [SerializeField] private DialoguePart[] dialogues;
    private Coroutine showDialogueCoroutine;

    void Start() {
        foreach(var d in dialogues) {
            d.GetDialogue();
        }
    }

    public void ShowDialogue() {
        if (showDialogueCoroutine != null) {
            StopCoroutine(showDialogueCoroutine);
        }

        showDialogueCoroutine = StartCoroutine(showDialogue());
    }

    private IEnumerator showDialogue() {
        foreach(var d in dialogues) {
            PlayerEventBus.showDialogue.Invoke(d.dialogue,d.fade);
            yield return new WaitForSeconds(d.dialogue.ShowingTime() + d.fade);
        }
        showDialogueCoroutine = null;
    }
}

[Serializable]
public class DialoguePart {
    public string dialogueID;
    public float fade;
    [HideInInspector] public CharacterDialogue dialogue;
    public void GetDialogue() {
        var d = LocalizationManager.instance.GetDialogue(dialogueID);

        if (d == null) {
            Debug.LogError($"Cannot find dialogue of id \"{dialogueID}\"");
        }

        dialogue = (CharacterDialogue)d;
        Debug.Log(dialogue.text);
    }
}