using UnityEngine;

public class DialogueHandler : MonoBehaviour {
    [SerializeField] private TMPWrapper dialogueText;
    [SerializeField] private TMPWrapper speakerText;
    void Start() {
        PlayerEventBus.showDialogue.AddListener(showDialogue);
    }

    private void showDialogue(CharacterDialogue dialogue, float fade) {
        speakerText.SetText(dialogue.speakerName);
        dialogueText.ShowText(dialogue, fade);
    }
}