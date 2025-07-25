using UnityEngine;
using UnityEngine.Events;

public class WeaponEventBus : MonoBehaviour
{
    public UnityEvent<Ray, RaycastHit, bool> weaponFire;
}
