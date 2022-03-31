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
    private Vector3 aimVector;
    [SerializeField] private float swayStrength;
    [SerializeField] private Vector2 camMax;
    [SerializeField] private Sprite fastSprite;
    [SerializeField] private Sprite slowSprite;

    public uint health;
    Score score;

    private bool paused;
    GameObject pausemenu;

    private void Awake()
    {
        pausemenu = GameObject.Find("PauseMenu");
        pausemenu.SetActive(false);

        

    }
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        camPosition = new Vector2(0, 0);
        camSway = new Vector2(0, 0);

        health = 100;

        if (pausemenu.transform.childCount > 0 && pausemenu.transform.GetChild(0).childCount > 4)
        {
            pausemenu.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ResumeGame);
            pausemenu.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(SceneController.controller.LoadMenu);
            pausemenu.transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(SceneController.controller.LoadEnd);
        }
        else
        {
            Debug.Log("error");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //screen to world point helps convert the pixel coordinates of the mouse to world coordinates

        aimVector = (mousePos - playerRb.position).normalized;   //make player point to mouse

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            mainCam.GetComponent<CameraShake>().shakecamera(.5f, 1.5f);
        }

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

        //had to add if statement so that these lines do not override the camera shake while shooting
        if (!mainCam.GetComponent<CameraShake>().shaketrue)
        {
            Vector2 newPosition = playerRb.transform.position + camSway;
            camPosition = camPosition + ((newPosition - camPosition) * camSpeed * Time.deltaTime);

            mainCam.transform.position = new Vector3(camPosition.x, camPosition.y, -10);
        }


        //flips player sprite
        if(camPosition.x < playerRb.transform.position.x)
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
<<<<<<< Updated upstream
=======

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            mainCam.GetComponent<CameraShake>().shakecamera(.5f, 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;

            if (paused)
            {
                Time.timeScale = 0;
                pausemenu.SetActive(true);

            }
            else {
                Time.timeScale = 1;
            }
        }

>>>>>>> Stashed changes
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPf, transform.position + aimVector*2, playerRb.transform.rotation);

        bullet.CreateBullet(aimVector);

        playerRb.AddForce(-aimVector, ForceMode2D.Impulse);
        //transform.position = Vector3.Lerp(transform.position, transform.position - transform.up, 1);

    }


    private void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pausemenu.SetActive(false);
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
