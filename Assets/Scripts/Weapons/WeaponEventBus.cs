using UnityEngine;
using UnityEngine.Events;

public class WeaponEventBus : MonoBehaviour
{
    public UnityEvent<RaycastHit, bool> weaponFire = new();
    public UnityEvent weaponFireWithoutData = new();
    public UnityEvent weaponReloading = new();
    public UnityEvent weaponReloaded = new();
}
