using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject _hands;
    [SerializeField] private WeaponDatabase.ID[] _startingWeaponIDs;
    [SerializeField] private List<IWeapon> _weapons;
    private int currentWeaponIndex = 0;
    private IWeapon _currentWeapon;
    private InputSystem_Actions _input;
    void Awake() {
        _input = new InputSystem_Actions();
        _input.Player.NextWeapon.performed += nextWeapon;
        _input.Player.PreviousWeapon.performed += previousWeapon;
        _input.Player.Enable();
    }

    void Start() {
        var wdb = WeaponDatabase.Instance;
        foreach(var id in _startingWeaponIDs) {
            Debug.Log($"Loading weapon: {id}");
            var weaponInstance = Instantiate(wdb.GetWeapon(id), _hands.transform);
            _weapons.Add(weaponInstance.GetComponent<IWeapon>());
            weaponInstance.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    private void nextWeapon(InputAction.CallbackContext ctx) {
        Debug.Log("Drawing next weapon");
        if(_currentWeapon != null) {
            _currentWeapon.Hide();
        }

        _currentWeapon = _weapons[0];
        _currentWeapon.transform.parent = _hands.transform;

        _currentWeapon.Draw();
    }

    // Update is called once per frame
    private void previousWeapon(InputAction.CallbackContext ctx) {
        Debug.Log("Drawing previous weapon");
        if(_currentWeapon != null) {
            _currentWeapon.Hide();
        }

        _currentWeapon = _weapons[0];
        _currentWeapon.transform.parent = _hands.transform;

        _currentWeapon.Draw();
    }
}
