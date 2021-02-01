using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	Animator Anim;
	public GameObject EnterEffect;
	public Vector2 Size;
	public LayerMask Hitable;
	Collider2D[] Col;
	public bool ExitPortal;
	/*[HideInInspector]*/ public GameObject ExitPortalGO;
	Rigidbody2D RB;

	Projectile RecievedProjectile;
	Bomb RecievedBomb;

	AudioManager AM;
	// Start is called before the first frame update
	void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		AM.Play("Portal");
		Anim = GetComponent<Animator>();
		Anim.SetBool("Portal", true);
    }

    // Update is called once per frame
    void Update()
    {
		if (ExitPortal && Input.GetMouseButtonDown(0))
		{
			if (transform.localEulerAngles.y == 0)
				transform.rotation = Quaternion.Euler(0, 180, transform.localEulerAngles.z * -1);
			else
				transform.rotation = Quaternion.Euler(0, 0, transform.localEulerAngles.z * -1);
		}
		Col = Physics2D.OverlapBoxAll(transform.position, Size, transform.localEulerAngles.z, Hitable);

		if(Col != null)
		{
			foreach (Collider2D C in Col)
			{
				if (C.tag != "Environment")
				{
					if (!ExitPortal)
					{
						if (ExitPortalGO != null)
						{
							AM.Play("PortalEnter");
							GameObject EF = Instantiate(EnterEffect, transform.position, EnterEffect.transform.rotation);
							Destroy(EF, 2f);

							if (C.tag == "Projectile")
							{
								if(RecievedProjectile = C.GetComponent<Projectile>())
								{
									RecievedProjectile.Follow = false;
									RecievedProjectile.ReleaseFromObject = null;
								}else if (RecievedBomb = C.GetComponent<Bomb>())
								{
									RecievedBomb.ReleaseFromObject = null;
								}
							}
							C.gameObject.transform.position = ExitPortalGO.transform.position;
							Destroy(gameObject);
						}
					}

					if (ExitPortal)
					{
						AM.Play("PortalEnter");
						GameObject EF = Instantiate(EnterEffect, transform.position, EnterEffect.transform.rotation);
						Destroy(EF, 2f);

						if (RB = C.GetComponent<Rigidbody2D>())
						{
							RB.velocity = Vector2.zero;
							RB.AddForce(transform.right * 25f, ForceMode2D.Impulse);
						}

						if (C.tag != "Enemy" && C.tag != "Player")
							C.transform.rotation = transform.rotation;

						Destroy(gameObject);
					}
				}
			}
		}


		if (Input.GetKeyDown(KeyCode.Space))
		{
			AM.Play("Portal");
			Anim.SetBool("Portal", false);
		}

	}
	public void RemovePortal()
	{
		Destroy(gameObject);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, Size);
	}

}
