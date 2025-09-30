using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
public class LocalizationManager : MonoBehaviour 
{
    [SerializeField] private string fileName;
    [SerializeField] private GlobalSettings globalSettings;
    private Localization localization;
    [HideInInspector] public static LocalizationManager instance = null;
    void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Two localization managers detected...");
            Destroy(gameObject);
            return;
        }
        instance = this;

        LoadLocals();
    }

    public void LoadLocals(GameLanguage language) {
        string pathToResource = globalSettings.GetPathToLocals(language);
        pathToResource = $"{pathToResource}/{fileName}";
        TextAsset json = Resources.Load<TextAsset>(pathToResource);
        if (json == null) {
            Debug.LogError($"Cannot load file with localization: \"{pathToResource}\"");
            return;
        }

        localization = JsonConvert.DeserializeObject<Localization>(json.text);
    }

    public void LoadLocals() {
        LoadLocals(globalSettings.currentLanguage);
    }

    public CharacterDialogue? GetDialogue(string id) {
        foreach(var d in localization.dialogues) {
            if (d.id == id) {
                return d;
            }
        }
        Debug.LogWarning($"Cannot find dialogue of id: \"{id}\"");
        return null;
    }

    public UIText? GetUIText(string id) {
        foreach(var t in localization.ui) {
            if (t.id == id) {
                return t;
            }
        }
        Debug.LogWarning($"Cannot find ui text of id: \"{id}\"");
        return null;
    }
}