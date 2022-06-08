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

			Tower collisionTower = human.GetComponentInParent<Tower>();
			if (collisionTower)
			{
				List<Human> collectedHumans = collisionTower.HumanCollect(_footFixationPoint, _fixationMaxDistance);
				if (collectedHumans != null)
				{
					Debug.Log(collectedHumans.Count);
					for (int i = collectedHumans.Count - 1; i >= 0; i--)
					{
						InsertHuman(collectedHumans[i]);
						MoveDownFootFixationPoint(collectedHumans[i]);
					}
					MoveDownTowerCheckCollaider();
				}
				collisionTower.Break(collision.contacts[0].point);
			}

		}
	}

	private void InsertHuman(Human insertedHuman)
	{
		_humans.Insert(0, insertedHuman);
		ResetHumanPosirion(insertedHuman);
	}

	private void ResetHumanPosirion(Human human)
	{
		human.transform.parent = transform;
		human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
		human.transform.localRotation = Quaternion.identity;
	}
	private void MoveDownFootFixationPoint(Human human)
	{
		Vector3 footFixationPointNewPosition = _footFixationPoint.position;
		footFixationPointNewPosition.y -= human.fixationPoint.transform.position.y;
		_footFixationPoint.position = footFixationPointNewPosition;
	}

	public void MoveDownTowerCheckCollaider()
	{
		_checkCollaider.center = _footFixationPoint.localPosition;
	}
}
