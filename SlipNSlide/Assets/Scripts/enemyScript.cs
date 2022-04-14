using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    //Enemy Stats
    public float speed;
    public float health;
    public float fireRate;

    public float awareness;
    public float attackRange;

    public GameObject manager;
    public GameObject backArm;
    public GameObject frontArm;
    public SpriteRenderer weapon;
    public Sprite[] weaponSprites;
    [SerializeField] private MuzzleFlash muzzleFlash;
    Score score;

    //Player based info
    public Transform target;
    private Vector3 aimVector;

    //Ammo type
    public bulletScript bulletPf;
    private int clipSize;
    private int currentAmmo;
    private float reloadTimer;
    private float fireTimer;
    private int currentWeapon;

    private State currentState;
    private enum State
    {
        Casual,
        Battle
    }

    void Start()
    {
        float difficultyScale = 1.5f;

        manager = GameObject.Find("Manager");
        score = manager.GetComponent<Score>();
        target = GameObject.Find("player").transform;
        currentState = State.Casual;

        switch (Random.Range(0, 3))
        {
            case 0:
                weapon.sprite = weaponSprites[0];
                health = 4;
                currentWeapon = 0;
                clipSize = Random.Range(1,4);
                reloadTimer = .6f * difficultyScale;
                fireTimer = .5f * difficultyScale;
                break;
            case 1:
                weapon.sprite = weaponSprites[1];
                health = 2;
                currentWeapon = 1;
                clipSize = Random.Range(3, 9);
                reloadTimer = 1.5f * difficultyScale;
                fireTimer = .05f * difficultyScale;
                break;
            case 2:
                weapon.sprite = weaponSprites[2];
                health = 5;
                currentWeapon = 2;
                clipSize = Random.Range(1, 3);
                reloadTimer = 1f * difficultyScale;
                fireTimer = .8f * difficultyScale;
                break;
        }
        currentAmmo = clipSize;
    }

    void Update()
    {
        switch (currentState)
        {
            //Stand still if not in combat
            case State.Casual:
                transform.position = this.transform.position;

                FindTarget();
                break;

            case State.Battle:
                RepositionLimbs();
                //If player is out of shooting range, move in
                if (Vector2.Distance(transform.position, target.position) > attackRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                }
                //If player is within range, stop moving toward them
                else if (Vector2.Distance(transform.position, target.position) <= attackRange)
                {
                    transform.position = this.transform.position;

                    Attack();
                }
                break;
        }
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Collide with player bullets
    //    if (collision.CompareTag("Player") || collision.CompareTag("playerBullet"))
    //    {
    //        TakeDamage();
    //    }
    //}

    void FindTarget()
    {
        if (Vector2.Distance(transform.position, target.position) < awareness)
        {
            currentState = State.Battle;
        }
    }

    void Attack()
    {
        if(fireRate <= 0)
        {
            if (aimVector.x < 0)
            {
                switch (currentWeapon)
                {
                    case 0:
                        FireEnemyBullet(.12f, .55f, 2, 12, 0);
                        break;
                    case 1:
                        FireEnemyBullet(.07f, .6f, 1, 12, 0);
                        break;
                    case 2:
                        FireEnemyBullet(.06f, .55f, 2, 12, 0);
                        FireEnemyBullet(0f, .55f, 2, 12, Mathf.PI / 24);
                        FireEnemyBullet(.12f, .55f, 2, 12, -Mathf.PI / 24);
                        break;
                }
            }
            else
            {
                switch (currentWeapon)
                {
                    case 0:
                        FireEnemyBullet(-.12f, .55f, 2, 12, 0);
                        break;
                    case 1:
                        FireEnemyBullet(-.07f, .6f, 1, 12, 0);
                        break;
                    case 2:
                        FireEnemyBullet(-.06f, .55f, 2, 12, 0);
                        FireEnemyBullet(-.12f, .55f, 2, 12, Mathf.PI / 24);
                        FireEnemyBullet(0f, .55f, 2, 12, -Mathf.PI / 24);
                        break;
                }
            }
            fireRate = fireTimer;
            currentAmmo--;
            if(currentAmmo <= 0)
            {
                currentAmmo = clipSize;
                fireRate = reloadTimer;
            }
        }
        else 
        {
            fireRate -= Time.deltaTime;
        }
    }

    private void FireEnemyBullet(float rightShift, float muzzleDistance, int damage, float speed, float angle)
    {
        bulletScript bullet = Instantiate(bulletPf, backArm.transform.position + aimVector * (muzzleDistance + .12f) + backArm.transform.right * rightShift, transform.rotation);
        Instantiate(muzzleFlash, backArm.transform.position + aimVector * muzzleDistance + backArm.transform.right * rightShift, transform.rotation);
        bullet.SetDamage(damage);
        bullet.SetSpeed(speed);
        bullet.SetRotation(rotate(aimVector, angle));
    }

    //from Boz0r on unity forum: https://forum.unity.com/threads/whats-the-best-way-to-rotate-a-vector2-in-unity.729605/
    private Vector2 rotate(Vector2 v, float angle)
    {
        return new Vector2(
            v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle),
            v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle)
        );
    }

    void RepositionLimbs()
    {
        aimVector = (target.position - new Vector3(transform.position.x, backArm.transform.position.y, 0)).normalized;
        backArm.transform.up = aimVector;


        //flips sprite
        if (aimVector.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            weapon.flipY = false;
            backArm.transform.localPosition = new Vector3(.435f, .803f, 0);
            weapon.transform.right = backArm.transform.up;
            weapon.transform.position = backArm.transform.position + backArm.transform.up * .45f + backArm.transform.right * -.034f;
            frontArm.transform.localPosition = new Vector3(.287f, .741f, 0);
            switch (currentWeapon)
            {
                case 0:
                    weapon.transform.position = backArm.transform.position + backArm.transform.up * .45f + backArm.transform.right * -.034f;
                    frontArm.transform.localPosition = new Vector3(.287f, .741f, 0);
                    frontArm.transform.up = weapon.transform.position - frontArm.transform.position;
                    break;
                case 1:
                    weapon.transform.position = backArm.transform.position + backArm.transform.up * .16f;
                    frontArm.transform.localPosition = new Vector3(.25f, .652f, 0);
                    frontArm.transform.up = weapon.transform.position - frontArm.transform.position;
                    break;
                case 2:
                    weapon.transform.position = backArm.transform.position + backArm.transform.up * .052f + backArm.transform.right * .06f;
                    frontArm.transform.localPosition = new Vector3(.041f, .633f, 0);
                    frontArm.transform.up = weapon.transform.position + weapon.transform.right * -.1f + weapon.transform.up * -.04f - frontArm.transform.position;
                    break;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            weapon.flipY = true;
            backArm.transform.localPosition = new Vector3(-.435f, .803f, 0);
            weapon.transform.position = backArm.transform.position + backArm.transform.up * .45f + backArm.transform.right * .034f;
            frontArm.transform.position = transform.position + new Vector3(-.287f, .741f, 0);
            switch (currentWeapon)
            {
                case 0:
                    weapon.transform.position = backArm.transform.position + backArm.transform.up * .45f + backArm.transform.right * .034f;
                    frontArm.transform.localPosition = new Vector3(-.287f, .741f, 0);
                    frontArm.transform.up = weapon.transform.position - frontArm.transform.position;
                    break;
                case 1:
                    weapon.transform.position = backArm.transform.position + backArm.transform.up * .16f;
                    frontArm.transform.localPosition = new Vector3(-.25f, .652f, 0);
                    frontArm.transform.up = weapon.transform.position - frontArm.transform.position;
                    break;
                case 2:
                    weapon.transform.position = backArm.transform.position + backArm.transform.up * .052f + backArm.transform.right * -.06f;
                    frontArm.transform.localPosition = new Vector3(-.041f, .633f, 0);
                    frontArm.transform.up = weapon.transform.position + weapon.transform.right * -.1f + weapon.transform.up * .04f - frontArm.transform.position;
                    break;
            }
        }
    }

    public void TakeDamage(float d)
    {
        health-= d;
        if (health <= 0)
        {
            score.addScore(1);
            Destroy(gameObject);
        }
    }
}
