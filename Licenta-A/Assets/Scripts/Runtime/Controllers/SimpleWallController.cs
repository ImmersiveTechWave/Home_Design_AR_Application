using UnityEngine;

namespace AF
{
    public class SimpleWallController : MonoBehaviour
    {
        public SimpleWallView View { get; private set; }

        private void Start()
        {
            View = gameObject.GetComponent<SimpleWallView>();
        }
    }
}
