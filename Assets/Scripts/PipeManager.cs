using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlappyGame
{
    public class PipeManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private GameObject _pipePrefab;
        [SerializeField] private GameObject _scoreTriggerPrefab;
        [SerializeField] private float _pipeHorizontalDistanceMin;
        [SerializeField] private float _pipeHorizontalDistanceMax;
        [SerializeField] private float _pipeVerticalDistanceMin;
        [SerializeField] private float _pipeVerticalDistanceMax;
        [SerializeField] private float _pipeMinVerticalPos;
        [SerializeField] private float _pipeMaxVerticalPos;
        [SerializeField] private float _levelSpeed;

        [SerializeField] private List<Transform> _pipes = new List<Transform>();
        [SerializeField] private List<Transform> _scoreTriggers = new List<Transform>();
        private float _gameplayAreaWidth;
        private Vector3 _currentPipeSetPos;

        public void Pause(bool pause)
        {
            _rigidBody.isKinematic = pause;

            if (!pause)
            {
                _rigidBody.velocity = Vector3.right * -_levelSpeed;
            }
        }

        public void GeneratePipeSet()
        {
            List<Transform> pipeSet = GetPipeSet();
            pipeSet[0].transform.rotation = Quaternion.Euler(90, 0, 0);
            pipeSet[1].transform.rotation = Quaternion.Euler(-90, 0, 0);
            _currentPipeSetPos.x += Random.Range(_pipeHorizontalDistanceMin, _pipeHorizontalDistanceMax);
            _currentPipeSetPos.y = Random.Range(_pipeMinVerticalPos, _pipeMaxVerticalPos);
            pipeSet[1].transform.localPosition = _currentPipeSetPos;
            _currentPipeSetPos.y += Random.Range(_pipeVerticalDistanceMin, _pipeVerticalDistanceMax);
            pipeSet[0].transform.localPosition = _currentPipeSetPos;

            Transform scoreTrigger = GetScoreTrigger();
            scoreTrigger.transform.localPosition = new Vector3(_currentPipeSetPos.x, 0, 0);
        }

        private void Start()
        {
            Camera cam = Camera.main;
            _gameplayAreaWidth = 2.0f * cam.transform.position.z * -1f * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * cam.aspect;

            GenerateStartPipes();
            Pause(true);
        }

        private void GenerateStartPipes()
        {
            for (int i = 0; i <= _gameplayAreaWidth / _pipeHorizontalDistanceMin; i++)
            {
                GeneratePipeSet();
            }
        }

        private List<Transform> GetPipeSet()
        {
            var pipeSet = _pipes.Where(p => !p.gameObject.activeInHierarchy).Take(2).ToList();

            while (pipeSet.Count() < 2)
            {
                Transform newPipe = Instantiate(_pipePrefab, transform).transform;

                _pipes.Add(newPipe);
                pipeSet.Add(newPipe);
            }

            pipeSet.ForEach(o => o.gameObject.SetActive(true));

            return pipeSet;
        }

        private Transform GetScoreTrigger()
        {
            Transform scoreTrigger = _scoreTriggers.FirstOrDefault(p => !p.gameObject.activeInHierarchy);

            if (scoreTrigger == null)
            {
                scoreTrigger = Instantiate(_scoreTriggerPrefab, transform).transform;
                _scoreTriggers.Add(scoreTrigger);
                
            }

            scoreTrigger.gameObject.SetActive(true);

            return scoreTrigger;
        }
    }
}
