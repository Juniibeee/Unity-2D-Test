using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to change the location of the hero if a FountainRespawn point is found in the scene.  See EssentialsLoader.cs
public class FountainRespawn : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint;
}
