using System.Collections.Generic;
using UnityEngine;

public class ControlledTower : MonoBehaviour
{
	[SerializeField] private Transform _footFixationPoint;
	[SerializeField] private float _fixationMaxDistance;
	[SerializeField] private Human _startHuman;
	[SerializeField] private BoxCollider _checkCollaider;

	private List<Human> _humans;

	private void Start()
	{
		_humans = new List<Human>();
		Vector3 spawnPoint = transform.position;
		_humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out Human human))
		{
			HandleCollisionWithTower(human);
		}
		if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
		{
			HandleCollisionWithObstacle(obstacle);
		}
	}

	private void HandleCollisionWithObstacle(Obstacle obstacle)
	{

	}

	private void HandleCollisionWithTower(Human human)
	{
		Tower collisionTower = human.GetComponentInParent<Tower>();
		if (collisionTower)
		{
			List<Human> collectedHumans = collisionTower.HumanCollect(_footFixationPoint, _fixationMaxDistance);
			if (collectedHumans != null)
			{
				for (int i = collectedHumans.Count - 1; i >= 0; i--)
				{
					InsertHuman(collectedHumans[i]);
				}
				MoveDownFootFixationPoint();
				MoveDownTowerCheckCollaider();
			}
			collisionTower.Break();
		}
	}

	private void InsertHuman(Human insertedHuman)
	{
		_humans.Insert(0, insertedHuman);
		ResetHumanPosition(insertedHuman);
	}

	private void ResetHumanPosition(Human human)
	{
		human.transform.parent = transform;
		human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
		human.transform.localRotation = Quaternion.identity;
	}
	private void MoveDownFootFixationPoint()
	{
		Vector3 footFixationPointNewPosition = _footFixationPoint.position;
		Vector3 TowerMinPointPosition = _humans[0].GetComponent<Collider>().bounds.min;
		footFixationPointNewPosition.y = TowerMinPointPosition.y;
		_footFixationPoint.position = footFixationPointNewPosition;
	}

	public void MoveDownTowerCheckCollaider()
	{
		_checkCollaider.center = _footFixationPoint.localPosition;
	}
}

/*
 Todo

b) create obstacle
	2. create prefabe
	3. add collider
	4. check collision
	5. function removing humans from tower
	6. test
	7. add obstacles generator to level creator
	8. test
	9. save to git
 
c) fix:
	1. human positions in played tower
	2. change humans parent after destrow towers to prevent double collision
 */
