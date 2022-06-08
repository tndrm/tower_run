using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
	[SerializeField] private float _jumpForse;
	private Rigidbody _rigitBody;
	private bool _isOnGraund = true;

	private void Start()
	{
		_rigitBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && _isOnGraund)
		{
			_isOnGraund = false;
			_rigitBody.AddForce(Vector3.up * _jumpForse);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.TryGetComponent(out Road road)){
			_isOnGraund = true;
		}
	}
}
