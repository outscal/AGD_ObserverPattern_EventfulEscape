using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorView : MonoBehaviour, IInteractable
{
    [SerializeField] private float swingAngle;
    [SerializeField] private int keysRequiredToOpen;
    private DoorState currentState;

    private void Start()
    {
        currentState = DoorState.Locked;
    }
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        DoorInteraction();
    }

    private void DoorInteraction()
    {
        switch (currentState)
        {
            case DoorState.Locked:
                openDoor();
                break;
            case DoorState.Close:
                openDoor();
                break;
            case DoorState.Open:
                closeDoor();
                break;
        }
    }

    private void closeDoor()
    {
        transform.Rotate(0f, transform.rotation.y - swingAngle, 0f);
        currentState = DoorState.Close;
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.DoorSlam);
    }

    private void openDoor()
    {
        if (GameService.Instance.GetPlayerController().KeysEquipped >= keysRequiredToOpen)
        {
            transform.Rotate(0f, transform.rotation.y + swingAngle, 0f);
            currentState = DoorState.Open;
            GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.DoorOpen);
        }
    }
}
