using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace FlappyGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _HUD;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _countDown;

        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [SerializeField] private TextMeshProUGUI _highestScoreTextHUD;
        [SerializeField] private TextMeshProUGUI _highestScoreTextMenu;
        [SerializeField] private GameObject _exitButton;

        public void PlayButtonPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PauseButtonPressed()
        {
            if (_pauseMenu.activeInHierarchy)
            {
                ResumeButtonPressed();
                return;
            }

            _pauseMenu.SetActive(true);
            _countDown.SetActive(false);
            _HUD.SetActive(false);
            _gameManager.PauseGameplay();
        }

        public void ResumeButtonPressed()
        {
            _pauseMenu.SetActive(false);
            _countDown.SetActive(true);
        }

        public void ManuButtonPressed()
        {
            _pauseMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void ExitButtonPressed()
        {
            Application.Quit();
        }

        public void UpdateScore(int score)
        {
            _currentScoreText.text = score.ToString();
        }

        public void ForceMenu()
        {
            _highestScoreTextHUD.text = _highestScoreTextMenu.text = _gameManager.HighestScore.ToString();

            _mainMenu.SetActive(true);
            _HUD.SetActive(false);
            _pauseMenu.SetActive(false);
            _countDown.SetActive(false);
        }

        private void Start()
        {
            _HUD.SetActive(true);
            _highestScoreTextHUD.text = _highestScoreTextMenu.text = _gameManager.HighestScore.ToString();
            _currentScoreText.text = "0";

#if !UNITY_STANDALONE_WIN
            _exitButton.SetActive(false);
#endif
        }
    }
}
