using UnityEngine;
using UnityEngine.InputSystem;
public class Weapon : IWeapon
{
    private InputSystem_Actions _input;
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string fireAnimation = "Fire";
    [SerializeField] private string reloadAnimation = "Reload";
    void Awake() {
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
    public void Fire(InputAction.CallbackContext ctx) {
        _animator.Play(fireAnimation);
    }
    public void Reload(InputAction.CallbackContext ctx) {
        _animator.Play(reloadAnimation);
    }
    public override void Draw() {
        transform.gameObject.SetActive(true);
    }
    public override void Hide() {
        transform.gameObject.SetActive(false);
    }
}