using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponEventBus))]
public class Weapon : IItem
{
    private InputSystem_Actions _input;
    private WeaponEventBus _events;
    private ItemData _data;
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string fireAnimation = "Fire";
    [SerializeField] private string reloadAnimation = "Reload";
    void Awake() {
        _events = GetComponent<WeaponEventBus>();
        _input = new InputSystem_Actions();
        _input.Player.Fire.performed += Fire;
        _input.Player.Reload.performed += Reload;
    }
    void OnEnable() {
        _input.Enable();
    }
    void OnDisable() {
        _input.Disable();
    }

    Ray currentRay;
    RaycastHit currentHit;
    void Update() {
        Debug.DrawLine(currentRay.origin, currentHit.point);
    }
    public void Fire(InputAction.CallbackContext ctx) {
        Ammo ammo = _data.GetProperty<Ammo>();
        if(ammo.count <= 0) return;

        _animator.Play(fireAnimation);
        Debug.Log($"{ammo.count}");
        ammo.count -= 1;

        //FIX get those values from render texture
        int x = 240;
        int y = 135;

        Vector3 renderTextureCenter = new Vector3(x / 2f, y / 2f, 0);
        Ray ray = Camera.main.ScreenPointToRay(renderTextureCenter);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            _events.weaponFire.Invoke(ray, hit, true);
        }

        currentHit = hit;
        currentRay = ray;
        _events.weaponFire.Invoke(ray, hit, false);
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