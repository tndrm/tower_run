using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;
	private Animator _animator;

    public Transform fixationPoint => _fixationPoint;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}
	public float GetBottomBoundsPositionY()
	{
        Vector3 bottomBoundsPosition = GetComponent<Collider>().bounds.min;
        return bottomBoundsPosition.y;
    }
     public void FallDown()

	{
		transform.SetParent(null);
		if (!GetComponent<Rigidbody>())
		{
			Rigidbody humanRigidBody = gameObject.AddComponent<Rigidbody>();
			humanRigidBody.useGravity = true;
			humanRigidBody.AddExplosionForce(1000f, transform.position + Random.insideUnitSphere * 5f, 5f, 3.0f);
		}
		Invoke("DestroyHuman", 1f);
	}

	public void StartRunAnimation()
	{
		_animator.SetBool("is_run_normal", true);
	}
	public void StopRunAnimation()
	{
		_animator.SetBool("is_run_normal", false);
	}

	public void DestroyHuman()
	{
		Destroy(gameObject);
	}

}
