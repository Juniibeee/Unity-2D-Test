using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explodePrefab;

    // Use in Bomb animation
    public void Explode() {
        GameObject newBomb = Instantiate(explodePrefab, transform.position, transform.rotation);
        newBomb.GetComponent<AttackDamage>().isBombExplosion = true;
        Destroy(gameObject);
    }
}
