using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
	public Transform CheckPos;
	public float CheckRadius;
	public LayerMask Hitable;
	Collider2D Col;
	AudioManager AM;

	void Start()
	{
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			AM.Play("PlayerAttack");
			Col = Physics2D.OverlapCircle(CheckPos.position, CheckRadius, Hitable);
			if(Col != null)
			{
				if (Col.GetComponent<Stats>() != null)
				{
					Col.GetComponent<Stats>().Kill();
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(CheckPos.position, CheckRadius);
	}
}
