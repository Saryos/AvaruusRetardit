using UnityEngine;
using System.Collections;

public class MySimpleController{
	
	public GameObject cre;

	public CharacterController controller; // either CC or RB depending on prefab
	public Rigidbody rbody; 
	
	public MySimpleController(GameObject temp){
		cre = temp;
		controller = cre.GetComponent<CharacterController>(); 
		rbody = cre.GetComponent<Rigidbody>();
	}
	
	public Vector3 Velocity(){
		if(controller){
			return controller.velocity;
		} else {
			return rbody.velocity;
		}
	}
	
	public void Move(Vector3 speed){
		if(controller){
			controller.Move(speed*Time.deltaTime); // Move
		} else {
			Vector3 dest = rbody.position + speed*Time.deltaTime;
			dest.y = Terrain.activeTerrain.SampleHeight(dest);
			rbody.MovePosition(dest);
			//rbody.MovePosition(rbody.position + moveForce*Time.deltaTime);
		}
	}
	
}
