using UnityEngine;

public class PotionView : MonoBehaviour, IInteractable
{

    [SerializeField] SoundType soundType;
    private int potionEffect = 20;

    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
        gameObject.SetActive(false);
    }
}
