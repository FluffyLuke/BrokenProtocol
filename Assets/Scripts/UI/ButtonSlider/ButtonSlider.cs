using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class ButtonSlider: MonoBehaviour
{
    [Header("Bars")]
    public int barCount = 5;
    public int barOnCount = 0;

    [Header("References")]
    public GameObject barPrefab;
    public Sprite barOnSprite;
    public Sprite barOffSprite;
    public Transform barContainer;
    // From 0 to 1
    public UnityEvent<float> valueUpdate = new();
    private List<GameObject> bars = new();

    private bool isUpdating = false;

    void Awake() {
        foreach(Transform b in barContainer) {
            bars.Add(b.gameObject);
        }
    }

    private void OnValidate() {
        
        if (isUpdating) return;

        float count = 0;
        foreach(var _ in barContainer) count++;
        if (count == barCount) {
            UpdateBarIcons();
            return;
        }

        isUpdating = true;
        barOnCount = Mathf.Min(barCount, barOnCount);
        UpdateBarLength();
        isUpdating = false;
    }

    private void UpdateBarLength() {
        if (barPrefab == null || barContainer == null) {
            Debug.LogError("There are no needed references.");
            return;
        }

        #if UNITY_EDITOR
        GameObject[] childrenToDestroy = new GameObject[barContainer.childCount];
        for (int i = 0; i < barContainer.childCount; i++) {
            childrenToDestroy[i] = barContainer.GetChild(i).gameObject;
        }
        EditorApplication.delayCall += () => {
            foreach (GameObject child in childrenToDestroy) DestroyImmediate(child.gameObject);
        };
        //foreach (Transform child in barContainer.transform) DestroyImmediate(child.gameObject);
        #else
        foreach (Transform child in barContainer.transform) Destroy(child.gameObject);
        #endif

        bars.Clear();

        // FIX: Fix the warning message about sending message in OnValidate
        #if UNITY_EDITOR
        for (int i = 0; i < barCount; i++) {
            Object b = PrefabUtility.InstantiatePrefab(barPrefab, barContainer);
            bars.Add((GameObject)b);
        }
        #else
        for (int i = 0; i < barCount; i++) {
            var b = Instantiate(barPrefab, barContainer);
            bars.Add(b);
        }
        #endif
        UpdateBarIcons();
    }
    private void UpdateBarIcons() {

        if (bars.Count == 0) {
            foreach(Transform b in barContainer) {
                bars.Add(b.gameObject);
            }
        }

        for(int i = 0; i < barCount; i++) {
            GameObject bar = bars[i];
            if (!bar.TryGetComponent(out Image image)) {
                Debug.LogWarning($"Found object {bar.name} in bar's contents?");
            }
            if (barOnCount > i) {
                //Debug.Log("ON");
                image.sprite = barOnSprite;
            }
            else image.sprite = barOffSprite;
        }
    }
    public void AddBar() {
        barCount++;
        UpdateBarLength();
    }

    public void RemoveBar() {
        barCount = Mathf.Max(0, barCount - 1);
        UpdateBarLength();
    }

    public void IncreaseValue() {
        barOnCount = Mathf.Min(barCount, barOnCount + 1);
        UpdateBarIcons();
        valueUpdate.Invoke((float)barOnCount / (float)barCount);
    }

    public void DecreaseValue() {
        barOnCount = Mathf.Max(0, barOnCount - 1);
        UpdateBarIcons();
        valueUpdate.Invoke((float)barOnCount / (float)barCount);
    }

    // From 0 to 1
    public void SetValue(float percent) {
        percent = Mathf.Max(0, percent);
        percent = Mathf.Min(1, percent);

        barOnCount = (int)(barCount * percent);
        UpdateBarIcons();
    }
}
