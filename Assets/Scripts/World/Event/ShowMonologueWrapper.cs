using UnityEngine;
public class ShowMonologueWrapper : MonoBehaviour {
    public string id;
    public float fade = 3;
    private CharacterDialogue text;
    void Start() {
        text = (CharacterDialogue)LocalizationManager.instance.GetDialogue(id);
    }
    public void ShowMonologue() {
        PlayerEventBus.ShowMonologue.Invoke(text, fade);
    }
}