using UnityEngine;

public class OpenPauseWindow : MonoBehaviour
{
    [SerializeField] private CanvasManager manager;
    [SerializeField] private ScreenVolumeManager volume;
    //[SerializeField] private string nameOfPauseCanvas;
    void Awake() {
        PlayerEventBus.OpenPause.AddListener(openWindow);
        PlayerEventBus.isPauseOpened = false;
    }

    public void openWindow(bool shouldOpen) {
        if (shouldOpen) {
            PlayerEventBus.isPauseOpened = true;
            manager.gameObject.SetActive(true);
            volume.gameObject.SetActive(true);
            Time.timeScale = 0;
        } else {
            PlayerEventBus.isPauseOpened = false;
            manager.gameObject.SetActive(false);
            volume.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        //manager.ChangeCurrentCanvas(nameOfPauseCanvas);
    }
}