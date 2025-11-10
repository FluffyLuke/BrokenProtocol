using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/SoundAsset")]
public class SoundAsset : ScriptableObject
{
    public SoundAssetID id = SoundAssetID.NotDefined;
    public AudioClip[] clips;
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);
    public float volume = 1f;

    public AudioClip GetRandomClip()
        => clips[UnityEngine.Random.Range(0, clips.Length)];
}

public enum SoundAssetID {
    NotDefined,
    // Player
    PlayerWalkSnow,
    PlayerRunSnow,
}