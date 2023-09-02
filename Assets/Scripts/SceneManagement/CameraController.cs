using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : Singleton<CameraController>
{
    #region Private Variables

    private Transform player;
    private Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;
    private const string groundString = "Ground";

    #endregion

    #region Unity Methods

    private void Start() {
        if (FindObjectOfType<PlayerController>()) {
            player = PlayerController.Instance.transform;
        }
        theMap = GameObject.Find(groundString).GetComponent<Tilemap>();

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
    }

    private void Update() {
        FindPlayer();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), 
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), 
            transform.position.z);
    }

    #endregion

    #region Private Methods

    private void FindPlayer() {
        if (player == null) {
            player = PlayerController.Instance.transform;
        }

        if (theMap == null) {
            theMap = GameObject.Find(groundString).GetComponent<Tilemap>();

            bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
            topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
        }
    }

    #endregion
}
