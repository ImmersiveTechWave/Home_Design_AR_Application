using UnityEngine;

namespace AF
{
    public class SimpleWallController : MonoBehaviour, ISelectable
    {
        public SimpleWallView View { get; private set; }

        private void Start()
        {
            View = gameObject.GetComponent<SimpleWallView>();
            View.Outline.enabled = false;
        }

        public void Select()
        {
            if (transform.localScale == Vector3.one)
            {
                if (App.SelectedWall != null)
                {
                    App.SelectedWall.View.Outline.enabled = false;
                }
                App.SelectedWall = this;
                App.SelectedWall.View.Outline.enabled = true;
            }
        }
    }
}
