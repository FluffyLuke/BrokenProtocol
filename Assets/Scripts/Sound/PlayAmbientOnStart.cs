using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayAmbientOnStart : MonoBehaviour {
    [SerializeField] private SoundAssetID id;
    [SerializeField] private float delay = 5f;
    [SerializeField] private float fadeDuration = 3f;
    void Start() {
        StartCoroutine(playAmbient());
    }

    private IEnumerator playAmbient() {
        yield return new WaitForSeconds(delay);
        AmbientManager.instance.PlayAmbient(id, fadeDuration);
    }
}