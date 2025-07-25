using UnityEngine;
using DG.Tweening;
using System.Collections;
[RequireComponent(typeof(WeaponEventBus))]
public class SimpleShot : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHoleDecal;
    [SerializeField] private GameObject _muzzleFlash;
    private WeaponEventBus _events;
    private Coroutine _flash;
    void Start() {
        _events = GetComponent<WeaponEventBus>();
        _events.weaponFire.AddListener(onFire);
    }

    void onFire(Ray ray, RaycastHit hit, bool ifHit) {
        if(_flash != null) {
            StopCoroutine(_flash);
        }

        _flash = StartCoroutine(flash());

        spawnBulletHole(ray, hit, ifHit);
    }

    private IEnumerator flash() {
        _muzzleFlash.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _muzzleFlash.SetActive(false);
    }

    private void spawnBulletHole(Ray ray, RaycastHit hit, bool ifHit) {
        if(ifHit) {
            Vector3 offsetPosition = hit.point + hit.normal * 0.01f;
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            Instantiate(_bulletHoleDecal, offsetPosition, rotation);
        }
    }
}
