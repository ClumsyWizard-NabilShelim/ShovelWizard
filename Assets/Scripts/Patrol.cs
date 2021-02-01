using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
	public bool CantMove;
	Animator Anim;
/*	[HideInInspector] */public bool CanMove = true;
	bool FacingRight;
	public float RayDist;
	public float speed;
	RaycastHit2D HitHori;
	RaycastHit2D HitVerti;
	public LayerMask Hitable;
	public Transform CheckPos;
	public Transform WallCheckPos;

    void Start()
    {
		if (GetComponent<Animator>() != null)
		{
			Anim = GetComponent<Animator>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Anim.SetBool("Walk", true);
		if (CanMove && !CantMove)
		{
			HitHori = Physics2D.Raycast(WallCheckPos.position, transform.right, RayDist, Hitable);
			HitVerti = Physics2D.Raycast(CheckPos.position, Vector2.down, 2 * RayDist, Hitable);

			if (HitHori.collider != null || HitVerti.collider == null)
			{
				if (FacingRight)
				{
					FacingRight = false;
					transform.rotation = Quaternion.Euler(0, 180, 0);
				}
				else
				{
					FacingRight = true;
					transform.rotation = Quaternion.Euler(0, 0, 0);
				}
			}

			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawRay(WallCheckPos.position, Vector2.right * RayDist);
		Gizmos.DrawRay(CheckPos.position, Vector2.down * (2*RayDist));
	}
}
