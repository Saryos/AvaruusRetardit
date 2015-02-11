using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public Rigidbody rbody;
	public Vector3 velocity;
	public GameObject station;
	public Rigidbody rstation;
	public bool docking = false;
	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
		rstation = station.GetComponent<Rigidbody>();
	}
	
// Update is called once per frame
	void Update () {
		Vector3 dest = rbody.position + velocity*Time.deltaTime;
		rbody.MovePosition(dest);
		
		if(Input.GetKey("w")){
			//if(velocity.magnitude < 20f){
				velocity = velocity + transform.forward;
			//}
		}
		if(Input.GetKey("s")){
			//if(velocity.magnitude > -20f){
				velocity = velocity - transform.forward;
			//}
		}
		if(Input.GetKey("a")){
			transform.Rotate(Vector3.up * 10f* Time.deltaTime, Space.World);
		}
		if(Input.GetKey("d")){
			transform.Rotate(Vector3.up * 10f * -Time.deltaTime, Space.World);
		}
		
		Vector3 kotiin = rbody.position - rstation.position;
		
		if(Input.GetKey("p")){
			if (kotiin.magnitude < 200){
				docking = true;
				AudioClip clip = Resources.Load("ED_Dock_clipped") as AudioClip;
				audio.clip = clip;
				audio.Play();
			}
			else {
				AudioClip clip = Resources.Load("Dock_range") as AudioClip;
				audio.clip = clip;
				audio.Play();
			}
		}
		
		if (docking && !audio.isPlaying){
			AudioClip clip = Resources.Load("BD_clipped") as AudioClip;
			audio.clip = clip;
			audio.Play();
		}
		
		
		
	}
}
