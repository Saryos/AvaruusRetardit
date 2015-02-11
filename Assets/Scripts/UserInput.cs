using UnityEngine;
using System.Collections;
//using Karjanajo;
 
public class UserInput : MonoBehaviour {
  /*
  public Controllables hahmot;
  private MinimapCamera minimap;
  public GameObject MinimapCamera_prefab;
  public GameObject aitausNuoli_prefab;
 // private Valinta valittu;
 // public GameObject Valittu_prefab;
 */
  float scrollSpeed = 5;
  float distance = 0;
  float fprotation = 0;
  bool firstPerson = false;
  bool started = false;
  /*
  private SceneMasterScript sceneMaster;
*/
    // Use this for initialization
  void Start () {/*
		sceneMaster = GameObject.Find("SceneMaster").GetComponent<SceneMasterScript>();
    minimap=((GameObject)Instantiate(MinimapCamera_prefab, transform.position, MinimapCamera_prefab.transform.rotation)).GetComponent<MinimapCamera>();
    ((GameObject)Instantiate(aitausNuoli_prefab, transform.position, aitausNuoli_prefab.transform.rotation)).GetComponent<OsoitusNuoli>();
    //valittu = ((GameObject)Instantiate(Valittu_prefab, transform.position, Valittu_prefab.transform.rotation)).GetComponent<Valinta>();
    //minimap.addArrows( hahmot );
*/
    }
 
    // Update is called once per frame
  void Update () {
  /*
    if( !started ){
      Vector3 lol = hahmot.GiveLocation();
      lol.y += 50;
      lol.z += 100;
      Camera.main.transform.position = lol;
      started = true;
      //valittu.addTarget( hahmot.giveHorse() );
      hahmot.giveHorse().targetChange( true );
    }
    */
    ControlCharacter();
    MoveCamera();
    RotateCamera();
  }


  private void ControlCharacter(){
    /*
    minimap.changeTarget( hahmot.GiveLocation() );
  
    if(Input.GetMouseButtonDown(0)){
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      Physics.Raycast(ray, out hit);
      hahmot.Move(hit);
    }
    
    if(Input.GetKey("w")){
      hahmot.Forward();
    }
    if(Inpt.GetKey("a")){
      hahmot.Left();
    }
    */
    /*
    if(Input.GetKey("d")){
			AudioClip clip = Resources.Load("ED_Dock_clipped") as AudioClip;
			audio.clip = clip;
			audio.Play();
    }
    
	if (!audio.isPlaying){
		AudioClip clip = Resources.Load("BD_clipped") as AudioClip;
		audio.clip = clip;
		audio.Play();
	}
	*/
    /*
    if(Input.GetKey("space")){
      Camera.main.transform.position = hahmot.GiveLocation() + (Camera.main.transform.forward)*-40;
    }
    if(Input.GetKeyDown("e")){
      if (firstPerson){
        firstPerson=false;
      } else {
        firstPerson = true;
        distance = 0;
        fprotation = 0;
      }
    }
    if(Input.GetKeyDown("tab")){
      hahmot.ChooseNext();
      minimap.changeTarget( hahmot.GiveLocation() );
      //valittu.addTarget( hahmot.giveHorse() );
    }
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			hahmot.Select(1);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			hahmot.Select(2);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			hahmot.Select(3);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){
			hahmot.Select(4);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha5)){
			hahmot.Select(5);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha6)){
			hahmot.Select(6);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha7)){
			hahmot.Select(7);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
		if(Input.GetKeyDown(KeyCode.Alpha8)){
			hahmot.Select(8);
			minimap.changeTarget( hahmot.GiveLocation() );
		}
    
    if(Input.GetKeyDown("t")){
      sceneMaster.ToggleTestGui();
    }
    if(Input.GetKeyDown("p")){
      sceneMaster.ToggleFPS();
    }
		if(Input.GetKeyDown("m")){
			sceneMaster.ToggleMusic();
		}
		if(Input.GetKeyDown("r")){
			sceneMaster.ToggleRain();
		}
		
    if(Input.GetMouseButtonDown(0)){
      print(Input.mousePosition);
    }
*/
    }
   
