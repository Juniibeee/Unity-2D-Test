using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Public Variables

    public int currentHealth;
    public int maxHealth;

    #endregion

    #region Private Variables
    
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float whiteFlashTime = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float respawnTimeFloat = 2f;
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private bool canTakeDamage = true;
    private bool isDead = false;
    private Rigidbody2D rb;

    #endregion

    #region Unity Methods

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
        maxHealth = startingHealth;
        defaultMat = spriteRenderer.material;
    }

    private void OnCollisionStay2D(Collision2D other) { 
        if (other.gameObject.CompareTag("Enemy") && canTakeDamage && currentHealth > 0) {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            TakeDamage(enemy.damageDoneToHero);
            GetComponent<KnockBack>().getKnockedBack(other.gameObject.transform, enemy.enemyKnockBackThrust);
        }
    }

    #endregion

    #region Public Methods

    public void CheckIfDeath() {
        if (currentHealth <= 0 && !isDead) {
            // isDead set to prevent death animation from triggering multiple times
            isDead = true;
            PlayerController.Instance.canMove = false;
            PlayerController.Instance.canAttack = false;
            myAnimator.SetTrigger("dead");
            StartCoroutine(RespawnRoutine());
        } else {
            PlayerController.Instance.canMove = true;
            PlayerController.Instance.canAttack = true;
        }
    }

    public void TakeDamage(int damage) {
        spriteRenderer.material = whiteFlashMat;
        currentHealth -= damage;
        canTakeDamage = false;
        StartCoroutine(SetDefaultMatRoutine());
        StartCoroutine(DamageRecoveryTimeRoutine());
    }

    #endregion

    #region Private Coroutines

    private IEnumerator SetDefaultMatRoutine() {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = defaultMat;
    }

    private IEnumerator DamageRecoveryTimeRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator RespawnRoutine() {
        yield return new WaitForSeconds(respawnTimeFloat);
        Destroy(PlayerController.Instance.gameObject);
        SceneManager.LoadScene("Town");
    }

    #endregion
}
