using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public bool IsSpike;
	[HideInInspector] public GameObject ReleaseFromObject;
	public GameObject Effect;
	Transform Player;
	public bool Follow;
	public float Speed;
	public Rigidbody2D RB;
	AudioManager AM;
	public string SoundEffect;
    // Start is called before the first frame update
    void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		if (Follow)
		{
			Player = GameObject.FindGameObjectWithTag("Player").transform;
			StartCoroutine(DestroyRocket());
		}
    }

	// Update is called once per frame
	void Update()
	{
		if (!Follow)
		{
			RB.velocity = transform.right * Speed * Time.deltaTime;
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, 9 * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D Col)
	{
		if (!IsSpike)
		{
			if (Col.tag != "Environment")
			{
				if (ReleaseFromObject != null)
				{
					if (Col.gameObject != ReleaseFromObject)
					{
						KillObject(Col.gameObject);
					}
				}
				else
				{
					KillObject(Col.gameObject);
				}
			}
		}
		else
		{
			if (ReleaseFromObject != null)
			{
				if (Col.gameObject != ReleaseFromObject)
				{
					KillObject(Col.gameObject);
				}
			}
			else
			{
				KillObject(Col.gameObject);
			}
		}
	}

	void KillObject(GameObject HitObject)
	{
		if (HitObject.GetComponent<Stats>() != null)
		{
			HitObject.GetComponent<Stats>().Kill();
		}
		if(HitObject.tag == "Boss")
		{
			HitObject.GetComponent<Boss>().DamageBoss(100);
		}
		if (HitObject.tag == "Portal")
		{
			if (HitObject.GetComponent<Portal>() != null)
			{
				if (HitObject.GetComponent<Portal>().ExitPortalGO == null)
				{
					return;
				}
			}
		}
		Hit();

	}

	IEnumerator DestroyRocket()
	{
		yield return new WaitForSeconds(3f);
		Hit();
	}

	void Hit()
	{
		AM.Play(SoundEffect);
		GameObject EF = Instantiate(Effect, transform.position, Quaternion.identity);
		Destroy(EF, 2f);
		Destroy(gameObject);
	}
}
