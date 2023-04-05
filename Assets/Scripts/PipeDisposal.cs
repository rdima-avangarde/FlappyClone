using UnityEngine;

namespace FlappyGame
{
    public class PipeDisposal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Pipes"))
                other.gameObject.SetActive(false);
        }
    }
}
