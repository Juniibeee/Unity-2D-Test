using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knockback class can be put on gameobjects that you want to thrust back with rigidbody force against other objects that would typically deal damage
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockbackTime;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void getKnockedBack(Transform damageSource, float knockBackThrust) {
        Vector2 difference = transform.position - damageSource.position;
        difference = difference.normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);

        // if KnockBack class is on our player game object
        if (GetComponent<PlayerController>()) {
            PlayerController.Instance.canMove = false;
        }

        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine() {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;

        // if KnockBack class is on our player game object
        if (GetComponent<PlayerController>()) {
            PlayerController.Instance.canMove = true;
            GetComponent<PlayerHealth>().CheckIfDeath();
        }
    }
}
