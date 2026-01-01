using UnityEngine;

public class OpenPauseWindow : MonoBehaviour
{
    [SerializeField] private PauseCanvas pause;
    [SerializeField] private ScreenVolumeManager volume;
    private bool opened = false;
    //[SerializeField] private string nameOfPauseCanvas;
    void Awake() {
        PlayerEventBus.OpenPause.AddListener(openWindow);
    }

    public void openWindow() {
        opened = !opened;

        PlayerEventBus.PauseGame.Invoke(opened);
        PlayerEventBus.EnableCursor.Invoke(opened);

        if (opened) {
            pause.gameObject.SetActive(true);
            pause.EnablePauseCanvas();
            volume.gameObject.SetActive(true);
        } else {
            pause.gameObject.SetActive(false);
            pause.DisablePauseCanvas();
            volume.gameObject.SetActive(false);
        }
        //manager.ChangeCurrentCanvas(nameOfPauseCanvas);
    }
}