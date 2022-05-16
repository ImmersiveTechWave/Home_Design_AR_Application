using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AF.UI;
using UnityEngine.EventSystems;

namespace AF
{
	public class ObjectUIController : MonoBehaviour
	{
		private Button button;
		private TextMeshProUGUI text;
		private UIMainScreenScreenController screenView;

		private void Start()
		{
			button = transform.GetComponentInChildren<Button>();
			text = transform.GetComponentInChildren<TextMeshProUGUI>();
			screenView = FindObjectOfType<UIMainScreenScreenController>();
			//button.onClick.AddListener(CreateObject);
			//button.OnPointerClick
		}

		public void CreateObject()
		{
			var name = text.text;
			var objectR = Resources.Load<ObjectController>(ObjectsPath.ALL_OBJECTS_PATH + "/" + name);
			var objectGO = Instantiate(objectR, Vector3.zero, Quaternion.identity);

			objectGO.ObjectWasInstantiated();
		}

		private void OnDestroy()
		{
			button?.onClick.RemoveAllListeners();
		}
	}
}
