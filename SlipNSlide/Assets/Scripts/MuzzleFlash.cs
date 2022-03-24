using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private float visibleTime;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        visibleTime = .1f;
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - currentTime / visibleTime);

        if(currentTime > visibleTime)
        {
            Destroy(this);
        }
    }
}
