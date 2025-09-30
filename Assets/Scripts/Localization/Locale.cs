using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public struct CharacterDialogue {
    [JsonProperty("id")] 
    public string id;
    [JsonProperty("speaker_name")] 
    public string speakerName;
    [JsonProperty("text")] 
    public string text;
    [JsonProperty("Speed")] 
    public float speed;
}

[System.Serializable]
public struct UIText {
    [JsonProperty("id")]
    public string id;
    [JsonProperty("text")]
    public string text;
    [JsonProperty("Speed")]
    public float speed;
}

[System.Serializable]
public struct Localization {
    [JsonProperty("file_id")] public string fileID;
    [JsonProperty("file_lang")] public string lang;
    [JsonProperty("dialogues")] public CharacterDialogue[] dialogues;
    [JsonProperty("ui")] public UIText[] ui;
}