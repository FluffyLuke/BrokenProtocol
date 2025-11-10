using UnityEngine;

public class SoundHandle {
    private AudioSource source;

    public SoundHandle(AudioSource source) {
        this.source = source;
    }
    public void StopAndDestroy() {
        source.Stop();
        GameObject.Destroy(source.gameObject);
    }
}