using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	[SerializeField] private Human[] _humansTemplates;
	[SerializeField] private Vector2Int _humansInTowerRange;

	private List<Human> _humansInTower;

	private void Start()
	{
		_humansInTower = new List<Human>();
		int humansInTowerQuantity = Random.Range(_humansInTowerRange.x, _humansInTowerRange.y);
		SpawnHumans(humansInTowerQuantity);

	}

	private void SpawnHumans(int quantityInTower)
	{
		Vector3 spawnPoint = transform.position;
		for (int i = 0; i < quantityInTower; i++)
		{
			Human spawnHuman = _humansTemplates[Random.Range(0, _humansTemplates.Length)];
			_humansInTower.Add(Instantiate(spawnHuman, spawnPoint, Quaternion.Normalize(transform.rotation), transform));
			spawnPoint = _humansInTower[i].fixationPoint.position;
		}
	}

	public List<Human> HumanCollect(Transform footFixationPoint, float fixationMaxDistance)
	{
		for (int i = 0; i < _humansInTower.Count; i++)
		{
			if (CheckDistanceY(_humansInTower[i].fixationPoint.transform, footFixationPoint) < fixationMaxDistance)
			{
				List<Human> collectedHumans = _humansInTower.GetRange(0, i + 1);
				_humansInTower.RemoveRange(0, i + 1);
				return collectedHumans;
			}
		}
		return null;
	}

	private float CheckDistanceY(Transform humanFixationPoint, Transform footFixationPoint)
	{
		Vector3 footFixationPointY = new Vector3(0, footFixationPoint.position.y, 0);
		Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
		return Vector3.Distance(humanFixationPointY, footFixationPointY);
	}

	public void Break()
	{
		foreach (var human in _humansInTower)
		{
			human.FallDown();
		}
		Invoke("DestroyTower", 1f);
	}

	private void DestroyTower()
	{
		Destroy(gameObject);
	}

}
