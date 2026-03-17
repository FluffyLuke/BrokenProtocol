using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(WeaponEventBus))]
public class SimpleShot : MonoBehaviour {
    [SerializeField] private GameObject bulletHoleDecal;
    [SerializeField] private GameObject muzzleFlash;
    private GameObject decalsHolder = null;
    private WeaponEventBus events;
    private Coroutine flash;
    void Start() {
        events = GetComponent<WeaponEventBus>();
        events.weaponFire.AddListener(onFire);
        decalsHolder = GameObject.FindWithTag(Tags.DecalsHolderTag);
    }

    void onFire(RaycastHit hit, bool ifHit) {
        if(flash != null) {
            StopCoroutine(flash);
        }

        flash = StartCoroutine(spawnFlash());

        spawnBulletHole(hit, ifHit);
    }

    private IEnumerator spawnFlash() {
        muzzleFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    private void spawnBulletHole(RaycastHit hit, bool ifHit) {
        if(ifHit) {
            Vector3 offsetPosition = hit.point + hit.normal * 0.01f;
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            GameObject instance = Instantiate(bulletHoleDecal, offsetPosition, rotation);

            if (decalsHolder != null) {
                instance.transform.parent = decalsHolder.transform;
            }

            HitDetector detector = hit.transform.GetComponent<HitDetector>();
            if (detector != null) {
                detector.OnHit();
            }
        }
    }
}
