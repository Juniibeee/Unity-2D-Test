using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Keeping Bomb as placeholder if you wanted to implement something like item limited amounts 
    public enum TypeOfPickUp{Rupee, Bomb};
    public TypeOfPickUp typeOfPickUp;

    private const string playerString = "Player";

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerString)) {
            Destroy(gameObject);

            if (typeOfPickUp == TypeOfPickUp.Rupee) {
                PickUpRupee();
            }
        }
    }

    private void PickUpRupee() {
        FindObjectOfType<RupeeWallet>().IncreaseRupeeCount(1);
    }
}
