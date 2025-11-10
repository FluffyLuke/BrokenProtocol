using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour {
    [SerializeField] private SoundAsset[] sounds;
    private Dictionary<SoundAssetID, SoundAsset> lookup;
    public static SoundManager instance;

    void Awake() {
        lookup = sounds.ToDictionary(s => s.id);

        if (instance != null) {
            Destroy(gameObject);
        }
        
        instance = this;
    }

    public bool PlayAndLoop(SoundAssetID id, Vector3 position, out SoundHandle handle) {
        handle = default;
        if (!lookup.TryGetValue(id, out SoundAsset sound))
        {
            Debug.LogError($"Cannot found asset of id: \"{id}\"");
            return false;
        }

        GameObject gameObject = new GameObject("SoundSource");
        gameObject.transform.position = position;
        gameObject.transform.SetParent(this.transform);

        AudioSource source = gameObject.AddComponent<AudioSource>();
        AudioClip clip = sound.GetRandomClip();
        source.resource = clip;
        source.pitch = Random.Range(sound.pitchRange.x, sound.pitchRange.y);
        source.volume = sound.volume;
        source.loop = true;
        source.Play();

        handle = new SoundHandle(source);

        return true;
    }

    public bool PlayOneShot(SoundAssetID id, Vector3 position) {
        if (!lookup.TryGetValue(id, out SoundAsset sound))
        {
            Debug.LogError($"Cannot found asset of id: \"{id}\"");
            return false;
        }

        GameObject gameObject = new GameObject("SoundSource");
        gameObject.transform.position = position;
        gameObject.transform.SetParent(this.transform);

        AudioSource source = gameObject.AddComponent<AudioSource>();
        AudioClip clip = sound.GetRandomClip();
        source.pitch = Random.Range(sound.pitchRange.x, sound.pitchRange.y);
        source.volume = sound.volume;
        source.PlayOneShot(clip);
        
        Destroy(gameObject, clip.length);
        return true;
    }
}