using System;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    [SerializeField] private SoundType soundType;
    private SwitchState currentState;
    public static event Action OnLightSwitchToggled;

    private void OnEnable()
    {
        EventService.Instance.OnLightSwitchToggled.AddListener(onLightSwitch);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(onLightsTurnedOffByGhost);
    }
    private void OnDisable()
    {
        EventService.Instance.OnLightSwitchToggled.RemoveListener(onLightsToggled);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(onLightsTurnedOffByGhost);
    }


    private void Start() => currentState = SwitchState.Off;


    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        EventService.Instance.OnLightSwitchToggled.InvokeEvent();
    }

    private void toggleLights()
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

    private void setLights(bool lights)
    {
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
        if (lights)
        {
            currentState = SwitchState.On;
        }
        else
        {
            currentState = SwitchState.Off;
        }
    }

    private void onLightSwitch()
    {
        toggleLights();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
        GameService.Instance.GetInstructionView().HideInstruction();
    }
    private void onLightsTurnedOffByGhost()
    {
        setLights(false);
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
        GameService.Instance.GetInstructionView().ShowInstruction(InstructionType.LightsOff);
    }

    private void onLightsToggled()
    {
        toggleLights();
        GameService.Instance.GetSoundView().PlaySoundEffects(soundType);
    }
}
