using UnityEngine;

[CreateAssetMenu(menuName = "Misc/NoteDefinition")]
public class NoteData : ScriptableObject
{
    public string noteNameID;
    public string noteContentID;
    public Sprite note;
}