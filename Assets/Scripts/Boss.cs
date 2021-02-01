using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BossState
{
	Stage1,
	Stage2,
	FinalStage
}
public class Boss : MonoBehaviour
{
	public Animator FadeAnim;
	public bool IntroDone;
	Rigidbody2D RB;
	AudioManager AM;
	Animator Anim;
	public Image HealthBar;
	Transform Player;
	[Header("BossHealth")]
	public float Health;
	float HealthValue;
	Vector2 Pos;
	[Header("Boss Movement")]
	public BossState CurrentState;

	public float AttackDelay;
	float Currenttime;
	public GameObject BossPortal;
	BossPortal Portal;

	//[Header("Attack_1")]

	[Header("Attack_1")]
	public Transform ShootPos;
	public GameObject[] PortalInObject;

	[Header("Attack_2")]
	public GameObject[] enemies;
	public Transform[] SpawnPos;
	// Start is called before the first frame update
	void Start()
    {
		RB = GetComponent<Rigidbody2D>();
		Anim = GetComponent<Animator>();
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		Currenttime = AttackDelay;
		Player = GameObject.FindGameObjectWithTag("Player").transform;
		HealthValue = Health;
    }

    // Update is called once per frame
    void Update()
    {
		if (IntroDone)
		{
			if (CurrentState == BossState.Stage2)
			{
				AttackDelay = AttackDelay / 2;
			}

			HealthBar.fillAmount = HealthValue / Health;
			if (Player != null)
			{
				if (Player.position.x < transform.position.x)
				{
					transform.rotation = Quaternion.Euler(0, 180, 0);
				}
				if (Player.position.x > transform.position.x)
				{
					transform.rotation = Quaternion.Euler(0, 0, 0);
				}
				if (HealthValue >= 70)
				{
					CurrentState = BossState.Stage1;
				}
				else if (HealthValue >= 40 && HealthValue < 70)
				{
					CurrentState = BossState.Stage2;
				}
				else if (HealthValue >= 1 && HealthValue < 40)
				{
					CurrentState = BossState.FinalStage;
				}
				else
				{
					KillBoss();
				}


				if (Currenttime <= 0)
				{
					Anim.SetTrigger("Attack");
					int I = Random.Range(0, 2);
					if (I == 0)
					{
						Attack_1();
					}
					else
					{
						Attack_2();
					}
					Currenttime = AttackDelay;
				}
				else
				{
					Currenttime -= Time.deltaTime;
				}
			}
		}
	}

	public void DamageBoss(float Amount)
	{
		HealthValue -= Amount;
		Anim.SetTrigger("Hurt");
	}


	void KillBoss()
	{
		StartCoroutine(EndGame());
	}

	IEnumerator EndGame()
	{
		FadeAnim.SetTrigger("Fade");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("EndScene");

	}

	void Attack_1()
	{
		Portal = Instantiate(BossPortal, Player.transform.position + new Vector3(0, 3f), Quaternion.identity).GetComponent<BossPortal>();
		Vector2 Dif = Player.transform.position - Portal.transform.position;
		float Rotz = Mathf.Atan2(Dif.y, Dif.x) * Mathf.Rad2Deg;
		Portal.transform.rotation = Quaternion.Euler(0, 0, Rotz);
		int RandInt = Random.Range(0, PortalInObject.Length);
		Portal.ObjectToSpawn = PortalInObject[RandInt];
	}

	void Attack_2()
	{
		int I = Random.Range(0, SpawnPos.Length);
		if(SpawnPos[I].childCount == 0)
		{
			int A = Random.Range(0, enemies.Length);
			Portal = Instantiate(BossPortal, SpawnPos[I].position, Quaternion.identity).GetComponent<BossPortal>();
			Portal.ObjectToSpawn = enemies[A];
			Portal.EnemyParent = SpawnPos[I];
		}
		else
		{
			I = Random.Range(0, SpawnPos.Length);
		}
	}
}
