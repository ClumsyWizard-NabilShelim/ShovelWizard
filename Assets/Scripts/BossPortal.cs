using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
	AudioManager AM;
	Boss B;
	public GameObject Effect;
	[HideInInspector] public GameObject ObjectToSpawn;
	[HideInInspector] public Transform EnemyParent;
	///*[HideInInspector] */public float delay;
	public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
		B = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		AM.Play("Portal");
		//currentTime = delay;
	}

    // Update is called once per frame
    void Update()
    {
		if (B.CurrentState == BossState.FinalStage)
		{
			currentTime = 0.6f;
		}

		if (currentTime <= 0)
		{
			AM.Play("PortalEnter");
			GameObject Ef = Instantiate(Effect, transform.position, Quaternion.identity);
			Destroy(Ef, 2f);
			if(ObjectToSpawn.tag != "Enemy")
			{
				Instantiate(ObjectToSpawn, transform.position, transform.rotation);
			}
			else
			{
				GameObject e = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
				if(EnemyParent != null)
				{
					e.transform.parent = EnemyParent;
				}
			}
			Destroy(gameObject);
			currentTime = 1;
		}
		else
		{
			currentTime -= Time.deltaTime;
		}
    }
}
