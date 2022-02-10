using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    public Bullet bulletPf;
    private Rigidbody2D playerRb;
    public Camera mainCam;
    private Vector2 camPosition;
    private Vector3 camSway;
    private float camSpeed;
    [SerializeField] private float swayStrength;
    [SerializeField] private Vector2 camMax;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        camPosition = new Vector2(0, 0);
        camSway = new Vector2(0, 0);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        //camera movement

        //adjusts sway dynamically

        camSway = (Input.mousePosition - transform.position) * swayStrength;
        if (camSway.x > camMax.x)
        {
            camSway.x = camMax.x;
        }
        if (camSway.y > camMax.y)
        {
            camSway.y = camMax.y;
        }
        if (camSway.x < -camMax.x)
        {
            camSway.x = -camMax.x;
        }
        if (camSway.y < -camMax.y)
        {
            camSway.y = -camMax.y;
        }

        camPosition = playerRb.transform.position + camSway;

        mainCam.transform.position = new Vector3(camPosition.x, camPosition.y, -10);
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPf, transform.position, Quaternion.identity);

        bullet.CreateBullet(transform.up);

        playerRb.AddForce(-transform.up, ForceMode2D.Impulse);
        //transform.position = Vector3.Lerp(transform.position, transform.position - transform.up, 1);

    }

}
