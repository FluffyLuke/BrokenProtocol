using System;
using System.Collections;
using UnityEngine;

public class NoteDisappear : MonoBehaviour {
    public float appearSpeed = 1f;
    [Header("References")]
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject star;
    private Material starMaterial;
    private Coroutine currentCoroutine;

    // State
    bool tooClose = false, tooFar = true;
    void Start() {
        starMaterial = star.GetComponent<Renderer>().material;
    }

    void Update() {
        star.transform.LookAt(point.transform.position);
    }

    public void TooClose(bool state) {
        tooClose = state;
        checkState();
    }

    public void TooFar(bool state) {
        tooFar = state;
        checkState();
    }

    private void checkState() {
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        if (tooClose || tooFar) {
            StartCoroutine(disappear());
        } else {
            StartCoroutine(appear());
        }
    }

    private IEnumerator disappear() {
        float currentPercentage = starMaterial.GetFloat("_Percent");

        currentPercentage -= Time.deltaTime / appearSpeed;
        while (currentPercentage > 0) {
            starMaterial.SetFloat("_Percent", currentPercentage);
            currentPercentage -= Time.deltaTime * appearSpeed;
            yield return null;
        }
        starMaterial.SetFloat("_Percent", 0);
        currentCoroutine = null;
    }

    private IEnumerator appear() {
        float currentPercentage = starMaterial.GetFloat("_Percent");

        currentPercentage += Time.deltaTime / appearSpeed;
        while (currentPercentage < 1) {
            starMaterial.SetFloat("_Percent", currentPercentage);
            currentPercentage += Time.deltaTime * appearSpeed;
            yield return null;
        }
        starMaterial.SetFloat("_Percent", 1);
        currentCoroutine = null;
    }
}