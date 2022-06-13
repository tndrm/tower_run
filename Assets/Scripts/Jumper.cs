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
