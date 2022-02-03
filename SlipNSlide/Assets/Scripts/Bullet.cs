using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public void CreateBullet(Vector2 direction) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Debug.Log(direction);
        rb.AddForce(direction , ForceMode2D.Impulse);
    }
}
