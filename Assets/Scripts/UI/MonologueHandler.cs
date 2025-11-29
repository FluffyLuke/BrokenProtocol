using UnityEngine;

[RequireComponent(typeof(TMPWrapper))]
public class MonologueHandler : MonoBehaviour {
    private TMPWrapper text;
    void Start() {
        text = GetComponent<TMPWrapper>();

        PlayerEventBus.showMonologue.AddListener(showMonologue);
    }

    private void showMonologue(CharacterDialogue monologue, float fade) {
        text.ShowText(monologue, fade);
    }
}