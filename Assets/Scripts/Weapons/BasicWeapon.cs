using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponEventBus))]
public class Weapon : IItem
{
    private InputSystem_Actions _input;
    private bool fireButtonHeld = false;
    private WeaponEventBus _events;
    private ItemData _data;
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string fireAnimation = "Fire";
    [SerializeField] private string reloadAnimation = "Reload";
    [Header("WeaponOption")]
    public float repeatFireSecs = 0;
    private Coroutine repeatFireCoroutine;
    [Header("Decals")]
    public LayerMask LayersToIgnore;
    void Awake() {
        _events = GetComponent<WeaponEventBus>();
        _input = new InputSystem_Actions();
        _input.Player.Fire.performed += fireWeaponCallback;
        _input.Player.Fire.canceled += fireWeaponCancelCallback;
        _input.Player.Reload.performed += Reload;
    }
    void OnEnable() {
        _input.Enable();
    }
    void OnDisable() {
        _input.Disable();
    }
    private void fireWeaponCallback(InputAction.CallbackContext ctx) {
        fireButtonHeld = true;
        FireWeapon();
    }
    private void fireWeaponCancelCallback(InputAction.CallbackContext ctx) {
        fireButtonHeld = false;
    }
    public void FireWeapon() {
        if(repeatFireCoroutine != null) {
            StopCoroutine(repeatFireCoroutine);
        }

        Ammo ammo = _data.GetProperty<Ammo>();
        if(ammo.count <= 0) return;

        _animator.Play(fireAnimation, -1, 0.0f);
        Debug.Log($"{ammo.count}");
        ammo.count -= 1;

        float maxDistance = 1000f;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, ~LayersToIgnore)) {
            _events.weaponFire.Invoke(hit, true);
        }

        _events.weaponFire.Invoke(hit, false);

        if(repeatFireSecs != 0) {
            StartCoroutine(fireAgain());
        }
    }

    private IEnumerator fireAgain() {
        yield return new WaitForSeconds(repeatFireSecs);
        if(fireButtonHeld) {
            FireWeapon();
        }
    }
    public void Reload(InputAction.CallbackContext ctx) {
        Ammo ammo = _data.GetProperty<Ammo>();
        ammo.count = ammo.maxAmmo;
        _animator.Play(reloadAnimation);
    }
    public override void Grab(ItemData itemData) {
        _data = itemData;
        Ammo ammo = _data.GetProperty<Ammo>();
        Debug.Log(ammo.count);
        transform.gameObject.SetActive(true);
    }
    public override void Hide() {
        transform.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}