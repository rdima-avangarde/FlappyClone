using UnityEngine;
using TMPro;

namespace FlappyGame
{
    public class CountDown : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private int _secondsCount;
        [SerializeField] private TextMeshProUGUI _countDownText;
        [SerializeField] private GameObject _HUD;

        private float _timer;

        private void OnEnable()
        {
            _timer = _secondsCount;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if(_timer <= 0)
            {
                gameObject.SetActive(false);
                _HUD.SetActive(true);
                _gameManager.ResumeGameplay();
            }

            _countDownText.text = Mathf.Ceil(_timer).ToString();
        }
    }
}