  private void MoveCamera(){
  /*
    if(firstPerson){
      Vector3 pos = hahmot.GiveLocation();
      pos.y+=2.3F;
      Camera.main.transform.position = pos+distance*Camera.main.transform.forward;
    }
    */
    float wheel = Input.GetAxis("Mouse ScrollWheel");
    //Zoom on mousewheel
    //if (firstPerson){
      distance += wheel*3;
    /*} else {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      Physics.Raycast(ray, out hit);
   //   if( Camera.main.transform.position.y + (hit.point.y-Camera.main.transform.position.y)*wheel > 200 ){
   //   } else if( Camera.main.transform.position.y + (hit.point.y-Camera.main.transform.position.y)*wheel < 100 ){
   //   } else {
   */
        //Camera.main.transform.position += (hit.point-Camera.main.transform.position)*wheel;
		Camera.main.transform.position += Camera.main.transform.forward*wheel*50;
    //  }
    //}
    
    // Movement vectors X and Z
    Vector3 camMoveX = Camera.main.transform.forward;
    camMoveX.y=0;
    camMoveX.Normalize();
    Vector3 camMoveZ = new Vector3(camMoveX.z, 0, -camMoveX.x);
    // scroll map on middlebutton
    if(Input.GetMouseButton(2)){
      float moveX = Input.GetAxisRaw ("Mouse X");
      float moveY = Input.GetAxisRaw ("Mouse Y");
      Camera.main.transform.position += camMoveX*moveY*25;
      Camera.main.transform.position += camMoveZ*moveX*25; 
    }
    
    // Keyboard movement
    if(Input.GetKey("left")) { //(xpos >= 0 && xpos < ResourceManager.ScrollWidth) || 
       Camera.main.transform.position-= camMoveZ*scrollSpeed;
    } else
    if(Input.GetKey("right")) { //(xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth) || 
      Camera.main.transform.position += camMoveZ*scrollSpeed;
    }
    
    //vertical camera movement
    if(Input.GetKey("down")) {//(ypos >= 0 && ypos < ResourceManager.ScrollWidth) || 
      Camera.main.transform.position -= camMoveX*scrollSpeed;
    } else if( Input.GetKey("up")) { //(ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth) ||
      Camera.main.transform.position += camMoveX*scrollSpeed;
    }
    /*
    // Don't go underground
    if (Camera.main.transform.position.y < Terrain.activeTerrain.SampleHeight(Camera.main.transform.position)+1F){
      Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 
        Terrain.activeTerrain.SampleHeight(Camera.main.transform.position)+1F, Camera.main.transform.position.z);
    }
    */
  }
 
  private void RotateCamera() {
    // right mouse button down -> rotate around camera
    /*
    if (firstPerson){
      Vector3 pos = hahmot.GiveLocation();
      Camera.main.transform.rotation = hahmot.GiveRotation();
      Camera.main.transform.RotateAround ( pos, Vector3.up, 90 * fprotation*10F);
    }
    */
    
    if(Input.GetMouseButton(1)){
      float rotationX = Input.GetAxisRaw ("Mouse X");
      float rotationY = Input.GetAxisRaw ("Mouse Y");
      //     Ray ray = Camera.main.ScreenPointToRay( new Vector3((Screen.width/2), (Screen.height/2)) );
      //     RaycastHit hit;
      //      Physics.Raycast(ray, out hit);
      
      //      Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.forward)*-zooming*10;
      // Horizontal
      if (firstPerson){
        fprotation += rotationX*Time.deltaTime;
      } else {
        Camera.main.transform.RotateAround ( Camera.main.transform.position, Vector3.up, 90 * Time.deltaTime*rotationX*10F);
      
        // Vertical
        Vector3 oldForward = Camera.main.transform.forward;
        Quaternion oldCameraDirection=Camera.main.transform.rotation;
        Vector3 CameraLeftAxis= new Vector3(-oldForward.z,0,oldForward.x);
        Camera.main.transform.RotateAround ( Camera.main.transform.position, CameraLeftAxis, 90 * Time.deltaTime*rotationY*10F);
        // We have a problem if camera points directly up or down, undo
        Vector3 testForward=Camera.main.transform.forward;
        testForward.y = 0;
        if(testForward.magnitude < 0.5){
          Camera.main.transform.rotation = oldCameraDirection;
        }
      }
    }    
  }
}