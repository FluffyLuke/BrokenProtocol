using UnityEngine;
using UnityEngine.UI;
public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Color notSelectedColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Image itemIcon;
    private Image itemIconBackground;
    private void Awake() {
        itemIconBackground = GetComponent<Image>();   
    }
    public void Init(Sprite icon, bool selected) {
        if(selected) itemIconBackground.color = selectedColor;
        else itemIconBackground.color = notSelectedColor;
        itemIcon.sprite = icon;
    }
}
