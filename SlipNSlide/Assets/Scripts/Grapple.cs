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

	private bool didFire = false;
	private bool retracting = false;
	private bool didHit = false;


	private Vector2 targetPos;

	// Start is called before the first frame update
	void Start()
	{
		lr = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!didFire && Input.GetMouseButtonDown(1))
		{
			DoGrapple();
		}

		//This is when grapple hook hit and moving player to grapple location
		if (retracting)
		{
			Vector2 grapplePos = Vector2.Lerp(transform.position, targetPos, grappleSpeed * Time.deltaTime);
			transform.position = grapplePos;
			lr.SetPosition(0, transform.position);
			if (Vector2.Distance(transform.position, targetPos) < 1.0f) //might need to update distance allowed
			{
				//reset bools ready for next shot
				retracting = false;
				didFire = false;
				lr.enabled = false;
			}
			//this is when grapple hook missed
		} else if (didFire && !didHit) {
			//Debug.Log("thingy");
			StartCoroutine(Wait(1));
			StartCoroutine(RetractGrapple());
		}
	}

	//fire grapple
	private void DoGrapple()
	{
		didFire = false;
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		direction = direction.normalized;


		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleDistance, typeToGrab);

		//means that it hit something
		if (hit.collider != null)
		{
			targetPos = hit.point;
			didHit = true;
		}
		else
		{
			targetPos = transform.position + (Vector3)direction * grappleDistance;
			didHit = false;
		}

		Debug.Log(didHit);
		//targetPos = hit.point;

		lr.enabled = true;
		lr.positionCount = 2;

		StartCoroutine(FireGrapple());
		
	}

	//shoots out the grapple
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
		if (didHit) retracting = true;
		didFire = true;
	}

	//Retracts grapple to player pos after a missed shot
	IEnumerator RetractGrapple()
	{
		float t = 0;
		float totalTime = 10;

		lr.SetPosition(0, targetPos);
		lr.SetPosition(1, transform.position);

		Vector2 currentPos;

		for (t = 0; t < totalTime; t += shootSpeed * Time.deltaTime)
		{
			currentPos = Vector2.Lerp(targetPos, transform.position, t / totalTime);
			lr.SetPosition(0, currentPos);
			lr.SetPosition(1, transform.position);
			yield return null;
		}

		lr.SetPosition(1, transform.position);

		//reset bools ready for next shot
		retracting = false;
		didFire = false;
		lr.enabled = false;
	}

	IEnumerator Wait(float x) {
		yield return new WaitForSeconds(x);
	}


}

