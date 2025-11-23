using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPWrapper : MonoBehaviour 
{
    private TextMeshProUGUI textGUI;
    private Coroutine showTextCoroutine;
    private float defaultSpeed = 0; 

    void Awake() {
        textGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text, bool hide = false) {
        textGUI.text = text;
        textGUI.maxVisibleCharacters = hide ? 0 : int.MaxValue;
    }

    public void SetText(CharacterDialogue dialogue, bool hide = false) {
        defaultSpeed = dialogue.speed;
        SetText(dialogue.text, hide);
    }

    public void SetText(UIText uiText, bool hide = false) {
        defaultSpeed = uiText.speed;
        SetText(uiText.text, hide);
    }

    public void ShowText() {
        if(showTextCoroutine != null) {
            Debug.Log("Skipped text showing...");
            StopCoroutine(showTextCoroutine);
        }

        showTextCoroutine = StartCoroutine(showText(defaultSpeed, -1));
    }

    public void ShowText(float speed, int clearAfter = -1) {
        if(showTextCoroutine != null) {
            Debug.Log("Skipped text showing...");
            StopCoroutine(showTextCoroutine);
        }

        if (speed <= 0) {
            showTextCoroutine = StartCoroutine(showText(defaultSpeed, clearAfter));
            return;
        }

        showTextCoroutine = StartCoroutine(showText(speed, clearAfter));
    }

    public void ShowText(CharacterDialogue dialogue, int clearAfter = -1) {
        defaultSpeed = dialogue.speed;
        SetText(dialogue.text);
        ShowText(dialogue.speed, clearAfter);
    }

    public void ShowText(UIText uiText, int clearAfter = -1) {
        defaultSpeed = uiText.speed;
        SetText(uiText.text);
        ShowText(uiText.speed, clearAfter);
    }

    private IEnumerator showText(float speed, int clearAfter) {
        if (speed <= 0) {
            textGUI.maxVisibleCharacters = int.MaxValue;
            yield break;
        }

        float cps = 1f / speed;

        textGUI.maxVisibleCharacters = 0;

        foreach(char l in textGUI.text) {
            textGUI.maxVisibleCharacters += 1;
            
            if (l != ' ') {
                yield return new WaitForSeconds(cps);
            }
        }

        if (clearAfter <= 0) {
            yield break;
        }
        
        yield return new WaitForSeconds(clearAfter);
        textGUI.text = "";
    }
}