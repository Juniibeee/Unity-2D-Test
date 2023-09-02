using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject blue_rupee;
    [SerializeField] private enum ObjectType {pot, bush};
    [SerializeField] private ObjectType objectType;

    public void BreakObject() {
        gameObject.GetComponent<Animator>().SetTrigger("Break");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DelayDestroyRoutine(gameObject));
    }

    private IEnumerator DelayDestroyRoutine(GameObject other) {
        switch (objectType) {
            case ObjectType.pot: 
                Instantiate(blue_rupee, transform.position, Quaternion.identity);
                break;
            case ObjectType.bush: 
                break; 
        }

        yield return new WaitForSeconds(2f);
        Destroy(other);
    }
}
