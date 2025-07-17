using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class ResizeScreen : MonoBehaviour
{
    public RectTransform Screen;
    void Start()
    {
        Debug.Log(Screen);
        Vector2 previousSize = Screen.sizeDelta;
        Screen.sizeDelta = new(previousSize.x, 0);
        float fromY = 0;
        DOTween.To(() => fromY, y => fromY = y, previousSize.y, 0.2f).SetDelay(0.5f)
            .OnUpdate(() =>
            {
                Debug.Log("DUPA");
                Screen.sizeDelta = new(previousSize.x, fromY);
            });
    }
}
