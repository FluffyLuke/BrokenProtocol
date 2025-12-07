using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour {
    [SerializeField] private SoundAsset[] sounds;
    private Dictionary<string, SoundAsset> lookup;
    [SerializeField] private AudioMixerGroup masterAudioGroup;
    [SerializeField] private AudioMixerGroup sfxAudioGroup;
    [SerializeField] private AudioMixerGroup ambientAudioGroup;
    public static SoundManager instance;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;

        lookup = sounds.ToDictionary(s => s.id);
    }

    void Start() {
        if (masterAudioGroup == null)
            Debug.LogError("Master audio group is not assigned!");

        if (sfxAudioGroup == null)
            Debug.LogError("SFX group is not assigned!");

        if (ambientAudioGroup == null)
            Debug.LogError("Ambient group is not assigned!");
    }

    public bool PlayAndLoop(string id, Vector3 position, out SoundHandle handle) {
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
        
        switch (sound.busID) {
            case AudioBusID.NotDefined:
                source.outputAudioMixerGroup = masterAudioGroup;
                break;
            case AudioBusID.Ambient:
                source.outputAudioMixerGroup = ambientAudioGroup;
                break;
            case AudioBusID.SFX:
                source.outputAudioMixerGroup = sfxAudioGroup;
                break;
            default:
                Debug.LogError("wtf?");
                break;
        }

        source.Play();

        handle = new SoundHandle(source);

        return true;
    }

    public bool PlayOneShot(string id, Vector3 position) {
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