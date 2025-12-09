using UnityEngine;

public class ChangeCanvasWrapper : MonoBehaviour {
    [SerializeField] private CanvasManager manager;
    public string canvasName = "";
    public void ChangeCanvas(string name) {
        manager.ChangeCurrentCanvas(name);
    }

    public void ChangeCanvas() {
        manager.ChangeCurrentCanvas(canvasName);
    }
}