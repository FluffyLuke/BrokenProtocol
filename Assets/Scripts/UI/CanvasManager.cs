
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public UnityEvent TopLevelReturn;
    public Canvas[] Canvases;
    private List<Canvas> quene = new();
    private Canvas topCanvas;
    private Canvas currentCanvas;
    void Start() {
        if(Instance != null) {
            Debug.LogWarning("Two canvas managers detected");
            Destroy(this.gameObject);
        }

        Instance = this;

        topCanvas = Canvases[0];
        currentCanvas = topCanvas;

        foreach(var c in Canvases) {
            c.gameObject.SetActive(false);
        }

        topCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ChangeCurrentCanvas(string canvasName) {
        foreach(Canvas c in Canvases) {
            if(c.name == canvasName) {
                Debug.Log($"Changing to canvas of name: \"{canvasName}\"");
                currentCanvas.gameObject.SetActive(false);
                quene.Add(currentCanvas);
                currentCanvas = c;
                currentCanvas.gameObject.SetActive(true);
                return;
            }
        }
        Debug.LogError($"Cannot find canvas of name: \"{canvasName}\"");
    }

    public void Return() {
        if(quene.Count == 0) {
            Debug.Log("Cannot return further");
            TopLevelReturn.Invoke();
            return;
        }

        currentCanvas.gameObject.SetActive(false);
        currentCanvas = quene.Last();
        quene.Remove(currentCanvas);
        currentCanvas.gameObject.SetActive(true);
    }

    private void OnDestroy() {
        if(Instance == this) {
            Instance = null;
        }    
    }
}
