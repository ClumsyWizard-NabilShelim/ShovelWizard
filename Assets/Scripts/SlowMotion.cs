using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
	public float SlowDownFactor = 0.05f;
	[HideInInspector] public bool SlowedDown;
	float NormalFixedDeltaTime;

	void Start()
	{
		NormalFixedDeltaTime = Time.fixedDeltaTime;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SlowedDown = !SlowedDown;
		}

		if (SlowedDown)
		{
			SlowDown();
		}
		else
		{
			normalizeTime();
		}

		Time.timeScale = Mathf.Clamp01(Time.timeScale);
	}

	void SlowDown()
	{
		Time.timeScale = SlowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}

	void normalizeTime()
	{
		if (Time.timeScale < 1)
		{
			Time.timeScale += (1f / SlowDownFactor) * Time.deltaTime;
			Time.fixedDeltaTime = NormalFixedDeltaTime;
		}
	}
}
