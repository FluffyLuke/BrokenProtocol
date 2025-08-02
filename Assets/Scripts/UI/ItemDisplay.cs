using UnityEngine;
using UnityEngine.UI;
public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Color _notSelectedColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Image _itemIcon;
    private Image _itemIconBackground;
    private void Awake() {
        _itemIconBackground = GetComponent<Image>();   
    }
    public void Init(Sprite icon, bool selected) {
        if(selected) _itemIconBackground.color = _selectedColor;
        else _itemIconBackground.color = _notSelectedColor;
        _itemIcon.sprite = icon;
    }
}
