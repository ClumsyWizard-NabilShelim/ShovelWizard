using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	Camera Cam;
	public SpriteRenderer PlayerSprite;
	public SpriteRenderer WeaponSprite;
	[HideInInspector] public bool CanMove;
    // Start is called before the first frame update
    void Start()
    {
		CanMove = true;
		Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		if (CanMove)
		{
			Vector2 Dif = Cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float Rotz = Mathf.Atan2(Dif.y, Dif.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Rotz), 0.07f);

			Vector2 PlayerPos = Cam.WorldToScreenPoint(transform.parent.position);

			if ((Input.mousePosition.x - PlayerPos.x) < 0)
			{
				PlayerSprite.flipX = true;
				//WeaponSprite.flipY = true;
			}
			else
			{
				PlayerSprite.flipX = false;
				//WeaponSprite.flipY = false;
			}
		}
    }
}
