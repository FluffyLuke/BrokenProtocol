using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmbienceManage : MonoBehaviour {
    [SerializeField] private SoundAsset[] ambience;
    private Dictionary<SoundAssetID, SoundAsset> lookup;
    [SerializeField] private AudioSource source;
    private Coroutine fadeCoroutine = null;
    public AmbienceManage instance;
    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;

        lookup = ambience.ToDictionary(s => s.id);
    }
    void Start() {
        if (source == null) {
            source = GetComponentInChildren<AudioSource>();
            if (source == null) {
                Debug.Log("Ambience manager has no audio source");
                return;
            }
        }
    }

    public bool PlayAmbience(SoundAssetID id, float fadeDuration) {
        if (!lookup.TryGetValue(id, out SoundAsset sound))
        {
            Debug.LogError($"Cannot found asset of id: \"{id}\"");
            return false;
        }

        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(fade(sound, fadeDuration));

        return true;
    }

    public IEnumerator fade(SoundAsset sound, float fadeDuration) {
        float startValue = source.volume;
        float elapsed = 0f;
  
        // Fade out
        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startValue, 0f, elapsed / fadeDuration);
            yield return null;
        }
        source.volume = 0;

        // New ambience
        AudioClip clip = sound.GetRandomClip();
        source.clip = clip;
        source.pitch = Random.Range(sound.pitchRange.x, sound.pitchRange.y);
        source.volume = 0;
        source.loop = true;
        source.Play();

        // Fade in
        elapsed = 0f;
        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, sound.volume, elapsed / fadeDuration);
            yield return null;
        }
        source.volume = sound.volume;
        fadeCoroutine = null;
    }
}