using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
	[SerializeField] private float _defaultJumpForce;
	private Rigidbody _rigitBody;
	private bool _isOnGraund = true;
	private float _jumpForce;

	private void Start()
	{
		_rigitBody = GetComponent<Rigidbody>();
		_jumpForce = _defaultJumpForce;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && _isOnGraund)
		{
			_isOnGraund = false;
			_rigitBody.AddForce(Vector3.up * _jumpForce);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.TryGetComponent(out Road road)){
			_isOnGraund = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Booster booster))
		{
			_jumpForce = _defaultJumpForce * booster.impruveCoeficient;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		_jumpForce = _defaultJumpForce;
	}
}

/*
 Todo
a) create jump booster
	+ 1. save to git 
	+ 2. create prefab
	3. create collider
	4. check collision human-booster (on trigger)
	5. create function busted jump
	6. test
	7. add boosters cenerator to level creator
	8. test
	9. save to git

b) create obstacle
	1. save to git
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
