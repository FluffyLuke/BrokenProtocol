
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public UnityEvent TopLevelReturn;
    public Canvas[] Canvases;
    private List<Canvas> _quene = new();
    private Canvas _topCanvas;
    private Canvas _currentCanvas;
    void Start() {
        if(Instance != null) {
            Debug.LogWarning("Two canvas managers detected");
            Destroy(this.gameObject);
        }

        Instance = this;

        _topCanvas = Canvases[0];
        _currentCanvas = _topCanvas;

        foreach(var c in Canvases) {
            c.gameObject.SetActive(false);
        }

        _topCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ChangeCurrentCanvas(string canvasName) {
        foreach(Canvas c in Canvases) {
            if(c.name == canvasName) {
                Debug.Log($"Changing to canvas of name: \"{canvasName}\"");
                _currentCanvas.gameObject.SetActive(false);
                _quene.Add(_currentCanvas);
                _currentCanvas = c;
                _currentCanvas.gameObject.SetActive(true);
                return;
            }
        }
        Debug.LogError($"Cannot find canvas of name: \"{canvasName}\"");
    }

    public void Return() {
        if(_quene.Count == 0) {
            Debug.Log("Cannot return further");
            TopLevelReturn.Invoke();
            return;
        }

        _currentCanvas.gameObject.SetActive(false);
        _currentCanvas = _quene.Last();
        _quene.Remove(_currentCanvas);
        _currentCanvas.gameObject.SetActive(true);
    }

    private void OnDestroy() {
        if(Instance == this) {
            Instance = null;
        }    
    }
}
