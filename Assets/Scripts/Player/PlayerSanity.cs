using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Responsible for handling Sanity of our Player
/// </summary>
public class PlayerSanity : MonoBehaviour // mono?
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    [SerializeField] private UIManager UIManager;  //??
    private float maxSanity;
    private bool isPlayerInDark = false;
    private bool isAlive = true;

    private GameEvent lightSwitchEvent = new GameEvent();


    private void Start()
    {
        maxSanity = sanityLevel;
    }

    private void OnEnable()
    {

        EventManager.Instance.OnLightsOffByGhost += OnLightsOffByGhost;
        // EventManager.Instance.OnLightsSwitchToggled += OnLightsToggled;
        lightSwitchEvent.addListener(OnLightsToggled);
        EventManager.OnRatRush += OnSupernaturalEvent;
        EventManager.OnSkullDrop += OnSupernaturalEvent;
        EventManager.OnPotionDrink += OnDrankPotion;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnLightsOffByGhost -= OnLightsOffByGhost;
        //  EventManager.Instance.OnLightsSwitchToggled -= OnLightsToggled;
        lightSwitchEvent.removeListener(OnLightsToggled);
        EventManager.OnRatRush -= OnSupernaturalEvent;
        EventManager.OnSkullDrop -= OnSupernaturalEvent;
        EventManager.OnPotionDrink -= OnDrankPotion;
    }

    void Update()
    {
        if (!isAlive)
            return;

        if (isPlayerInDark)
            DecreaseSanity(sanityDropRate * Time.deltaTime * 10);
        else
            DecreaseSanity(sanityDropRate * Time.deltaTime);
    }

    public void DecreaseSanity(float amountToDecrease)
    {
        sanityLevel -= amountToDecrease;
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameOver();
        }
        UIManager.UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    public void IncreaseSanity(float amountToIncrease)
    {
        sanityLevel += amountToIncrease;
        if (sanityLevel > 100)
        {
            sanityLevel = 100;
        }
        UIManager.UpdateInsanity(1f - sanityLevel / maxSanity);
    }


    void GameOver()
    {
        Debug.Log("Player Died");
        isAlive = false;
        EventManager.Instance.InvokeOnPlayerDeath();
        SoundManager.Instance.PlaySoundEffects(SoundType.JumpScare1, false);
    }

    private void OnLightsOffByGhost()
    {
        isPlayerInDark = true;
    }

    private void OnLightsToggled()
    {
        isPlayerInDark = !isPlayerInDark;
    }

    private void OnSupernaturalEvent()
    {
        DecreaseSanity(sanityDropAmountPerEvent);
    }

    private void OnDrankPotion()
    {
        IncreaseSanity(20);
    }
}