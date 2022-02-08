using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Bullet bulletPf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(0, 0, 0.5f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0,0,-0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

    private void Shoot() {
        Bullet bullet = Instantiate(bulletPf, transform.position, Quaternion.identity);


        bullet.CreateBullet(transform.up);


    }

}
