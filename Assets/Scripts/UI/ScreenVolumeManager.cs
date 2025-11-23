using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class ScreenVolumeManager : MonoBehaviour {
    private Volume volume;
    private Bloom bloom;
    float bloomStartIntensivity;
    private LensDistortion lensDistortion;
    float lensStartIntensivity;

    void Start() {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out lensDistortion);

        bloomStartIntensivity = bloom.intensity.value;
        lensStartIntensivity = lensDistortion.intensity.value;
    }

    public void DisableVolume(float t = 0) {
        manageVolume(t, false);
    }

    public void EnableVolume(float t = 0) {
        manageVolume(t, true);
    }

    private IEnumerator manageVolume(float t, bool enable) {
        float elapsed = 0;

        if (enable) {
            while (elapsed < t) {
                elapsed += Time.deltaTime;
                bloom.intensity.value = Mathf.Lerp(0, bloomStartIntensivity, elapsed/t);
                lensDistortion.intensity.value = Mathf.Lerp(0, lensStartIntensivity, elapsed/t);
            }

            bloom.intensity.value = bloomStartIntensivity;
            lensDistortion.intensity.value = lensStartIntensivity;
        } else {
            while (elapsed < t) {
                elapsed += Time.deltaTime;
                bloom.intensity.value = Mathf.Lerp(bloomStartIntensivity, 0, elapsed/t);
                lensDistortion.intensity.value = Mathf.Lerp(lensStartIntensivity, 0, elapsed/t);
            }

            bloom.intensity.value = 0;
            lensDistortion.intensity.value = 0;
        }

        yield return null;
    }
}