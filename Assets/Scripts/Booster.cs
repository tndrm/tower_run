using UnityEngine;


public class Booster : MonoBehaviour
{
	[SerializeField] private float _impruveCoeficient;
	[SerializeField] private float _distanceToTower;
	public float impruveCoeficient => _impruveCoeficient;
	public float distanceToTower => _distanceToTower;
}
