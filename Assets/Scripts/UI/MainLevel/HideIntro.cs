using UnityEngine;

public class HideIntro : MonoBehaviour, IManagedCanvas
{
    public void OnCanvasDisable() {}

    public void OnCanvasEnable() {
        Debug.Log("Dupa 123");
    }
}