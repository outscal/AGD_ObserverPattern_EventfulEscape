using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIView : MonoBehaviour
{
    [Header("Player Sanity")]
    [SerializeField] GameObject rootViewPanel;
    [SerializeField] Image insanityImage;
    [SerializeField] Image redVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI keysFoundText;

    [Header("Game End Panel")]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;

    private void OnEnable()
    {

        tryAgainButton.onClick.AddListener(onTryAgainButtonClicked);
        quitButton.onClick.AddListener(onQuitButtonClicked);
        EventService.Instance.OnKeyPickedUp.AddListener(updateKeyText);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(setRedVignette);

        EventService.Instance.OnPlayerEscapedEvent.AddListener(onPlayerEscaped);
        EventService.Instance.OnPlayerDeathEvent.AddListener(setRedVignette);
        EventService.Instance.OnPlayerDeathEvent.AddListener(onPlayerDeath);
        EventService.Instance.OnRatRush.AddListener(setRedVignette);


        tryAgainButton.onClick.AddListener(onTryAgainButtonClicked);
        quitButton.onClick.AddListener(onQuitButtonClicked);
    }

    private void OnDisable()
    {

        EventService.Instance.OnKeyPickedUp.RemoveListener(updateKeyText);
        EventService.Instance.OnLightsOffByGhostEvent?.RemoveListener(setRedVignette);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(setRedVignette);
        EventService.Instance.OnPlayerEscapedEvent.RemoveListener(onPlayerEscaped);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(setRedVignette);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(onPlayerDeath);
        EventService.Instance.OnRatRush.RemoveListener(setRedVignette);

    }
    public void UpdateInsanity(float playerSanity) => insanityImage.rectTransform.localScale = new Vector3(1, playerSanity, 1);
    private void updateKeyText(int keys) => keysFoundText.SetText($"Keys Found: {keys}/3");

    private void onQuitButtonClicked() => Application.Quit();
    private void onTryAgainButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    //Assignment - Call this method as a lister of LightsOffByGhostEvent
    private void setRedVignette()
    {
        redVignette.enabled = true;
        redVignette.canvasRenderer.SetAlpha(0.5f);
        redVignette.CrossFadeAlpha(0, 5, false);
    }

    private void onPlayerDeath()
    {
        gameEndText.SetText("Game Over");
        gameEndPanel.SetActive(true);
    }

    private void onPlayerEscaped()
    {
        gameEndText.SetText("You Escaped");
        gameEndPanel.SetActive(false);
    }


}

