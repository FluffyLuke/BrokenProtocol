using System;
using System.Collections;
using UnityEngine;

public class DialogueHandler : MonoBehaviour {
    [SerializeField] private TMPWrapper dialogueText;
    [SerializeField] private TMPWrapper speakerText;
    private Coroutine currentDialogueCoroutine = null;
    void Start() {
        PlayerEventBus.ShowDialogue.AddListener(showDialogue);
    }

    private void showDialogue(CharacterDialoguePart[] dialogue) {
        if (currentDialogueCoroutine != null) {
            Debug.Log("Skipping current dialogue");
            StopCoroutine(currentDialogueCoroutine);
        }
        currentDialogueCoroutine = StartCoroutine(lockDialogue(dialogue));
    }

    private IEnumerator lockDialogue(CharacterDialoguePart[] dialogue) {
        PlayerEventBus.DialogueIsTakingPlace = true;

        foreach (var d in dialogue) {
            speakerText.SetText(d.dialogue.speakerName);
            dialogueText.ShowText(d.dialogue, -1);
            
            yield return new WaitForSeconds(d.dialogue.ShowingTime() + d.fade);
        }

        speakerText.SetText("");
        dialogueText.SetText("");

        PlayerEventBus.DialogueIsTakingPlace = false;        
    }
}

[Serializable]
public class CharacterDialoguePart {
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