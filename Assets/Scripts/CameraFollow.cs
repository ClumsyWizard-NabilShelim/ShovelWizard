using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform Player;
	public float Minx;
	public float MaxX;
	public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 PlayerPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);
		Vector3 LerpedPos = Vector3.Lerp(transform.position, PlayerPos, MoveSpeed);

		transform.position = new Vector3(Mathf.Clamp(LerpedPos.x, Minx, MaxX), transform.position.y, transform.position.z);

		//if(transform.position.x <= MaxX && transform.position.x >= Minx)
		//	transform.position = new Vector3(LerpedPos.x, 0, transform.position.z);
		//if(transform.position.y <= MaxY && transform.position.y >= Minx)
		//	transform.position = new Vector3(0, LerpedPos.y, transform.position.z);
	}
}
