using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMaker : MonoBehaviour
{
	RaycastHit2D Hit;
	public LayerMask Hitable;
	[HideInInspector] public bool IsPaused;
	Camera Cam;
	public GameObject OpenPortal;
	public GameObject ClosePortal;
	GameObject OpenPortalGO;
	GameObject ClosePortalGO;
	bool CanSpawn;
	Vector2 StartPos;
	Vector2 Endpos;
	public Transform StartPosIndicator;
	public Transform EndPosIndicator;
	// Start is called before the first frame update
	void Start()
    {
		CanSpawn = true;
		StartPosIndicator.gameObject.SetActive(false);
		EndPosIndicator.gameObject.SetActive(false);
		Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		if (CanSpawn && !IsPaused)
		{
			if (Input.GetMouseButtonDown(0))
			{
				StartPos = Cam.ScreenToWorldPoint(Input.mousePosition);
				StartPosIndicator.gameObject.SetActive(true);
				StartPosIndicator.position = StartPos;
			}
			if (Input.GetMouseButton(0))
			{
				Endpos = Cam.ScreenToWorldPoint(Input.mousePosition);
				EndPosIndicator.gameObject.SetActive(true);
				EndPosIndicator.position = Endpos;
			}
			if (Input.GetMouseButtonUp(0))
			{
				StartPosIndicator.gameObject.SetActive(false);
				EndPosIndicator.gameObject.SetActive(false);
				Vector2 Midpoint = (Endpos + StartPos) / 2;
				Collider2D col = Physics2D.OverlapCircle(Midpoint, 0.5f, Hitable);
				if(col == null)
				{
					Vector2 Difference = Endpos - StartPos;
					float Rotz = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
					if (!OpenPortalGO)
					{
						OpenPortalGO = Instantiate(OpenPortal, Midpoint, Quaternion.identity);
						OpenPortalGO.transform.rotation = Quaternion.Euler(0, 0, Rotz - 90);
					}
					else
					{
						Vector2 Dir = Midpoint - (Vector2)transform.position;
					//	Debug.DrawRay(transform.position, Dir, )
						if(Dir.magnitude <= 20f)
						{
							ClosePortalGO = Instantiate(ClosePortal, Midpoint, Quaternion.identity);
							OpenPortalGO.GetComponent<Portal>().ExitPortalGO = ClosePortalGO;
							ClosePortalGO.transform.rotation = Quaternion.Euler(0, 0, Rotz - 90);
							CanSpawn = false;
						}
					}
				}
			}
		}
		if(ClosePortalGO == null)
		{
			CanSpawn = true;
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.DrawRay(transform.position, transform.right * 20f);
	}
}
