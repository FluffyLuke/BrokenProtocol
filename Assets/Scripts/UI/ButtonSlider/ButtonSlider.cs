using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    private UnityEvent valueUpdate = new();

    private bool isUpdating = false;

    private void OnValidate() {
        
        if (isUpdating) return;

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

        GameObject[] newBars = new GameObject[barCount];

        // FIX: Fix the warning message about sending message in OnValidate
        #if UNITY_EDITOR
        for (int i = 0; i < barCount; i++) {
            Object b = PrefabUtility.InstantiatePrefab(barPrefab, barContainer);
            newBars[i] = b.GetComponent<Transform>().gameObject;
        }
        #else
        for (int i = 0; i < barCount; i++) {
            var b = Instantiate(barPrefab, barContainer);
            newBars[i] = b;
        }
        #endif
        UpdateBarIcons(newBars);
    }

    private void UpdateBarIcons() {
        GameObject[] bars = new GameObject[barContainer.childCount];
        for (int i = 0; i < barContainer.childCount; i++) {
            bars[i] = barContainer.GetChild(i).gameObject;
        }
        UpdateBarIcons(bars);
    }

    private void UpdateBarIcons(GameObject[] bars) {
        for(int i = 0; i < barCount; i++) {
            GameObject bar = bars[i];
            if (!bar.TryGetComponent(out Image image)) {
                Debug.LogWarning($"Found object {bar.name} in bar's contents?");
            }
            
            if (barOnCount > i) image.sprite = barOnSprite;
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
        valueUpdate.Invoke();
    }

    public void DecreaseValue() {
        barOnCount = Mathf.Max(0, barOnCount - 1);
        UpdateBarIcons();
        valueUpdate.Invoke();
    }
}
