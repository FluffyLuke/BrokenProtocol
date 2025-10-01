using UnityEngine;

public class InteractMonologue : IInteractable
{
    public string monologueID;
    private CharacterDialogue monologueText;
    void Start() {
        monologueText = (CharacterDialogue)LocalizationManager.instance.GetDialogue("monologue_sign");
    }
    public override void Interact() {
        PlayerEventBus.showMonologue.Invoke(monologueText);
    }
}