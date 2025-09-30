using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameLanguage {
    English,
}

[CreateAssetMenu(menuName = "Misc/GlobalSettings")]
public class GlobalSettings : ScriptableObject
{
    [Serializable]
    private struct GameLanguagePath {
        public GameLanguage lang;
        public string path;
    }
    [SerializeField] private List<GameLanguagePath> pathToLocals = new();
    public GameLanguage currentLanguage = GameLanguage.English;
    public GameLanguage defaultLanguage = GameLanguage.English;
    public string GetPathToLocals(GameLanguage language) {
        foreach(GameLanguagePath l in pathToLocals) {
            if (l.lang == language) {
                return l.path;
            }
        }
        return null;
    }
    public string GetPathToLocals() {
        return GetPathToLocals(currentLanguage);
    }
}