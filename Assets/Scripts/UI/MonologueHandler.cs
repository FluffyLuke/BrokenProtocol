using UnityEngine;

[RequireComponent(typeof(TMPWrapper))]
public class MonologueHandler : MonoBehaviour {
    private TMPWrapper text;
    void Start() {
        text = GetComponent<TMPWrapper>();

        PlayerEventBus.showMonologue.AddListener(showMonologue);
    }

    private void showMonologue(CharacterDialogue monologue, float fade) {
        if (PlayerEventBus.DialogueIsTakingPlace) {
            Debug.Log("Cannot show monologue - dialogue is taking place.");
            return;
        }

        text.ShowText(monologue, fade);
    }
}