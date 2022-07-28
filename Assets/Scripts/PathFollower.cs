using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Rigidbody))]

public class PathFollower : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private PathCreator _pathCreator;
	
    private Rigidbody _rigidbody;
    private float _distanceTraveled;
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.MovePosition(_pathCreator.path.GetPointAtDistance(_distanceTraveled));

		transform.Rotate(0, 0, -90);
	}

	private void Update()
	{
		_distanceTraveled += Time.deltaTime * _speed;
		Vector3 nextPoint = _pathCreator.path.GetPointAtDistance(_distanceTraveled, EndOfPathInstruction.Loop);
		nextPoint.y = _rigidbody.position.y;

		_rigidbody.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTraveled);
		_rigidbody.MovePosition(nextPoint);
	}
}
