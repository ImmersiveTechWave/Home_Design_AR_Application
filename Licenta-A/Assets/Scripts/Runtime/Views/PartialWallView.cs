using UnityEngine;

namespace AF
{
    public class PartialWallView : MonoBehaviour
    {
        public GameObject Wall { get; set; }
        public GameObject MarginLeft { get; private set; }
        public GameObject MarginRight { get; private set; }
        public SelectObject SelectObject { get; set; }
       // public 

        private void Awake()
        {
            Wall = transform.Find("Wall").gameObject;
            MarginLeft = transform.Find("MarginLeft").gameObject;
            MarginRight = transform.Find("MarginRight").gameObject;
            SelectObject = transform.Find("SelectedBox").GetComponent<SelectObject>();
        }
    }
}
