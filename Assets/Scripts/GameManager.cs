using UnityEngine;
using UnityEngine.EventSystems;

namespace FlappyGame
{
    public class GameManager : MonoBehaviour
    {
        public int CurrentScore { get; private set; }
        public int HighestScore { get; private set; }

        public bool HasFlapInput { get; private set; }

        [SerializeField] UIManager _UIManager;
        [SerializeField] Flappy _flappy;
        [SerializeField] PipeManager _pipeManager;

        private bool _hasPauseInput;
        private bool _started;

        private const string SCORE_KEY = "FlappyScore";

        public void PauseGameplay()
        {
            _flappy.Pause(true);
            _pipeManager.Pause(true);
        }

        public void ResumeGameplay()
        {
            _flappy.Pause(false);
            _pipeManager.Pause(false);
        }

        public void EndGame()
        {
            if (PlayerPrefs.GetInt(SCORE_KEY) < CurrentScore)
            {
                PlayerPrefs.SetInt(SCORE_KEY, CurrentScore);
                HighestScore = CurrentScore;
            }

            _UIManager.ForceMenu();
            _pipeManager.Pause(true);
        }

        public void FlappyScored()
        {
            CurrentScore++;
            _pipeManager.GeneratePipeSet();
            _UIManager.UpdateScore(CurrentScore);
        }

        private void Update()
        {
            HandleInput();

            HasFlapInput = HasFlapInput && !EventSystem.current.IsPointerOverGameObject();

            if(!_started && HasFlapInput)
            {
                _started = true;
                ResumeGameplay();
            }

            if (_hasPauseInput)
            {
                _UIManager.PauseButtonPressed();
            }
        }

        private void HandleInput()
        {
#if UNITY_ANDROID || UNITY_IOS
            HasFlapInput = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#else
            HasFlapInput = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
            _hasPauseInput = Input.GetKeyDown(KeyCode.Escape);
#endif
        }

        private void Awake()
        {
            HighestScore = PlayerPrefs.GetInt(SCORE_KEY);
        }
    }
}
