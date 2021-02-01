using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	AudioManager AM;
	public Rigidbody2D RB;
	public Animator Anim;
	[Header("Move Variables")]
	public float MoveSpeed;
	float HorizontalInput;

	[Header("Jump Variables")]
	bool OnGround = true;
	public float JumpSpeed;
	public Transform Checkpos;
	public Vector2 CheckSize;
	public LayerMask CheckLayer;
	Collider2D Col;
	public GameObject LandEffect;

    // Start is called before the first frame update
    void Start()
    {
		AM = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
		HorizontalInput = Input.GetAxisRaw("Horizontal");
		Col = Physics2D.OverlapBox(Checkpos.position, CheckSize, 0, CheckLayer);

		if (Col != null)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				AM.Play("PlayerJump");
				RB.velocity = new Vector2(RB.velocity.x, JumpSpeed);
			}

			if (OnGround == false)
			{
			    AM.Play("PlayerLand");
				Anim.SetTrigger("Land");
				GameObject LEF = Instantiate(LandEffect, Checkpos.position, LandEffect.transform.rotation);
				Destroy(LEF, 2f);
				OnGround = true;
			}
		}
		else
		{
			OnGround = false;
		}
	}

	void FixedUpdate()
	{
		if (Col != null)
		{
			RB.velocity = new Vector2(HorizontalInput * MoveSpeed, RB.velocity.y);
		}
		else
		{
			RB.velocity = Vector2.Lerp(RB.velocity, new Vector2(HorizontalInput * MoveSpeed, RB.velocity.y), 1 * Time.deltaTime);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(Checkpos.position, CheckSize);
	}
}
