using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] float attackDamage;
    [SerializeField] float Speed;
    [SerializeField] private Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.left * Speed * Time.fixedDeltaTime);
        if (transform.position.x < -9)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.tag == "Player"){
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                Destroy(this.gameObject);
            }
        }
}

