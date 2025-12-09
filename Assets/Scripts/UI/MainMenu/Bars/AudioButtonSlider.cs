using UnityEngine;

[RequireComponent(typeof(ButtonSlider))]
public class AudioButtonSlider : MonoBehaviour {
    public AudioBusID bus;
    [SerializeField] private GlobalSettings settings;
    private ButtonSlider slider;
    void Awake() {
        slider = GetComponent<ButtonSlider>();
    }

    void Start() {
        GetValue();
        slider.valueUpdate.AddListener(UpdateSettings);
    }

    private void GetValue() {
        float value;

        switch (bus) {
            case AudioBusID.NotDefined:
                value = settings.Volume_Main;
                break;
            case AudioBusID.Ambient:
                value = settings.Volume_Ambient;
                break;
            case AudioBusID.SFX:
                value = settings.Volume_Sfx;
                break;
            default:
                Debug.LogError("wtf?");
                return;
        }
        Debug.Log($"Get {value}");
        slider.SetValue(value);
    }

    private void UpdateSettings(float percent) {
        Debug.Log($"Update {percent}");
        switch (bus) {
            case AudioBusID.NotDefined:
                settings.Volume_Main = percent;
                break;
            case AudioBusID.Ambient:
                settings.Volume_Ambient = percent;
                break;
            case AudioBusID.SFX:
                settings.Volume_Sfx = percent;
                break;
            default:
                Debug.LogError("wtf?");
                return;
        }
    }
}