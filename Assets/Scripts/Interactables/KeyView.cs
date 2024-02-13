using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    [SerializeField] GameUIView gameUIView;
    public void Interact()
    {
        int currentKey = GameService.Instance.GetPlayerController().KeysEquipped;
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);
        GameService.Instance.GetPlayerController().KeysEquipped++;
        currentKey++;
        EventService.Instance.onKeyPickedUpEvent.InvokeEvent(currentKey);
     

        gameObject.SetActive(false);
    }
}
