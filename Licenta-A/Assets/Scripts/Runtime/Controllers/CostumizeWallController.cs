using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AF.UI;

namespace AF
{
	public class CostumizeWallController : MonoBehaviour
	{
		private Button button;
		private TextMeshProUGUI text;
		private UIMainScreenScreenController screenView;

		private void Start()
		{
			button = transform.GetComponentInChildren<Button>();
			text = transform.GetComponentInChildren<TextMeshProUGUI>();
			screenView = FindObjectOfType<UIMainScreenScreenController>();
			button.onClick.AddListener(ChangeWall);
		}

		private void ChangeWall()
		{
			if (App.SelectedPartialWall != null)
			{
				if (App.SelectedPartialWall.transform.localScale == Vector3.one)
				{
					var position = App.SelectedPartialWall.View.Wall.transform.position;
					var rotation = App.SelectedPartialWall.View.Wall.transform.rotation;
					Destroy(App.SelectedPartialWall.View.Wall.gameObject);

					var wallName = "";
					var words = text.text.Split(' ');

					foreach (var word in words)
					{
						wallName += word;
					}

					var path = "Prefabes/Walls/" + wallName;
					var newWallPrefabe = Resources.Load<GameObject>(path);

					var newWallGo = Instantiate(newWallPrefabe, position, rotation);
					newWallGo.transform.SetParent(App.SelectedPartialWall.transform);
					newWallGo.name = "Wall";
					var firstFaceMaterial =  App.SelectedPartialWall.View.WallFaceController.FirstFaceRenderer.material;
					var secondFaceMaterial = App.SelectedPartialWall.View.WallFaceController.SecondFaceRenderer.material;
					App.SelectedPartialWall.View.Wall = newWallGo;
					App.SelectedPartialWall.View.SelectObject = App.SelectedPartialWall.View.transform.Find("SelectedBox").GetComponent<SelectObject>();
					App.SelectedPartialWall.View.WallFaceController = newWallGo.GetComponent<WallFaceController>();
					App.SelectedPartialWall.View.WallFaceController.SetFirstFaceMaterial(firstFaceMaterial);
					App.SelectedPartialWall.View.WallFaceController.SetSecondFaceMaterial(secondFaceMaterial);
				}
			}
		}

		private void OnDestroy()
		{
			button?.onClick.RemoveAllListeners();
		}
	}
}
