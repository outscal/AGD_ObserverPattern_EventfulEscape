using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIView : MonoBehaviour
{
    [SerializeField] private Button playButton;
    private void OnEnable() => playButton.onClick.AddListener(OnPlayButtonClicked);
    private void OnPlayButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
