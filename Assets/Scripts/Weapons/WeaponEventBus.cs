using UnityEngine;
using UnityEngine.Events;

public class WeaponEventBus : MonoBehaviour
{
    public UnityEvent<RaycastHit, bool> weaponFire;
    public UnityEvent weaponReload;
}
