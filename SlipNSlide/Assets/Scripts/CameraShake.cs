using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Vector3 camSway;
    public float camSpeed;
    public Vector2 camMax;
    [SerializeField] private float swayStrength;

    public Rigidbody2D playerRb;
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 2f;
    public float decreaseFactor = 1.0f;

    public bool shaketrue = false;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
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

        //Add camera sway to camera pos
        Vector2 newPosition = playerRb.transform.position + camSway;
        transform.position = (Vector2)transform.position + ((newPosition - (Vector2)transform.position) * camSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        //do camera shake
        if (shaketrue)
        {
            if (shakeDuration > 0)
            {
                camTransform.position = Vector3.Lerp(camTransform.position, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shaketrue = false;
            }
        }

      
    }

    public void shakecamera(float _shakeDuration, float _shakeAmount)
    {
        originalPos = camTransform.position;
        shaketrue = true;
        shakeDuration = _shakeDuration;
        shakeAmount = _shakeAmount;
    }
}
