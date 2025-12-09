using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    public GameObject barOnPrefab;
    public GameObject barOffPrefab;
    public Transform barContainer;
    private UnityEvent valueUpdate = new();

    private bool isUpdating = false;

    private void OnValidate() {
        
        if (isUpdating) return;

        isUpdating = true;
        barOnCount = Mathf.Min(barCount, barOnCount);
        UpdateBars();
        isUpdating = false;
    }

    private void UpdateBars() {
        if (barOnPrefab == null || barOffPrefab == null || barContainer == null) {
            Debug.LogError("There are no needed references.");
            return;
        }

        foreach (Transform child in barContainer.transform)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += () => {DestroyImmediate(child.gameObject);};
            #else
            Destroy(child.gameObject);
            #endif
        }

        // ON bars
        for (int i = 0; i < barOnCount; i++) {
            #if UNITY_EDITOR
            EditorApplication.delayCall += () => {
                PrefabUtility.InstantiatePrefab(barOnPrefab, barContainer);
            };
            #else
            Instantiate(barOnPrefab, barContainer);
            #endif
        }

        // OFF bars
        for (int i = 0; i < barCount - barOnCount; i++) {
            #if UNITY_EDITOR
            EditorApplication.delayCall += () => {
                PrefabUtility.InstantiatePrefab(barOffPrefab, barContainer);
            };
            #else
            Instantiate(barOffPrefab, barContainer);
            #endif
        }
    }
    public void AddBar() {
        barCount++;
        UpdateBars();
    }

    public void RemoveBar() {
        barCount = Mathf.Max(0, barCount - 1);
        UpdateBars();
    }

    public void IncreaseValue() {
        barOnCount = Mathf.Min(barCount, barOnCount + 1);
        UpdateBars();
        valueUpdate.Invoke();
    }

    public void DecreaseValue() {
        barOnCount = Mathf.Max(0, barOnCount - 1);
        UpdateBars();
        valueUpdate.Invoke();
    }
}
