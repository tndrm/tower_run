using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
	[SerializeField] private PathCreator _pathCreator;
	[SerializeField] private Tower _towerTemplate;
	[SerializeField] private int _towersCount;
	[SerializeField] private List<Booster> _boosterTemplates;
	

	private float _roadLenth;


	private void Start()
	{		
		_roadLenth = _pathCreator.path.length;

		GenerateLevel();
	}

	private void GenerateLevel()
	{
		float maxDistanseBetweenTowers = _roadLenth / _towersCount;

		float distanceFromStart = 0;
		Vector3 towerSpawnPoint;

		for (int i = 0; i < _towersCount; i++)
		{
			Booster booster = _boosterTemplates[Random.Range(0, _boosterTemplates.Count)];
			distanceFromStart += maxDistanseBetweenTowers;
			towerSpawnPoint = _pathCreator.path.GetPointAtDistance(distanceFromStart, EndOfPathInstruction.Stop);
			Instantiate(_towerTemplate, towerSpawnPoint, Quaternion.identity);
			Vector3 boosterSpawnPoint = _pathCreator.path.GetPointAtDistance(distanceFromStart - booster.distanceToTower);
			Instantiate(booster, boosterSpawnPoint, Quaternion.identity);
		}
	}
}
