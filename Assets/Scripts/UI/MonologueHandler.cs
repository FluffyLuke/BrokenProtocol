using UnityEngine;

[RequireComponent(typeof(TMPWrapper))]
public class MonologueHandler : MonoBehaviour {
    private TMPWrapper text;
    void Start() {
        text = GetComponent<TMPWrapper>();

        PlayerEventBus.showMonologue.AddListener(showMonologue);
    }

    private void showMonologue(CharacterDialogue dialogue) {
        text.SetText(dialogue, 3);
    }
}