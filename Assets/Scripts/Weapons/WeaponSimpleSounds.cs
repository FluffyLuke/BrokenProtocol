using UnityEngine;
[RequireComponent(typeof(WeaponEventBus))]
public class WeaponSounds : MonoBehaviour
{
    public string fireID;
    public string reloadID;
    private WeaponEventBus _events;
    void Start() {
        _events = GetComponent<WeaponEventBus>();
        _events.weaponFire.AddListener(onFire);
        _events.weaponReload.AddListener(onReload);
    }

    void onFire(RaycastHit hit, bool ifHit) {
        SoundManager.instance.PlayOneShot(fireID, Vector3.zero);
    }

    void onReload() {
        SoundManager.instance.PlayOneShot(reloadID, Vector3.zero);
    }
}
