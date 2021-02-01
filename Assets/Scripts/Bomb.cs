using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
/*	[HideInInspector] */public GameObject ReleaseFromObject;
	public LayerMask Hitable;
	public float radius;
	Collider2D Col;
	public GameObject Effect;
	AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
	}

	void OnTriggerEnter2D(Collider2D collision2D)
	{
		if (ReleaseFromObject != null && collision2D.gameObject != ReleaseFromObject)
		{
			Debug.Log(collision2D.gameObject);
			Explode();
		}
		else if (ReleaseFromObject == null)
		{
			Explode();
		}
	}

	void Explode()
	{
		AM.Play("Explosion");
		GameObject EF = Instantiate(Effect, transform.position, Quaternion.identity);
		Destroy(EF, 2f);

		Col = Physics2D.OverlapCircle(transform.position, radius, Hitable);

		if (Col != null)
		{
			if (Col.GetComponent<Stats>() != null)
			{
				Col.GetComponent<Stats>().Kill();
			}
			else
			{
				Destroy(Col.gameObject);
			}
		}

		Destroy(gameObject);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
