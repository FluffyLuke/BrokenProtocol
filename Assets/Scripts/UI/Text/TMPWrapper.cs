using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPWrapper : MonoBehaviour 
{
    private TextMeshProUGUI textGUI;
    private Coroutine showTextCoroutine;
    private float defaultSpeed = 0; 
    public UnityEvent CharacterInserted = new();

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

    public void ShowText(float speed, float clearAfter = -1) {
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

    public void ShowText(string text, float speed = 10, float clearAfter = -1) {
        defaultSpeed = speed;
        SetText(text);
        ShowText(speed, clearAfter);
    }

    public void ShowText(CharacterDialogue dialogue, float clearAfter = -1) {
        defaultSpeed = dialogue.speed;
        SetText(dialogue.text);
        ShowText(dialogue.speed, clearAfter);
    }

    public void ShowText(UIText uiText, float clearAfter = -1) {
        defaultSpeed = uiText.speed;
        SetText(uiText.text);
        ShowText(uiText.speed, clearAfter);
    }

    private IEnumerator showText(float speed, float clearAfter) {
        if (speed <= 0) {
            textGUI.maxVisibleCharacters = int.MaxValue;
            if (clearAfter <= 0) {
                yield break;
            }
            yield return new WaitForSeconds(clearAfter);
            textGUI.text = "";
            yield break;
        }

        float cps = 1f / speed;

        textGUI.maxVisibleCharacters = 0;

        foreach(char l in textGUI.text) {
            textGUI.maxVisibleCharacters += 1;
            CharacterInserted.Invoke();
            
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