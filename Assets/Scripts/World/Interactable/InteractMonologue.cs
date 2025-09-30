using UnityEngine;

public class InteractMonologue : MonoBehaviour, IInteractable
{
    public string monologueID;
    private CharacterDialogue monologueText;
    void Start() {
        monologueText = (CharacterDialogue)LocalizationManager.instance.GetDialogue("monologue_sign");
    }
    public void Interact() {
        PlayerEventBus.showMonologue.Invoke(monologueText);
    }
}