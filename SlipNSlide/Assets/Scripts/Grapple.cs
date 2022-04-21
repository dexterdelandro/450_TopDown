using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
	LineRenderer lr;

	public Tutorial tutorial;

	public float grappleDistance;
	public float grappleSpeed;
	public float shootSpeed;
	public float repelSpeed;
	public DistanceJoint2D joint;
	[SerializeField] private GameObject hook;

	public LayerMask typeToGrab;

	private bool didFire = false;
	private bool connect = false;
	private bool wouldHit = false;
	private bool doneRetracting = true;

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
		if (connect && Input.GetMouseButton(1))
		{
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, targetPos);
			joint.connectedAnchor = targetPos;
			joint.enabled = true;
			joint.breakForce = float.PositiveInfinity;

			if (!tutorial.completed && tutorial.stepCounter == 1) {
				tutorial.stepCounter = 2;
			}


			//this is when grapple hook missed
		} else if (didFire && !wouldHit) {
			//Debug.Log("thingy");
			StartCoroutine(Wait(1));
			if(doneRetracting)
			StartCoroutine(RetractGrapple());
		}

		//pulls player to grappled location while they are scrolling up
		if (connect && Input.GetKey(KeyCode.Space)){
			if (!tutorial.completed) tutorial.didSpace = true;

			Vector2 grapplePos = Vector2.Lerp(transform.position, targetPos, grappleSpeed * Time.deltaTime);
			hook.transform.position = targetPos;
			hook.transform.right = targetPos - (Vector2)transform.position;
			transform.position = grapplePos;
			lr.SetPosition(0, transform.position);
			if (Vector2.Distance(transform.position, targetPos) < 1.0f) //might need to update distance allowed
			{
				if (doneRetracting)
					StartCoroutine(RetractGrapple());
				return;
			}
		}
		else if (connect && Input.mouseScrollDelta.y > 0)
		{
			if (!tutorial.completed) tutorial.didScrollWheelUp = true;

			Vector2 grapplePos = Vector2.Lerp(transform.position, targetPos, repelSpeed * Time.deltaTime);
			hook.transform.position = targetPos;
			hook.transform.right = targetPos - (Vector2)transform.position;
			transform.position = grapplePos;
			lr.SetPosition(0, transform.position);
			if (Vector2.Distance(transform.position, targetPos) < 1.0f) //might need to update distance allowed
			{
				if (doneRetracting)
					StartCoroutine(RetractGrapple());
				return;
			}
		} else if (connect && joint.distance < grappleDistance - 1 && Input.mouseScrollDelta.y < 0) {
			if (!tutorial.completed) tutorial.didScrollWheelDown = true;

			Vector2 grapplePos = Vector2.Lerp(transform.position, 2 * ((Vector2)transform.position - targetPos), repelSpeed * Time.deltaTime);
			hook.transform.position = targetPos;
			hook.transform.right = targetPos - (Vector2)transform.position;
			transform.position = grapplePos;
			lr.SetPosition(0, transform.position);
			if (joint.distance > grappleDistance - 1) //might need to update distance allowed
			{
				joint.distance = grappleDistance - 1;
			}
		}

		if (connect && Input.GetMouseButtonUp(1))
		{
			if(doneRetracting)
			StartCoroutine(RetractGrapple());
		}
		hook.GetComponent<SpriteRenderer>().enabled = lr.enabled;
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
			wouldHit = true;
		}
		else
		{
			targetPos = transform.position + (Vector3)direction * grappleDistance;
			wouldHit = false;
		}

		Debug.Log(wouldHit);
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
			if (!connect && Input.GetMouseButtonUp(1)) {
				if (doneRetracting)
				StartCoroutine(RetractGrapple());
			}
			currentPos = Vector2.Lerp(transform.position, targetPos, t / totalTime);
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, currentPos);
			hook.transform.position = currentPos;
			hook.transform.right = currentPos - (Vector2)transform.position;
			yield return null;
		}

		lr.SetPosition(1, targetPos);
		if(wouldHit)connect = true;
		didFire = true;
	}

	//Retracts grapple to player pos after a missed shot
	IEnumerator RetractGrapple()
	{
		doneRetracting = false;
		float t = 0;
		float totalTime = 10;

		lr.SetPosition(0, targetPos);
		lr.SetPosition(1, transform.position);

		Vector2 currentPos;

		for (t = 0; t < totalTime; t += shootSpeed * Time.deltaTime * 1.5f)
		{
			currentPos = Vector2.Lerp(targetPos, transform.position, t / totalTime);
			lr.SetPosition(0, currentPos);
			lr.SetPosition(1, transform.position);
			hook.transform.position = currentPos;
			hook.transform.right = currentPos - (Vector2)transform.position;
			yield return null;
		}

		lr.SetPosition(1, transform.position);

		//reset bools ready for next shot
		yield return null;
		ResetGrapple();
	}

	IEnumerator Wait(float x) {
		yield return new WaitForSeconds(x);
	}

	private void ResetGrapple() {
		joint.breakForce = 0;
		joint.enabled = false;
		lr.enabled = false;
		connect = false;
		didFire = false;
		doneRetracting = true;
	}

}

