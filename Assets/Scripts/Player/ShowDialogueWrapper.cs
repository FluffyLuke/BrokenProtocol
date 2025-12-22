using System;
using System.Collections;
using UnityEngine;

public class ShowDialogueWrapper : MonoBehaviour {
    [SerializeField] private CharacterDialoguePart[] dialogue;
    private Coroutine showDialogueCoroutine;

    void Start() {
        foreach(var d in dialogue) {
            d.GetDialogue();
        }
    }

    public void ShowDialogue() {
        PlayerEventBus.ShowDialogue.Invoke(dialogue);
    }
}