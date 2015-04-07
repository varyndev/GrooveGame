using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float m_torque;

	[SerializeField]
	private Animator m_anim;

	[SerializeField]
	private float m_x;

	public float m_velMag;


	void Start () 
	{
	}
	
	void Update () 
	{
		m_x = Input.GetAxis("Horizontal");
		turn(m_x);
		if(Input.GetKey(KeyCode.UpArrow)) {
			push(30);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			decelerate(10);
		}
		m_velMag = GetComponent<Rigidbody>().velocity.magnitude;
		GetComponent<Rigidbody>().drag = Vector3.Angle(transform.forward,GetComponent<Rigidbody>().velocity.normalized)*0.01f;
	}

	void FixedUpdate()
	{
	}

	public void turn(float p_angle)
	{
		GetComponent<Rigidbody>().AddTorque(Vector3.up * p_angle,ForceMode.Impulse);
	}

	public void push(float p_force) 
	{
		GetComponent<Rigidbody>().AddForce(transform.forward * 3,ForceMode.Impulse);
	}

	public void fixAngle()
	{
	}

	public void decelerate(float p_z) 
	{
		GetComponent<Rigidbody>().AddForce(Vector3.forward * p_z,ForceMode.Impulse);
	}
}