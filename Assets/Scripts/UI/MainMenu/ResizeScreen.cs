using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

public class ResizeScreen : MonoBehaviour
{
    public RectTransform Screen;
    public CanvasManager Manager;
    public void Open() {
        Debug.Log(Screen);
        Vector2 previousSize = Screen.sizeDelta;
        Screen.sizeDelta = new(previousSize.x, 0);
        float fromY = 0;
        DOTween.To(() => fromY, y => fromY = y, previousSize.y, 0.2f).SetDelay(0.5f)
            .OnUpdate(() => {
                Screen.sizeDelta = new(previousSize.x, fromY);
            });
    }

    public void Close(Action onComplete) {
        Debug.Log(Screen);
        Vector2 previousSize = Screen.sizeDelta;
        float fromY = previousSize.y;
        Manager.ChangeCurrentCanvas("ExitScreen");
        DOTween.To(() => fromY, y => fromY = y, 0, 0.2f).SetDelay(0.5f)
            .OnUpdate(() => {
                Screen.sizeDelta = new(previousSize.x, fromY);
            })
            .OnComplete(() => {onComplete();});
    }
}
