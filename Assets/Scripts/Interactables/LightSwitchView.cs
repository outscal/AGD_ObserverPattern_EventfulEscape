using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private void Start() => currentState = SwitchState.Off;

    public delegate void LightSwitchDelegate();

    public event LightSwitchDelegate OnLightSwitch;

    private void OnEnable()
    {
        OnLightSwitch += LightSwitchView_OnLightSwitch;
        OnLightSwitch += LightSwitchView_OnLightSwitchSound; 
    }

    private void LightSwitchView_OnLightSwitchSound()
    {
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }

    private void LightSwitchView_OnLightSwitch()
    {
        ToggleLights();

        GameService.Instance.GetInstructionView().HideInstruction();
    }

    private void OnDisable()
    {
        OnLightSwitch -= LightSwitchView_OnLightSwitch;
        OnLightSwitch -= LightSwitchView_OnLightSwitchSound;
    }

    public void Interact()
    {
        OnLightSwitch?.Invoke();
    }

    private void ToggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }
}
