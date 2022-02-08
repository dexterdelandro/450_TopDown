using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Bullet bulletPf;
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //screen to world point helps convert the pixel coordinates of the mouse to world coordinates

        transform.up = (mousePos - playerRb.position).normalized;   //make player point to mouse

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0, 0, 0.5f);

        //    transform.up = (mousePos - playerRb.position).normalized;
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(0, 0, -0.5f);
        //}

        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

    private void Shoot() {
        Bullet bullet = Instantiate(bulletPf, transform.position, Quaternion.identity);

        bullet.CreateBullet(transform.up);

        playerRb.AddForce(-transform.up, ForceMode2D.Impulse);
        //transform.position = Vector3.Lerp(transform.position, transform.position - transform.up, 1);

    }

}
