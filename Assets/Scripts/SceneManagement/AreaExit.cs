using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public AreaEntrance theEntrance;
    public float waitToLoad = 1f; 

    [SerializeField] private string areaToLoad;
    [SerializeField] private string areaTransitionName;
    private bool shouldLoadAfterFade; 

    private void Start() {
        theEntrance.transitionName = areaTransitionName;
    }

    private void Update() { 
        if(shouldLoadAfterFade) {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0) {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            shouldLoadAfterFade = true;
            UIFade.Instance.FadeToBlack();

            PlayerController.Instance.areaTransitionName = areaTransitionName;
        }
    }
}
