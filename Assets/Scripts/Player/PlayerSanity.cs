using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;

    private float maxSanity;
    private PlayerController playerController;

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

        decreaseSanity(sanityDrop);
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

    private void decreaseSanity(float amountToDecrease)
    {
        Mathf.Floor(sanityLevel -= amountToDecrease);
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameService.Instance.GameOver();
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }
}