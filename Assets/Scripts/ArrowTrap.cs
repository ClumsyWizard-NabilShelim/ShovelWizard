using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
	public GameObject PlaceHolderArrow;
	public Transform Checkpos;
	public Transform ShootPos;
	public Vector2 Size;
	public LayerMask Hitable;
	Collider2D Col;
	public bool Repeat;
	public GameObject Arrow;
	bool Shot;
	AudioManager AM;
	public float Delay;
	float currentTime;
    // Start is called before the first frame update
    void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		currentTime = Delay;
	}

    // Update is called once per frame
    void Update()
    {
		if (Repeat)
		{
			Col = Physics2D.OverlapBox(Checkpos.position, Size, 0, Hitable);
			if (currentTime <= 0)
			{
				if (Col != null)
				{
					if(PlaceHolderArrow != null)
						Destroy(PlaceHolderArrow);

					AM.Play("TrapShoot");
					Instantiate(Arrow, ShootPos.position, ShootPos.rotation);
					Shot = true;
					currentTime = Delay;
				}
			}
			else
			{
				currentTime -= Time.deltaTime;
			}
		}
		else
		{
			if (!Shot)
			{
				Col = Physics2D.OverlapBox(Checkpos.position, Size, 0, Hitable);

				if (Col != null)
				{
					if (PlaceHolderArrow != null)
						Destroy(PlaceHolderArrow);

					AM.Play("TrapShoot");
					Instantiate(Arrow, ShootPos.position, ShootPos.rotation);
					Shot = true;
				}
			}
		}
    }

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(Checkpos.position, Size);
	}
}
