using UnityEngine;

namespace AF
{
    public class SimpleWallView : MonoBehaviour
    {
        public GameObject Wall { get; set; }
        public GameObject MarginLeft { get; private set; }
        public GameObject MarginRight { get; private set; }
        public Outline Outline { get; set; }

        private void Awake()
        {
            Wall = transform.Find("Wall").gameObject;
            MarginLeft = transform.Find("MarginLeft").gameObject;
            MarginRight = transform.Find("MarginRight").gameObject;
            Outline = Wall.GetComponent<Outline>();
        }
    }
}
