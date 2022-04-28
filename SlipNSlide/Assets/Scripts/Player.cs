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
    [SerializeField] private Text ammoText;
    [SerializeField] private GameObject arm;
    [SerializeField] private Sprite[] weapons;
    [SerializeField] private int[] ammo;
    [SerializeField] private int[] magAmmo;
    [SerializeField] private int[] magSize;
    [SerializeField] private float[] fireRate;
    [SerializeField] private float[] reloadSpeed;
    [SerializeField] private GameObject weapon;
    [SerializeField] private MuzzleFlash muzzleFlash;
    [SerializeField] private Sprite[] bigWeaponImages;
    [SerializeField] private Sprite[] smallWeaponImages;
    [SerializeField] private Image[] hudWeaponImages;

    public int health;
    private float shootTimer = 0;

    SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<magAmmo.Length; i++)
        {
            Reload(i);
        }
        playerRb = GetComponent<Rigidbody2D>();
        camPosition = new Vector2(0, 0);
        camSway = new Vector2(0, 0);
        currentWeapon = 0;
        health = 100;

        sceneController = GameObject.Find("EventSystem").GetComponent<SceneController>();

        //pausemenu.SetActive(false);
        //if (pausemenu.transform.childCount > 0 && pausemenu.transform.GetChild(0).childCount > 4)
        //{
        //    pausemenu.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ResumeGame);
        //    pausemenu.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(SceneController.controller.LoadMenu);
        //    pausemenu.transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(SceneController.controller.LoadEnd);
        //}
        //else
        //{
        //    Debug.Log("error");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
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


        //camera movement

        //adjusts sway dynamically

        //camSway = (Input.mousePosition - playerRb.transform.position - new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0)) * swayStrength;
        //if (camSway.x > camMax.x)
        //{
        //    camSway.x = camMax.x;
        //}
        //if (camSway.y > camMax.y)
        //{
        //    camSway.y = camMax.y;
        //}
        //if (camSway.x < -camMax.x)
        //{
        //    camSway.x = -camMax.x;
        //}
        //if (camSway.y < -camMax.y)
        //{
        //    camSway.y = -camMax.y;
        //}

        ////had to add if statement so that these lines do not override the camera shake while shooting
        //if (!mainCam.GetComponent<CameraShake>().shaketrue)
        //{
        //    Vector2 newPosition = playerRb.transform.position + camSway;
        //    camPosition = camPosition + ((newPosition - camPosition) * camSpeed * Time.deltaTime);

        //    mainCam.transform.position = new Vector3(camPosition.x, camPosition.y, -10);
        //}


        camSway = mainCam.GetComponent<CameraShake>().camSway;
        camMax = mainCam.GetComponent<CameraShake>().camMax;

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

        if ((magAmmo[currentWeapon] > 0) && (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && shootTimer <= 0)))
        {
            Shoot();
            mainCam.GetComponent<CameraShake>().shakecamera(.5f, 1.5f);
        }
        shootTimer -= Time.deltaTime;

        if(magAmmo[currentWeapon] == 0 && shootTimer <= 0)
        {
            Reload(currentWeapon);
        }

        /*if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentWeapon = (currentWeapon + 1) % 3;
            UpdateAmmoText();

        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentWeapon = (currentWeapon + 2) % 3;
            UpdateAmmoText();
        }*/

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            hudWeaponImages[0].sprite = bigWeaponImages[0];
            hudWeaponImages[1].sprite = smallWeaponImages[1];
            hudWeaponImages[2].sprite = smallWeaponImages[2];
            UpdateAmmoText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            hudWeaponImages[0].sprite = bigWeaponImages[1];
            hudWeaponImages[1].sprite = smallWeaponImages[2];
            hudWeaponImages[2].sprite = smallWeaponImages[0];
            UpdateAmmoText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            hudWeaponImages[0].sprite = bigWeaponImages[2];
            hudWeaponImages[1].sprite = smallWeaponImages[0];
            hudWeaponImages[2].sprite = smallWeaponImages[1];
            UpdateAmmoText();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            sceneController.HelpButtonPressed();    //pulls up pausemenu

        }

    }

    //private void ResumeGame()
    //{
    //    Time.timeScale = 1.0f;
    //    sceneController.HelpButtonPressed();    //closes pause menu
    //}

    private void Shoot()
    {
        magAmmo[currentWeapon]--;
        UpdateAmmoText();
        if(camSway.x < 0)
        {
            switch (currentWeapon)
            {
                case 0:
                    FireBullet(.12f, .8f, 1, 1, 20, 0);
                    break;
                case 1:
                    FireBullet(.06f, 1, 2, 1, 20, 0);
                    FireBullet(0f, 1, 2, 1, 20, Mathf.PI / 24);
                    FireBullet(.12f, 1, 2, 1, 20, -Mathf.PI / 24);
                    break;
                case 2:
                    FireBullet(.07f, 1.1f, .3f, .5f, 20, 0);
                    break;
            }
        }
        else
        {
            switch (currentWeapon)
            {
                case 0:
                    FireBullet(-.12f, .8f, 1, 1, 20, 0);
                    break;
                case 1:
                    FireBullet(-.06f, 1, 2, 1, 20, 0);
                    FireBullet(-.12f, 1, 2, 1, 20, Mathf.PI / 24);
                    FireBullet(0f, 1, 2, 1, 20, -Mathf.PI / 24);
                    break;
                case 2:
                    FireBullet(-.07f, 1.1f, .3f, .5f, 20, 0);
                    break;
            }
        }
        shootTimer = fireRate[currentWeapon];
        if (magAmmo[currentWeapon] == 0)
        {
            shootTimer = reloadSpeed[currentWeapon];
        }

        //transform.position = Vector3.Lerp(transform.position, transform.position - transform.up, 1);

    }

    private void FireBullet(float rightShift, float muzzleDistance, float forceModifier, float damage, float speed, float angle)
    {
        Bullet bullet = Instantiate(bulletPf, arm.transform.position + aimVector * (muzzleDistance + .12f) + arm.transform.right * rightShift, playerRb.transform.rotation);
        Instantiate(muzzleFlash, arm.transform.position + aimVector * muzzleDistance + arm.transform.right * rightShift, playerRb.transform.rotation);
        playerRb.AddForce(-aimVector * forceModifier, ForceMode2D.Impulse);
        bullet.SetDamage(damage);
        bullet.SetSpeed(speed);
        bullet.CreateBullet(rotate(aimVector, angle));
        bullet.transform.up = arm.transform.up;
    }

    private void Reload(int weapon)
    {
        if (ammo[weapon] >= magSize[weapon])
        {
            magAmmo[weapon] = magSize[weapon];
            ammo[weapon] -= magSize[weapon];
            if (weapon == 0)
            {
                ammo[weapon] = 1000;
            }
        }
        else if (ammo[weapon] >= 0)
        {
            magAmmo[weapon] = ammo[weapon];
            ammo[weapon] = 0;
        }
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        if(currentWeapon != 0)
        {
            ammoText.text = magAmmo[currentWeapon] + "/" + ammo[currentWeapon];
        }
        else
        {
            ammoText.text = magAmmo[currentWeapon] + "/∞";
        }
    }

    //from Boz0r on unity forum: https://forum.unity.com/threads/whats-the-best-way-to-rotate-a-vector2-in-unity.729605/
    private Vector2 rotate(Vector2 v, float angle)
    {
        return new Vector2(
            v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle),
            v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle)
        );
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
	//}22

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Medkit")
        {
            health += 35;
            if (health > 100) health = 100;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ammo") {
            ammo[1] += 6;
            ammo[2] += 90;
            Destroy(collision.gameObject);
            UpdateAmmoText();
        }
    }

}
