using UnityEngine;

namespace AF
{
    public class SimpleWallController : MonoBehaviour, ISelectable
    {
        public SimpleWallView View { get; private set; }

        private void Start()
        {
            View = gameObject.GetComponent<SimpleWallView>();
        }

        public void Select()
        {
            if (transform.localScale == Vector3.one)
            {
                App.SelectedWall = this;
            }
        }
    }
}
