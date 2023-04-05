using UnityEngine;

namespace FlappyGame
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _flappy;

        private void LateUpdate()
        {
            var pos = transform.position;
            pos.x = _flappy.position.x;
            transform.position = pos;
        }
    }
}
