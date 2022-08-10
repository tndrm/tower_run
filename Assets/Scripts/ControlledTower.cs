using System.Collections.Generic;
using UnityEngine;

public class ControlledTower : MonoBehaviour
{
	[SerializeField] private Transform _footFixationPoint;
	[SerializeField] private float _fixationMaxDistance;
	[SerializeField] private Human _startHuman;
	[SerializeField] private BoxCollider _checkCollaider;

	private List<Human> _humans;
	private float lastCollisionTime;

	private void Start()
	{
		_humans = new List<Human>();
		Vector3 spawnPoint = transform.position;
		_humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
		_humans[0].StartRunAnimation();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Human human))// TODO refactor: Controlled tower should colide with tower not a human!
		{
			HandleCollisionWithTower(human);
		}
		if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
		{
			if(Time.fixedTime - lastCollisionTime < 2f) Debug.LogError("double collision!"); //Check double collision
			lastCollisionTime = Time.fixedTime;
			
			HandleCollisionWithObstacle(obstacle);
		}
	}

	private void HandleCollisionWithObstacle(Obstacle obstacle)
	{

		float obstacleCrushPointY = obstacle.GetCrushPointPositionY();
		int lastCrashedHumanIndex = _humans.FindLastIndex(human => obstacleCrushPointY > human.GetBottomBoundsPositionY());
		if (lastCrashedHumanIndex + 1 == _humans.Count)
		{
			_humans[0].StopRunAnimation();
			Debug.Log("You loose"); //TODO feat: Create loosescreen
		}
		else
		{
			for (int i = 0; i <= lastCrashedHumanIndex; i++)
			{//TODO fix: the order in the tower should be the same the list
				RemoveHuman(_humans[i]);
			}
			_humans[0].StartRunAnimation();

		}
		MoveFootFixationPoint();
		MoveTowerCheckCollaider();
	}

	private void HandleCollisionWithTower(Human human)
	{
		Tower collisionTower = human.GetComponentInParent<Tower>();
		if (collisionTower)
		{
			List<Human> collectedHumans = collisionTower.HumanCollect(_footFixationPoint, _fixationMaxDistance);
			if (collectedHumans != null)
			{
				_humans[0].StopRunAnimation();
				for (int i = collectedHumans.Count - 1; i >= 0; i--)
				{
					InsertHuman(collectedHumans[i]);
				}
				MoveFootFixationPoint();
				MoveTowerCheckCollaider();
				_humans[0].StartRunAnimation();
			}
			collisionTower.Break();
		}
	}

	private void InsertHuman(Human insertedHuman)
	{
		_humans.Insert(0, insertedHuman);
		ResetHumanPosition(insertedHuman);
	}

	private void RemoveHuman(Human human)
	{
		human.FallDown();
		_humans.Remove(human);
	}

	private void ResetHumanPosition(Human human)
	{
		human.transform.parent = transform;
		human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
		human.transform.localRotation = Quaternion.identity;
	}
	private void MoveFootFixationPoint()
	{
		Vector3 footFixationPointNewPosition = _footFixationPoint.position;
		Vector3 TowerMinPointPosition = _humans[0].GetComponent<Collider>().bounds.min;
		footFixationPointNewPosition.y = _humans[0].GetBottomBoundsPositionY();
		_footFixationPoint.position = footFixationPointNewPosition;
	}

	public void MoveTowerCheckCollaider()
	{
		_checkCollaider.center = _footFixationPoint.localPosition;
	}
}

/*
 Todo
a) show run animation:
	1. show run animation
	2. after collision with tower move animation to down human
	3. add animations to all homans in tower
	4. fix look rotation people in tower

b) feat:
	1. show jump animation
 
c) fix:
	1. human positions in played tower
	2. change humans parent after destrow towers to prevent double collision ???
 */
