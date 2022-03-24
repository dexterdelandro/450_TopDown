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
    public float camSpeed;
    public int currentWeapon;
    private Vector3 aimVector;
    [SerializeField] private float swayStrength;
    [SerializeField] private Vector2 camMax;
    [SerializeField] private Sprite fastSprite;
    [SerializeField] private Sprite slowSprite;
    [SerializeField] private GameObject arm;
    [SerializeField] private Sprite[] weapons;
    [SerializeField] private GameObject weapon;
    [SerializeField] private MuzzleFlash muzzleFlash; 

    public uint health;
    Score score;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        camPosition = new Vector2(0, 0);
        camSway = new Vector2(0, 0);
        currentWeapon = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //screen to world point helps convert the pixel coordinates of the mouse to world coordinates

        aimVector = (mousePos - playerRb.position).normalized;   //make player point to mouse
        arm.transform.up = aimVector;
        if (GetComponent<SpriteRenderer>().flipX)
        {
            if (playerRb.velocity.magnitude > 3)
            {
                arm.transform.position = playerRb.position + new Vector2(.03f, .29f);
            }
            else
            {
                arm.transform.position = playerRb.position + new Vector2(-.177f, .17f);
            }
        }
        else
        {
            if (playerRb.velocity.magnitude > 3)
            {
                arm.transform.position = playerRb.position + new Vector2(-.03f, .29f);
            }
            else
            {
                arm.transform.position = playerRb.position + new Vector2(.177f, .17f);
            }
        }
        weapon.GetComponent<SpriteRenderer>().sprite = weapons[currentWeapon];
        weapon.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipX;
        weapon.transform.position = arm.transform.position + arm.transform.up * .76f + arm.transform.right * -.03f;
        switch (currentWeapon)
        {
            case 0:
                weapon.transform.position = arm.transform.position + arm.transform.up * .76f + arm.transform.right * -.03f;
                break;
            case 1:
                weapon.transform.position = arm.transform.position + arm.transform.up * .34f;
                break;
            case 2:
                weapon.transform.position = arm.transform.position + arm.transform.up * .34f + arm.transform.right * -.05f;
                break;
        }
        weapon.transform.right = arm.transform.up;

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0, 0, 0.5f);

        //    transform.up = (mousePos - playerRb.position).normalized;
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(0, 0, -0.5f);
        //}

<<<<<<< Updated upstream
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

=======
>>>>>>> Stashed changes
        //camera movement

        //adjusts sway dynamically

        camSway = (Input.mousePosition - playerRb.transform.position - new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0)) * swayStrength;
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

        Vector2 newPosition = playerRb.transform.position + camSway;
        camPosition = camPosition + ((newPosition - camPosition) * camSpeed * Time.deltaTime);

        mainCam.transform.position = new Vector3(camPosition.x, camPosition.y, -10);

        //flips player sprite
        if(camSway.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //changes player sprite
        if(playerRb.velocity.magnitude > 3)
        {
            GetComponent<SpriteRenderer>().sprite = fastSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = slowSprite;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            mainCam.GetComponent<CameraShake>().shakecamera(.5f, 1.5f);
        }
    }

    private void Shoot()
    {
        Bullet bullet;
        if(camSway.x < 0)
        {
            switch (currentWeapon)
            {
                case 0:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * .12f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * .8f + arm.transform.right * .12f, playerRb.transform.rotation);
                    break;
                case 1:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * .06f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * 1f + arm.transform.right * .06f, playerRb.transform.rotation);
                    break;
                case 2:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * .07f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * 1.1f + arm.transform.right * .07f, playerRb.transform.rotation);
                    break;
                default:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * .12f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * .8f + arm.transform.right * .12f, playerRb.transform.rotation);
                    break;

            }
        }
        else
        {
            switch (currentWeapon)
            {
                case 0:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * -.12f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * .8f + arm.transform.right * -.12f, playerRb.transform.rotation);
                    break;
                case 1:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * -.06f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * 1f + arm.transform.right * -.06f, playerRb.transform.rotation);
                    break;
                case 2:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * -.07f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * 1.1f + arm.transform.right * -.07f, playerRb.transform.rotation);
                    break;
                default:
                    bullet = Instantiate(bulletPf, arm.transform.position + aimVector * 2 + arm.transform.right * -.12f, playerRb.transform.rotation);
                    Instantiate(muzzleFlash, arm.transform.position + aimVector * .8f + arm.transform.right * -.12f, playerRb.transform.rotation);
                    break;
            }
        }

        bullet.CreateBullet(aimVector);
        bullet.transform.up = arm.transform.up;

        playerRb.AddForce(-aimVector, ForceMode2D.Impulse);
        //transform.position = Vector3.Lerp(transform.position, transform.position - transform.up, 1);

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Collide with player bullets

    //    //if (collision.gameObject.tag == "enemyBullet")
    //    //{
    //    //    Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
    //    //}

    //    if (collision.gameObject.tag == "enemyBullet")
    //    {
    //        health -= 10;
    //    }
    //}

}
