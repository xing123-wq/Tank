using UnityEngine;

namespace Assets.Script
{
    public class Explosion : MonoBehaviour
    {

        private void Update()
        {

        }
        private void Start()
        {
            Destroy(gameObject, 0.167f);
        }
    }
}
