using UnityEngine;

public class OpenPauseWindow : MonoBehaviour
{
    [SerializeField] private PauseCanvas pause;
    [SerializeField] private ScreenVolumeManager volume;
    //[SerializeField] private string nameOfPauseCanvas;
    void Awake() {
        PlayerEventBus.OpenPause.AddListener(openWindow);
        PlayerEventBus.isPauseOpened = false;
    }

    public void openWindow(bool shouldOpen) {
        if (shouldOpen) {
            PlayerEventBus.isPauseOpened = true;
            pause.gameObject.SetActive(true);
            pause.EnablePauseCanvas();
            volume.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else {
            PlayerEventBus.isPauseOpened = false;
            pause.gameObject.SetActive(false);
            pause.DisablePauseCanvas();
            volume.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        //manager.ChangeCurrentCanvas(nameOfPauseCanvas);
    }
}