using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AF
{
    public class CreateWalls : MonoBehaviour
    {
        private const float WALL_LENGHT = 4f;

        private GameObject startWallPrefabe;
        private GameObject endWallPrefabe;
        private GameObject previewWallPrefabe;
        private SimpleWallController simpleWallPrefabe;
        private GameObject startWallGO;
        private GameObject endWallGO;
        private GameObject previewWallGO;
        private InputManager inputManager;

        private bool isCreating;

        private void Start()
        {
            inputManager = FindObjectOfType<InputManager>();
            startWallPrefabe = Resources.Load<GameObject>(WallPaths.START_WALL_PATH);
            endWallPrefabe = Resources.Load<GameObject>(WallPaths.END_WALL_PATH);
            simpleWallPrefabe = Resources.Load<SimpleWallController>(WallPaths.SIMPLE_WALL_PATH);
            previewWallPrefabe = Resources.Load<GameObject>(WallPaths.PREVIEW_WALL_PATH);

            startWallGO = Instantiate(startWallPrefabe, Vector3.zero, Quaternion.identity);
            endWallGO = Instantiate(endWallPrefabe, Vector3.zero, Quaternion.identity);
        }

        private void Update()
        {
            DrawWall();
        }

        private void DrawWall()
        {
            if (Input.GetMouseButtonDown(0) && !inputManager.IsPointerOverUIElement())
            {
                SetStart();
            }
            else if (isCreating == true && Input.GetMouseButtonUp(0) && !inputManager.IsPointerOverUIElement())
            {
                SetEnd();
            }
            else
            {
                if (isCreating)
                {
                    Create();
                }
            }
        }

        IEnumerator CreateWalll()
        {
            previewWallGO = Instantiate(previewWallPrefabe, startWallPrefabe.transform.position, Quaternion.identity);
            var hit = inputManager.GetWorldPoint();
            var spherePrefabe = Resources.Load<BestMarginController>(WallPaths.BEST_MARGIN_SPHERE);
            var sphereGO = Instantiate(spherePrefabe, hit, Quaternion.identity);

            yield return new WaitForSeconds(0.01f);

            var bestPoint = sphereGO.GetBestPoint(hit);
            if (bestPoint == Vector3.zero)
            {
                bestPoint = hit;
            }
            else
            {
                bestPoint = new Vector3(bestPoint.x, 0, bestPoint.z);
            }
            isCreating = true;
            startWallGO.transform.position = bestPoint;
        }

        private void SetStart()
        {
            StartCoroutine(CreateWalll());
        }

        private void SetEnd()
        {
            isCreating = false;
            endWallGO.transform.position = inputManager.GetWorldPoint();
            var distance = Vector3.Distance(startWallGO.transform.position, endWallGO.transform.position);

            var numberOfWalls = (int)(distance / WALL_LENGHT);
            var difference = distance - numberOfWalls * WALL_LENGHT;

            var leftWallPosition = startWallGO.transform.position + previewWallGO.transform.forward * (difference / 4);

            var leftWall = Instantiate(simpleWallPrefabe, leftWallPosition, previewWallGO.transform.rotation);
            leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, leftWall.transform.localScale.y, difference / 8);

            Vector3 startPosition;
            var wallForward = previewWallGO.transform.forward;
            var startWallPosition = startWallGO.transform.position;

            for (int i = 0; i < numberOfWalls; i++)
            {
                startPosition = wallForward * (2 * 2 * i + difference / 2 + 2) + startWallPosition;
                Instantiate(simpleWallPrefabe, startPosition, previewWallGO.transform.rotation);
            }

            var rightWallPosition = wallForward * (difference / 2 + difference / 4 + WALL_LENGHT * numberOfWalls) + startWallPosition;
            var rightWall = Instantiate(simpleWallPrefabe, rightWallPosition, previewWallGO.transform.rotation);
            rightWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, leftWall.transform.localScale.y, difference / 8);

            Destroy(previewWallGO.gameObject);
        }

        private void Create()
        {
            endWallGO.transform.position = inputManager.GetWorldPoint();
            CreateWall();
        }

        void CreateWall()
        {
            startWallGO.transform.LookAt(endWallGO.transform.position);
            endWallGO.transform.LookAt(startWallGO.transform.position);
            var distance = (startWallGO.transform.position - endWallGO.transform.position).magnitude;

            previewWallGO.transform.position = startWallGO.transform.position + distance / 2 * startWallGO.transform.forward;
            previewWallGO.transform.rotation = startWallGO.transform.rotation;
            previewWallGO.transform.localScale = new Vector3(previewWallGO.transform.localScale.x, previewWallGO.transform.localScale.y, distance);
        }
    }
}
