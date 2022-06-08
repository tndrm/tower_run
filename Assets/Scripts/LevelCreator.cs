using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
	[SerializeField] private PathCreator _pathCreator;
	[SerializeField] private Tower _towerTemplate;
	[SerializeField] private int _towersCount;

	private void Start()
	{
		GenerateLevel();
	}

	private void GenerateLevel()
	{
		float roadLenth = _pathCreator.path.length;
		float maxDistanseBetweenTowers = roadLenth / _towersCount;

		float distanceFromStart = 0;
		Vector3 spawnPoint;

		for (int i = 0; i < _towersCount; i++)
		{
			distanceFromStart += maxDistanseBetweenTowers;
			spawnPoint = _pathCreator.path.GetPointAtDistance(distanceFromStart, EndOfPathInstruction.Stop);
			Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
		}
	}
}
