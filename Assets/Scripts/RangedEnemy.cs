using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
	public bool BombEnemy;
	public Animator Anim;
	[Header("Check For Player")]
	GameObject Player;
	public LayerMask Hitable;
	public Patrol patrol;

	[Header("shooting")]
	public GameObject Bullet;
	public Transform Weapon;
	public Transform[] ShootPos;
	public float ShootDelay;
	float CurrentTime;
	public SpriteRenderer WeaponRend;
	Vector2 Dir;
	Vector2 PlayerDir;
	public bool CanShoot;
	AudioManager AM;
	public string SoundEffect;
	// Start is called before the first frame update
	void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
		Player = GameObject.FindGameObjectWithTag("Player");
		CurrentTime = ShootDelay;
    }

    // Update is called once per frame
    void Update()
    {
		if (Player != null)
		{
			if (!CanShoot)
			{
				if(patrol.CanMove == false)
				{
					Anim.SetTrigger("Idel");
					patrol.CanMove = true;
				}
			}

			PlayerDir = Player.transform.position - transform.position;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, PlayerDir.normalized, 1000, Hitable);

			if (hit == true && hit.collider.tag == "Player")
			{
				CanShoot = true;
			}
			else
			{
				CanShoot = false;
				//Weapon.rotation = Quaternion.Euler(0, 0, 0);
				return;
			}

			if (CanShoot)
			{
				Dir = new Vector2(Player.transform.position.x - transform.position.x, 0);

				if (Vector2.Dot(Dir.normalized, transform.right) > 0.9f)
				{
					patrol.CanMove = false;
					float rotZ = Mathf.Atan2(PlayerDir.y, PlayerDir.x) * Mathf.Rad2Deg;
					Weapon.rotation = Quaternion.Euler(0, 0, rotZ);


					if (Player.transform.position.x < transform.position.x)
					{
						transform.rotation = Quaternion.Euler(0, 180, 0);
						if (WeaponRend != null)
							WeaponRend.flipY = true;
					}
					else
					{
						transform.rotation = Quaternion.Euler(0, 0, 0);
						if (WeaponRend != null)
							WeaponRend.flipY = false;
					}

					if (CurrentTime <= 0)
					{
						Anim.SetTrigger("Attack");
						CurrentTime = ShootDelay;
					}
					else
					{
						CurrentTime -= Time.deltaTime;
					}
				}
			}
		}
		else
		{
			Anim.SetTrigger("Idel");
			patrol.CanMove = true;
		}


	}

	public void Shoot()
	{
		AM.Play(SoundEffect);
		if (!BombEnemy)
		{
			foreach (Transform T in ShootPos)
			{
				Projectile P = Instantiate(Bullet, T.position, T.rotation).GetComponent<Projectile>();
				P.ReleaseFromObject = gameObject;
			}
		}
		else
		{
			Rigidbody2D RB = Instantiate(Bullet, ShootPos[0].position, Quaternion.identity).GetComponent<Rigidbody2D>();
			RB.GetComponent<Bomb>().ReleaseFromObject = gameObject;
			RB.velocity = CalculateTrajectory(RB);
		}
	}

	Vector2 CalculateTrajectory(Rigidbody2D RB)
	{
		float X = PlayerDir.x; // Horizontal Displacement ( distance between player transform and this enemy's transform in x-axis)
		float Y = PlayerDir.y; // Horizontal Displacement ( distance between player transform and this enemy's transform in y-axis)

		float Angle = Mathf.Atan((Y + (RB.gravityScale * 4.905f)) / X);// the angle at which the object will be thrown 

		float TotalVelocity = X / Mathf.Cos(Angle); // the total velocity that has to be give to the object

		float HoriVelocity = TotalVelocity * Mathf.Cos(Angle); // velocity in horizontal direction;
		float VertiVelocityY = TotalVelocity * Mathf.Sin(Angle); // velocity in vertical direction;

		return new Vector2(HoriVelocity, VertiVelocityY); // returning the velocity as a vector2.
	}

	//void OnDrawGizmos()
	//{
	//	Gizmos.DrawRay(transform.position, transform.right * Dist);
	//}
}
