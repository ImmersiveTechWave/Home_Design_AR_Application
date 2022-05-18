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
		private InputManager inputManager;

		private void Start()
		{
			button = transform.GetComponentInChildren<Button>();
			text = transform.GetComponentInChildren<TextMeshProUGUI>();
			screenView = FindObjectOfType<UIMainScreenScreenController>();
			inputManager = FindObjectOfType<InputManager>();
		}

		public void CreateObject()
		{
			var name = text.text;
			var objectR = Resources.Load<ObjectController>(ObjectsPath.ALL_OBJECTS_PATH + "/" + name);
			var objectGO = Instantiate(objectR, inputManager.GetWorldPoint(), Quaternion.identity);

			objectGO.ObjectWasInstantiated();
		}

		private void OnDestroy()
		{
			button?.onClick.RemoveAllListeners();
		}
	}
}
