using UnityEngine;

public class PauseGame : MonoBehaviour {
    void Start() {
        PlayerEventBus.PauseGame.AddListener(pauseGame);
    }

    private void pauseGame(bool value) {
        PlayerEventBus.isPaused = value;
        Time.timeScale = value ? 0 : 1;
    }
}