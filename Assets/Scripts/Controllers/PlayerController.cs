using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float m_torque;

	[SerializeField]
	private Animator m_anim;

	[SerializeField]
	private float m_x;

	[SerializeField]
	private Vector3 m_vel;

	public float m_velMag;



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_x = Input.GetAxis("Horizontal");

		//m_anim.SetFloat("x",m_x);

		turn(m_x);
		//m_anim.SetFloat("y", Mathf.Abs( rigidbody.velocity.z ));

		if(Input.GetKey(KeyCode.UpArrow)) {
			push(30);
			//m_anim.SetTrigger("pump");
		}

		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			decelerate(10);
			//m_anim.SetTrigger("pump");
		}

		m_velMag = GetComponent<Rigidbody>().velocity.magnitude;
		m_vel = GetComponent<Rigidbody>().velocity;

		GetComponent<Rigidbody>().drag = Vector3.Angle(transform.forward,GetComponent<Rigidbody>().velocity.normalized)*0.01f;

	}

	void FixedUpdate()
	{


	}

	public void turn(float p_angle)
	{
		//turn the sucker
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
