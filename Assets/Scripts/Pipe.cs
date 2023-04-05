using UnityEngine;

namespace FlappyGame
{
    public class Pipe : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}
