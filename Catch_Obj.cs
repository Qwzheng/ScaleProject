using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch_Obj : MonoBehaviour {

	public bool this_be_click = false;
	public bool control_value = false;

	public float dist;

	public GameObject Player;
	public Rigidbody My_rigi;
	public Material Mat_be_click;
	public Material Ori_Mat;

	private Vector3 startScale;
	private float startDist;
	private float lastDist;


	// Use this for initialization
	void Start () 
	{
		My_rigi = GetComponent<Rigidbody>();
		Ori_Mat = GetComponent<Renderer> ().material;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		Control_Scale ();

    }
	void Control_Scale ()
	{
		dist = Vector3.Distance (transform.position, Camera.main.transform.position);

		RaycastHit hitPoint;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hitPoint, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Default"))) {
			if (hitPoint.collider.tag == "Untagged") {
				if (this_be_click == true&&control_value==false) {
					this.transform.position = hitPoint.point;
				}
			}
		}
		//		//this obj is not be control

		if (this_be_click == false || control_value == true) {
			gameObject.tag = "Untagged";
			gameObject.layer = 0;
			My_rigi.isKinematic = false;
			My_rigi.mass = Mathf.Infinity;
			GetComponent<Renderer> ().material = Ori_Mat;
			My_rigi.useGravity = true;
			startScale = transform.localScale;
			startDist = Vector3.Distance (transform.position, Camera.main.transform.position);
			lastDist = startDist;
			this.transform.parent = null;
		}

		//this obj is be control
		else if (this_be_click == true ) {
			if (dist != lastDist) {
				transform.localScale = startScale / startDist * dist;
				lastDist = dist;
			}
			if (Input.GetMouseButtonDown (0)) {
				if (control_value == true) {
					control_value = false;
				} else if (control_value==false) {
					control_value = true;
				}
			}

			gameObject.tag = "Player";
			gameObject.layer = 4;
			My_rigi.isKinematic = false;
			My_rigi.mass =1;
			GetComponent<Renderer> ().material = Mat_be_click;
			My_rigi.useGravity = false;
			this.transform.parent = Player.transform;
		}
	}

	void OnMouseDown ()
	{
		if (this_be_click == false) {
			if (Player.transform.childCount == 1) {
				this_be_click = true;
				control_value = false;
			}
		} 
		else if(this_be_click == true){
				this_be_click = false;
		}
	}

}
