using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] GameObject ObstacleCrushPoint;

	public float GetCrushPointPositionY()
	{
		return ObstacleCrushPoint.transform.position.y;
	}
}
