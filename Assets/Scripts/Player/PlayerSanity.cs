using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    private float maxSanity;
    private PlayerController playerController;

    private void OnEnable()
    {
        EventService.Instance.OnRatRush.AddListener(OnSupernaturalEvent);
    }

    private void OnDisable()
    {
        EventService.Instance.OnRatRush.RemoveListener(OnSupernaturalEvent);
    }

    private void Start()
    {
        maxSanity = sanityLevel;
        playerController = GameService.Instance.GetPlayerController();
    }
    void Update()
    {
        if (playerController.PlayerState == PlayerState.Dead)
            return;

        float sanityDrop = updateSanity();

        increaseSanity(sanityDrop);
    }

    private float updateSanity()
    {
        float sanityDrop = sanityDropRate * Time.deltaTime;
        if (playerController.PlayerState == PlayerState.InDark)
        {
            sanityDrop *= 10f;
        }
        return sanityDrop;
    }

    private void increaseSanity(float amountToDecrease)
    {
        Mathf.Floor(sanityLevel -= amountToDecrease);
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameService.Instance.GameOver();
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    private void decreaseSanity(float amountToIncrease)
    {
        Mathf.Floor(sanityLevel += amountToIncrease);
        if (sanityLevel > 100)
        {
            sanityLevel = 100;
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }
    private void OnSupernaturalEvent()
    {
        increaseSanity(sanityDropAmountPerEvent);
    }

    private void OnDrankPotion(int potionEffect)
    {
        decreaseSanity(potionEffect);
    }
}