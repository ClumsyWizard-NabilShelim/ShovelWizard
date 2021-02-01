using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
	public Transform HoldPos;
	public float ThrowForce;
	float DefaultGravityScaleOfHeldObject;
	GameObject HeldObject;
	Rigidbody2D HeldObjectRB;
	public float Dist;
	public LayerMask Hitable;
	RaycastHit2D Col;
	bool Pull;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
			Col = Physics2D.Raycast(HoldPos.position, HoldPos.right, Dist, Hitable);

			if (Col)
			{
				Pull = true;
				HeldObject = Col.collider.gameObject;
				HeldObjectRB = HeldObject.GetComponent<Rigidbody2D>();
				DefaultGravityScaleOfHeldObject = HeldObjectRB.gravityScale;
				HeldObjectRB.gravityScale = 0;
				HeldObject.GetComponent<ContactObject>().Grabed = true;
				if (Col.collider.GetComponent<BossCrate>())
				{
					Col.collider.GetComponent<BossCrate>().Grabed = true;
				}
			}
		}

		if (Input.GetMouseButton(1))
		{
			if (HeldObject != null)
			{
				if (HeldObject.transform.position != HoldPos.position && Pull)
				{
					HeldObject.transform.position = Vector2.MoveTowards(HeldObject.transform.position, HoldPos.position, 10);
				}
				else
				{
					HeldObject.transform.parent = HoldPos;
				}
			}
		}

		if (Input.GetMouseButtonUp(1))
		{
			if(HeldObject != null && HeldObjectRB != null)
			{
				HeldObject.GetComponent<ContactObject>().Grabed = false;
				HeldObject.GetComponent<ContactObject>().Breakable = true;
				HeldObjectRB.AddForce(HoldPos.right * ThrowForce * Time.deltaTime, ForceMode2D.Impulse);
				HeldObjectRB.gravityScale = DefaultGravityScaleOfHeldObject;
				HeldObjectRB = null;
				HeldObject.transform.parent = null;
				HeldObject = null;
				Pull = true;
			}
		}
    }

	void OnDrawGizmos()
	{
		Gizmos.DrawRay(HoldPos.position, HoldPos.right * Dist);
	}
}
