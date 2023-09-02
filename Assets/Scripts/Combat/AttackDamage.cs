using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AttackDamage class can be put on any object with a trigger collider to set off different instances of damage.
public class AttackDamage : MonoBehaviour
{
    #region Public Variables

    // turned on in Bomb.cs script
    public bool isBombExplosion = false;

    #endregion

    #region Private Variables 

    [SerializeField] private int damageAmount;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockBackThrust;
    private const string enemyString = "Enemy";
    private const string playerString = "Player";

    #endregion

    #region Unity Methods

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyAttack(other);
        PlayerSelfDamage(other);
        DestructibleAttack(other);
        DestroyBoulderBlockingCave(other);
    }

    #endregion

    #region Private Methods

    private void EnemyAttack(Collider2D other) {
        if (other.gameObject.CompareTag(enemyString)) {
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
            other.GetComponent<KnockBack>().getKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        }
    }

    private void PlayerSelfDamage(Collider2D other) {
        if (other.gameObject.CompareTag(playerString)) {
            other.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            other.GetComponent<KnockBack>().getKnockedBack(transform, knockBackThrust);
        }
    }

    // Applies to bushes and pots
    private void DestructibleAttack(Collider2D other) {
        if (other.GetComponent<Breakable>()) {
            other.GetComponent<Breakable>().BreakObject();
        }
    }

    // Cave will only be destroyed by the instance bomb explosion prefab
    private void DestroyBoulderBlockingCave(Collider2D other) {
        if (other.GetComponent<Boulder>() && isBombExplosion) {
            other.GetComponent<Boulder>().DestroyBoulder();
        }
    }

    #endregion
}
