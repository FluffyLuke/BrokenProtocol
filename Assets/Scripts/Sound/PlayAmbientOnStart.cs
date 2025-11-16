using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayAmbientOnStart : MonoBehaviour {
    [SerializeField] private SoundAssetID id;
    void Start() {
        AmbientManager.instance.PlayAmbient(id, 0.5f);
    }
}