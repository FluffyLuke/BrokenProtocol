using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPWrapper : MonoBehaviour 
{
    private TextMeshProUGUI textGUI;
    private Coroutine showTextCoroutine;

    void Awake() {
        textGUI = GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string text, float speed,  int clearAfter = -1) {
        if(showTextCoroutine != null) {
            Debug.Log("Skipped text showing...");
            StopCoroutine(showTextCoroutine);
        }

        showTextCoroutine = StartCoroutine(showText(text, speed, clearAfter));
    }

    public void SetText(CharacterDialogue dialogue, int clearAfter = -1) {
        SetText(dialogue.text, dialogue.speed, clearAfter);
    }

    public void SetText(UIText uiText, int clearAfter = -1) {
        SetText(uiText.text, uiText.speed, clearAfter);
    }

    private IEnumerator showText(string text, float speed, int clearAfter) {
        if (speed <= 0) {
            textGUI.maxVisibleCharacters = int.MaxValue;
            textGUI.text = text;
            yield break;
        }

        float cps = 1f / speed;

        textGUI.text = text;
        textGUI.maxVisibleCharacters = 0;

        foreach(char l in text) {
            textGUI.maxVisibleCharacters += 1;
            
            if (l != ' ') {
                yield return new WaitForSeconds(cps);
            }
        }

        if (clearAfter >= 0) {
            yield return null;
        }

        yield return new WaitForSeconds(clearAfter);
        textGUI.text = "";
    }
}