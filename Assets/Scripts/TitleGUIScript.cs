using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class TitleGUIScript : MonoBehaviour {

	struct ScoreInfo {
		//public string gtextb; // caching
		public int cows;
		public int time;
		public string name;
	}
 
    private delegate void GUIMetodi();
    private GUIMetodi nykyinenGUI;
    private int halfScreenWidth = Screen.width/2;
    private int halfScreenHeigth = Screen.height/2;
    private int buttonWidth = 150;
    private int buttonHeight = 60;

    // Krediittijuttuja
    private float creditSpeed = 0.03F;
    private TextReader tr;
    private string path;
    private GUIText guitext;
    private string credits;
    private string highscores = "Todays Highscores:\n\n";
	private List<ScoreInfo>[][] scores;
    
    void Start()
    {
		scores = new List<ScoreInfo>[5][]; // [menu][column on menu]
		// Luodaan näytön halki liu'utettavat tekstit
		path = "GameData/Krediitit.txt";
		guitext = GameObject.Find("Credits").GetComponent<GUIText>() as GUIText;
		tr = new StreamReader(path);
		string temp;
		int count = 0;
		while( (temp = tr.ReadLine() ) != null)
		{
			credits += temp;
			credits += "\n";
			count++;
		}
		tr.Close();
		guitext.enabled = true;
		guitext.transform.position = new Vector3(0.8F, -0.6F, 0F);
		
		// highscore handling
		LoadHighScoreData();
		if(HighScorePasser.available){
			AddScore(HighScorePasser.scene, HighScorePasser.difficulty, HighScorePasser.cows,
			         HighScorePasser.time, HighScorePasser.name);
		} else {
			print("no highscore added");
		}
		HighScorePasser.available=false;
		GenHighScoreText();
		guitext.text = highscores;
		WriteHighScoreData();
		
		nykyinenGUI = Paavalikko;
  
	}
 
  
	void OnGUI () {
		nykyinenGUI();
		guitext.transform.Translate(Vector3.up * Time.deltaTime * creditSpeed);
		if (guitext.transform.position.y > 1.6F){
			nykyinenGUI = Paavalikko;
			guitext.transform.position = new Vector3(0.8F, -0.6F, 0F);
			guitext.text = highscores;
		}
	}
 
  
	private void Paavalikko(){
		// Taustaloota.
		GUI.Box(new Rect(halfScreenWidth-100, halfScreenHeigth-150, 200, 300), "Päävalikko");
		
		// Pelaanappi
		if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth-100 ,buttonWidth, buttonHeight), "Pelaa")) {       
			nykyinenGUI = Tasovalikko;
		}
    
		// Krediittinappi.
		if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth-30, buttonWidth, buttonHeight), "Krediitit")) {
			guitext.transform.position = new Vector3(0.5F, -0.6F, 0F);
			guitext.text = credits;
			nykyinenGUI = KrediittiScroll;
		}
    
		// Poistumisnappi.
		if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth+40, buttonWidth, buttonHeight), "Poistu")) {
			Application.Quit();
		}
	}
 
  
    private void Tasovalikko()
    {
        // Lodju
        GUI.Box(new Rect(halfScreenWidth-100, halfScreenHeigth-150, 200, 370), "Tasot");
    
        // Kenttävalikko
        if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth-130 ,buttonWidth, buttonHeight), "Tutorial")) {
			HighScorePasser.scene=0;
			Application.LoadLevel("Tutorial");
        }

		if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth-60 ,buttonWidth, buttonHeight), "Helppo joen ylitys")) {
			HighScorePasser.scene=1;
						Application.LoadLevel("HelppoYlitys");
		}

        if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth+10 ,buttonWidth, buttonHeight), "Aurinkoinen \n jyrkänteenreunama")) {
			HighScorePasser.scene=2;
			            Application.LoadLevel("JyrkanneLevel");
        }
    
        if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth+80 ,buttonWidth, buttonHeight), "Pimiä laakso")) {
			HighScorePasser.scene=3;
			            Application.LoadLevel("OinenLevel");
        }
    
        // Paluu päävalikkoon.
        if(GUI.Button(new Rect(halfScreenWidth-buttonWidth/2, halfScreenHeigth+150, buttonWidth, buttonHeight), "Takaisin")) {
            nykyinenGUI = Paavalikko;
        }
    }

    private void KrediittiScroll()
    {
    /*
        guitext.enabled = true;
        guitext.transform.Translate(Vector3.up * Time.deltaTime * creditSpeed);
        if (guitext.transform.position.y > 1.6F)
        {
            nykyinenGUI = Paavalikko;
            guitext.transform.position = new Vector3(0.5F, -0.6F, 0F);
        }
*/
    }
    
	public void AddScore(int lind, int diff, int c, int t, string n){
		ScoreInfo uus=new ScoreInfo();
		uus.cows=c;
		uus.time=t;
		uus.name=n;
		if(scores[lind]==null){ // if menu not initialized
			scores[lind]=new List<ScoreInfo>[5];
		}
		if(scores[lind][diff]==null){ // if column not initialized
			scores[lind][diff]=new List<ScoreInfo>();
		}
		scores[lind][diff].Add(uus);
	}
	
	private void LoadHighScoreData(){
		FileStream fs = new FileStream("GameData/highscores.txt", FileMode.Open, FileAccess.Read);
		StreamReader sr = new StreamReader(fs);
		while(sr.Peek() != -1){
			string txt = sr.ReadLine();
			string [] txtbits = txt.Split(' ');
			int lind = int.Parse(txtbits[0]);
			int diff = int.Parse(txtbits[1]);
			int c = int.Parse(txtbits[2]);
			int t = int.Parse(txtbits[3]);
			AddScore(lind, diff, c, t, txtbits[4]);			
		}
		
		print("Scores loaded");
		
		sr.Close();
		fs.Close();
	}
	public void WriteHighScoreData(){
		FileStream fs = new FileStream("GameData/highscores.txt", FileMode.Create, FileAccess.Write); // overwrite old file
		StreamWriter sw = new StreamWriter(fs);
		for(int k=0; k<5 ; k++){
			if(scores[k]!=null){
				print("primpass");
				for(int j=0; j<3; j++){
					if(scores[k][j]!=null){
					print("secpass");
					for(int i=0; i<scores[k][j].Count; i++){
						print("write");
						// do the magic thin
						ScoreInfo gel=scores[k][j][i];
				
						string str = k.ToString() + " " + j.ToString() + " " + gel.cows.ToString() + " "
							+ gel.time.ToString() + " " + gel.name;
						sw.WriteLine(str);
					}
					}
				}
			}
		}
		sw.Close();
		fs.Close();
	}
	public void GenHighScoreText(){
		highscores = "Todays Highscores:\n\n";
		highscores += "Tutorial:\n";
		AddSceneScores(0);
		highscores += "Helppo Ylitys:\n";
		AddSceneScores(1);
		highscores += "Aurinkoinen jyrkänne:\n";
		AddSceneScores(2);
		highscores += "Pimiä laakso:\n";
		AddSceneScores(3);
	}
	private void AddSceneScores(int scene){
		string[] difTexts = new string[3]{"  Easy:\n", "  Normal:\n", "  Hard:\n"};
		if(scores[scene]==null){
			return;
		}
		for(int j=0; j<3 ; j++){
			highscores += difTexts[j];
			if(scores[scene][j]!=null){
				for(int i=0; i<scores[scene][j].Count; i++){
					// do the magic thin
					ScoreInfo gel=scores[scene][j][i];
				
					string str = gel.name + " cows:" + gel.cows.ToString() + " time:"
						+ gel.time.ToString()+ "\n";
					highscores += str;
				}
			}
		}
	}
}
