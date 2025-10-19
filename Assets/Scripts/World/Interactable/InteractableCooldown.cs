using System.Collections;
using UnityEngine;
public class InteractableCooldown : MonoBehaviour
{
    [SerializeField] private IInteractable interactable;
    public float cooldown = 0f;
    void Start() {
        if(interactable == null) {
            interactable = GetComponent<IInteractable>();
            if(interactable == null) {
                Debug.LogError("No interactable can be found");
            }
        }

        interactable.interactedWith.AddListener(() => {
            StartCoroutine(setCooldown());
        });
    }

    private IEnumerator setCooldown() {
        interactable.canInteract = false;
        yield return new WaitForSeconds(cooldown);
        interactable.canInteract = true;
    }
}