using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AF
{
    public class CostumizeWallController : MonoBehaviour
    {
        private Button button;
        private TextMeshProUGUI text;

        private void Start()
        {
            button = transform.GetComponentInChildren<Button>();
            text = transform.GetComponentInChildren<TextMeshProUGUI>();
            button.onClick.AddListener(ChangeWall);
        }

        private void ChangeWall()
        {
            if (transform.localScale == Vector3.one)
            {
                var position = App.SelectedWall.View.Wall.transform.position;
                var rotation = App.SelectedWall.View.Wall.transform.rotation;
                Destroy(App.SelectedWall.View.Wall.gameObject);
                var path = "Prefabes/" + text.text.Trim();
                var newWallPrefabe = Resources.Load<GameObject>(path);

                var newWallGo = Instantiate(newWallPrefabe, position, rotation);
                newWallGo.transform.SetParent(App.SelectedWall.transform);
                newWallGo.name = "Wall";
                App.SelectedWall.View.Wall = newWallGo;
                App.SelectedWall.View.Outline = newWallGo.GetComponent<Outline>() ;
            }
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
