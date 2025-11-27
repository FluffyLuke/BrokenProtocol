using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(ResizeScreen))]
public class MainMenuManager : MonoBehaviour {
    [SerializeField] private ResizeScreen screenResizer;
    [SerializeField] private CanvasManager manager;
    public static UnityEvent StartGame = new();
    public static UnityEvent QuitGame = new();
    void Start() {
        StartGame.AddListener(startGame);
        QuitGame.AddListener(quitGame);
    }
    private void startGame() {
        screenResizer.Close(() => {
            SceneManager.LoadScene(Scenes.MainLevel);
        }, delay: 0.1f);
    }
    public void quitGame() {
        manager.ChangeCurrentCanvas("ExitScreen");
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
