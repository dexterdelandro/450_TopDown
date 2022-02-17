using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
	LineRenderer lr;

	public float grappleDistance;
	public float grappleSpeed;
	public float shootSpeed;

	public LayerMask typeToGrab;

	private bool grappling = false;
	private bool retracting = false;


	private Vector2 targetPos;

	// Start is called before the first frame update
	void Start()
	{
		lr = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!grappling && Input.GetMouseButtonDown(1))
		{
			DoGrapple();
		}


		if (retracting)
		{
			Vector2 grapplePos = Vector2.Lerp(transform.position, targetPos, grappleSpeed * Time.deltaTime);
			transform.position = grapplePos;
			lr.SetPosition(0, transform.position);
			if (Vector2.Distance(transform.position, targetPos) < 1.0f)
			{
				retracting = false;
				grappling = false;
				lr.enabled = false;
			}
		}
	}

	private void DoGrapple()
	{
		Debug.Log("LOL");
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		direction = direction.normalized;


		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleDistance, typeToGrab);

		//means that it hit something
		if (hit.collider != null) 
		
		//Debug.Log("hit");
		grappling = true;
		targetPos = hit.point;
		lr.enabled = true;
		lr.positionCount = 2;

		StartCoroutine(FireGrapple());
		
	}

	IEnumerator FireGrapple()
	{
		float t = 0;
		float totalTime = 10;

		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, transform.position);

		Vector2 currentPos;

		for (t = 0; t < totalTime; t += shootSpeed * Time.deltaTime)
		{
			currentPos = Vector2.Lerp(transform.position, targetPos, t / totalTime);
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, currentPos);
			yield return null;
		}

		lr.SetPosition(1, targetPos);
		retracting = true;
	}


}

