
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
public class CanvasManager : MonoBehaviour
{
    public UnityEvent TopLevelReturn = new();
    public UnityEvent canvasChange = new();
    public Canvas[] Canvases;
    private List<Canvas> quene = new();
    private Canvas topCanvas;
    private Canvas currentCanvas;
    void Start() {
        topCanvas = Canvases[0];
        currentCanvas = topCanvas;

        foreach(var c in Canvases) {
            if (c.TryGetComponent<IManagedCanvas>(out var managedCanvas)) {
                managedCanvas.content.SetActive(false);
            } else {
                c.gameObject.SetActive(false);
            }
        }
        
        if (topCanvas.TryGetComponent<IManagedCanvas>(out var managedCanvas2)) {
            managedCanvas2.OnCanvasEnable();
            managedCanvas2.content.SetActive(true);
        } else {
            topCanvas.gameObject.SetActive(true);
        }
    }

    public void ChangeCurrentCanvas(string canvasName) {
        foreach(Canvas c in Canvases) {
            if(c.name == canvasName) {
                Debug.Log($"Changing to canvas of name: \"{canvasName}\"");
                if (currentCanvas.TryGetComponent<IManagedCanvas>(out var managedCanvas)) {
                    managedCanvas.OnCanvasDisable();
                    managedCanvas.disabledCanvas.Invoke();
                    managedCanvas.content.SetActive(false);
                } else {
                    currentCanvas.gameObject.SetActive(false);
                }
                quene.Add(currentCanvas);
                currentCanvas = c;
                if (currentCanvas.TryGetComponent<IManagedCanvas>(out managedCanvas)) {
                    managedCanvas.OnCanvasEnable();
                    managedCanvas.enabledCanvas.Invoke();
                    managedCanvas.content.SetActive(true);
                } else {
                    currentCanvas.gameObject.SetActive(true);
                }

                canvasChange.Invoke();
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

        if (currentCanvas.TryGetComponent<IManagedCanvas>(out var managedCanvas)) {
            managedCanvas.OnCanvasDisable();
            managedCanvas.disabledCanvas.Invoke();
            managedCanvas.content.SetActive(false);
        } else {
            currentCanvas.gameObject.SetActive(false);
        }

        currentCanvas = quene.Last();
        quene.Remove(currentCanvas);

        if (currentCanvas.TryGetComponent(out managedCanvas)) {
            managedCanvas.OnCanvasEnable();
            managedCanvas.enabledCanvas.Invoke();
            managedCanvas.content.SetActive(true);
        } else {
            currentCanvas.gameObject.SetActive(true);
        }

        canvasChange.Invoke();
    }

    public void EnableCurrentCanvas(bool v) {
        if (v) {
            if (currentCanvas.TryGetComponent<IManagedCanvas>(out var managedCanvas)) {
                managedCanvas.OnCanvasEnable();
                managedCanvas.enabledCanvas.Invoke();
                managedCanvas.content.SetActive(true);
            } else {
                currentCanvas.gameObject.SetActive(true);
            }
        } else {
            if (currentCanvas.TryGetComponent<IManagedCanvas>(out var managedCanvas)) {
                managedCanvas.OnCanvasDisable();
                managedCanvas.disabledCanvas.Invoke();
                managedCanvas.content.SetActive(false);
            } else {
                currentCanvas.gameObject.SetActive(false);
            }
        }
    }
}
