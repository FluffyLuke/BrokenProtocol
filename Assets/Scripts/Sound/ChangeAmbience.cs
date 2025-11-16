using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChanceAmbience : MonoBehaviour {
    public Collider collider1, collider2;
    public SoundAssetID Ambience1, Ambience2;
    public float FadeDuration = 1f;
    void Start() {
        if (collider1.enabled == false && collider2.enabled == false) {
            Debug.LogWarning("Both colliders are turned off on ambience changer.");
        }

        if (collider1.enabled == true && collider2.enabled == true) {
            collider2.enabled = false;
            Debug.LogWarning("Both colliders are turned on on ambience changer. Turning one off.");
        }
    }
    public void PlayAmbience1() {
        Debug.Log("A");
        collider1.enabled = false;
        collider2.enabled = true;
        playAmbience(Ambience1);
    }
    public void PlayAmbience2() {
        Debug.Log("B");
        collider1.enabled = true;
        collider2.enabled = false;
        playAmbience(Ambience2);
    }

    private void playAmbience(SoundAssetID id) {
        AmbienceManager.instance.PlayAmbience(id, FadeDuration);
    }
}