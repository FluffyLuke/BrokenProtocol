using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ResizeScreen))]
public class MainMenuManager : MonoBehaviour {
    private ResizeScreen screenResizer; 
    void Start() {
        screenResizer = GetComponent<ResizeScreen>();
        screenResizer.Open();
    }
    public void StartGame() {
        screenResizer.Close(() => {
            SceneManager.LoadScene(Scenes.MainLevel);
        });
    }
    public void Quit() {
        screenResizer.Close(() => {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
            #else
                Application.Quit();
            #endif
        });
    }
}
