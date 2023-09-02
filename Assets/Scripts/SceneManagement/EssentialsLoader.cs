using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private GameObject cameraContainer;

    private void Start()
    {
        if(PlayerController.Instance == null) {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

            // Can place in any scene to set the spawn point of our hero in that scene
            if (FindObjectOfType<FountainRespawn>()) {
                clone.transform.position = FindObjectOfType<FountainRespawn>().respawnPoint.transform.position;
            } else {
                clone.transform.position = FindObjectOfType<AreaEntrance>().transform.position;
            }
        }

        if(CameraController.Instance == null) {
            Instantiate(cameraContainer).GetComponent<CameraController>();
        }
    }
}
