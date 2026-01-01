using UnityEngine;

public class Note: MonoBehaviour {
    [SerializeField] private NoteData data;
    public void ShowNote() {
        PlayerEventBus.ShowNote.Invoke(data);
    }
}