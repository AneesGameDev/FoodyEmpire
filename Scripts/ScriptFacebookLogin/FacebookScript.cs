/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveDataFb
{
	public string ID;
	public string email;
	public string Name;
	public string imgaestring;
	public int fblevel;
	public int fbcoins;
	public bool UserisLoggedin ;
	public Sprite profilesprite;
}

public class FacebookScript : MonoBehaviour
{
	public Text FB_userName;
	public Image image;
	public byte[] imgbyte;
	public string stringlevel;
	public string stringfbcoins;
	public GameObject LoginButton;
	public GameObject ShareButton;
	public CoinsControll SetCoins;
	public Board board;
	public GameData gameData;
	public SaveDataFb FbData = new SaveDataFb();
	public int tempcoins;
	public int templevel;
	public Text logintext;
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		//DontDestroyOnLoad(LoginButton);
		//DontDestroyOnLoad(ShareButton);
		if (!FB.IsInitialized)
		{
			// Initialize the Facebook SDK
			FB.Init(SetInit, onHidenUnity);
		}
		else
		{
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
			//StartCoroutine(ServerData.Instance.Register(ID, Name,email));

		}

		LoginButton.SetActive(true);
		ShareButton.SetActive(false);
	}
	void SetInit()
	{
		if (FbData.UserisLoggedin )
		{
			Debug.Log("Facebook is Login!");
		}
		else
		{
			Debug.Log("Facebook is not Logged in!");
		}
		DealWithFbMenus(FB.IsLoggedIn);
	}

	void onHidenUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void Start()
	{
		//PlayerPrefs.SetString("FBID", "0123");
		//PlayerPrefs.DeleteAll();
		SetCoins = FindObjectOfType<CoinsControll>();
		board = FindObjectOfType<Board>();
		gameData = FindObjectOfType<GameData>();
		if (gameData != null)
		{
			DontDestroyOnLoad(gameData);
		}
		if (board != null)
		{
			DontDestroyOnLoad(board);
		}
		if (SetCoins != null)
		{
			DontDestroyOnLoad(SetCoins);
		}

		if (PlayerPrefs.HasKey("prefid") && PlayerPrefs.HasKey("FBID") && PlayerPrefs.HasKey("FBNAME"))
		{
			
				Debug.Log("Start Is Caled Loggin is True");
			    FbData.ID = (PlayerPrefs.GetString("prefid"));
				ShareButton.SetActive(true);
				LoginButton.SetActive(false);
				FbData.Name = PlayerPrefs.GetString("FBNAME");
				FbData.email = PlayerPrefs.GetString("FBEMAIL");
				FbData.imgaestring = PlayerPrefs.GetString("FBIMAGE");
				FbData.fbcoins = PlayerPrefs.GetInt("FBCOINS");
				FbData.fblevel = PlayerPrefs.GetInt("FBLEVEL");
				FB_userName.text = FbData.Name;
				imgbyte = Convert.FromBase64String(FbData.imgaestring);
				Texture2D newTexture = new Texture2D(1, 1);
				newTexture.LoadImage(imgbyte);
				newTexture.Apply();
				Sprite balanceone = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
				image.sprite = balanceone;

			}
			else
			{
				ShareButton.SetActive(false);
				LoginButton.SetActive(true);
			}
		

		updateGameData();
		*//*		if (PlayerPrefs.HasKey("prefid") && PlayerPrefs.HasKey("prefname") && PlayerPrefs.HasKey("prefemail") && PlayerPrefs.HasKey("prefimagestring"))
				{*/
/*		
		if (FbData.UserisLoggedin) { 
			

		}
		else
		{
			ShareButton.SetActive(false);
			LoginButton.SetActive(true);
		}*//*
		//PlayerPrefs.SetInt("update", 11);
*//*		if (PlayerPrefs.HasKey("FBID") && PlayerPrefs.HasKey("FBNAME") && PlayerPrefs.HasKey("FBEMAIL") && PlayerPrefs.HasKey("FBIMAGE") && PlayerPrefs.HasKey("FBCOINS"))
		{
			if (PlayerPrefs.GetString("FBID") != null)
			{
				if (FbData.UserisLoggedin) { 
				FbData.ID = PlayerPrefs.GetString("FBID");
				StartCoroutine(GetPrefID(FbData.ID));
				}
			}


		}*//*

	}
	public void Update()
	{
*//*		if (PlayerPrefs.HasKey("Prefname"))
		{
			if (PlayerPrefs.GetString("Prefname") != null)
			{
				FB_userName.text = PlayerPrefs.GetString("Prefname");
			}

		}*//*
		updateGameData();
	}
	private void FixedUpdate()
	{

	}
	public void FBLogin()
	{

		List<string> permissions = new List<string>();
		permissions.Add("public_profile");
		permissions.Add("email");
		permissions.Add("gaming_user_picture");
		permissions.Add("gaming_profile");

		FB.LogInWithReadPermissions(permissions, AuthCallBack);


	}
*//*	public void FacebookLogout()
	{
		FB.LogOut();
		ID = "";
		Name = "";
		email = "";
	}*//*
	// Start is called before the first frame update
	void AuthCallBack(IResult result)
	{
		if (result.Error != null)
		{
			Debug.Log(result.Error);
		}
		else
		{
			if (FB.IsLoggedIn)
			{
				Debug.Log("Facebook is Login!");
				// Panel_Add.SetActive(true);
				var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
				FbData.ID = aToken.UserId;
				// Print current access token's User ID

				// Print current access token's granted permissions
				foreach (string perm in aToken.Permissions)
				{
					Debug.Log(perm);
				}
			}
			else
			{
				Debug.Log("Facebook is not Logged in!");
				Debug.Log("User cancelled login");
			}
			DealWithFbMenus(FB.IsLoggedIn);
		}
	}

	void DealWithFbMenus(bool isLoggedIn)
	{
		if (isLoggedIn)
		{
			FB.API("/me?fields=id,name,email", HttpMethod.GET, DisplayUsername);
			FB.API("/me/picture?type=square&height=4286width=428", HttpMethod.GET, DisplayProfilePic);

		}
		else
		{

		}

	}
	void DisplayUsername(IResult result)
	{
		if (result.Error == null)
		{

			FbData.ID = result.ResultDictionary["id"] as string;
			FbData.Name = result.ResultDictionary["name"] as string;
			FbData.email = (string)result.ResultDictionary["email"];
			FB_userName.text = FbData.Name;
			Debug.Log("" + FbData.Name);
			if (FbData.ID != null && FbData.Name != null && FbData.email != null)
			{
				PlayerPrefs.SetString("Prefid", FbData.ID);
				Debug.Log(FbData.ID);

				PlayerPrefs.SetString("Prefname", FbData.Name);
				PlayerPrefs.SetString("prefemail", FbData.email);
			}
		}
		else
		{
			Debug.Log(result.Error);
		}
	}

	void DisplayProfilePic(IGraphResult result)
	{
		if (result.Texture != null)
		{
			Debug.Log("Profile Pic");

			//FB_useerDp.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
			image.GetComponent<Image>().sprite = Sprite.Create(result.Texture, new Rect(0, 0, 700, 700), new Vector2(0,0));
			
			if (image != null)
			{
				imgbyte = image.sprite.texture.EncodeToPNG();
				FbData.imgaestring = Convert.ToBase64String(imgbyte);
				PlayerPrefs.SetString("prefimagestring", FbData.imgaestring);
				//FbData.imgaestring = imagestring;
			}
			logintext.text = "Name :" + FbData.Name + " email :" + FbData.email + "imagestring :" + FbData.imgaestring;
			if (FbData.ID != null && FbData.Name != null && FbData.email != null && stringlevel != null && FbData.imgaestring != null && stringfbcoins != null)
			{
				Debug.Log("Level is" + stringlevel);
				stringlevel = "1";
				stringfbcoins = "0";
				StartCoroutine(Register(FbData.ID, FbData.Name, FbData.email, stringlevel, FbData.imgaestring, stringfbcoins));
				//FB_Image = result.Texture.GetRawTextureData();
			}
			//FB_Img.material.mainTexture = result.Texture;

		}
		else
		{
			Debug.Log(result.Error);
		}
		Debug.Log(FbData.imgaestring);

	}

*//*	public void FacebookShare()
	{
		FB.ShareLink(new System.Uri("https://resocoder.com"), "Check it out!",
			"Good programming tutorials lol!",
			new System.Uri("https://resocoder.com/wp-content/uploads/2017/01/logoRound512.png"));
	}*//*

	public void FacebookGameRequest()
	{
		FB.AppRequest("Hey! Come and play this awesome game!", title: "Reso Coder Tutorial");
	}

	public IEnumerator DelayData()
	{
		yield return new WaitForSeconds(3.2f);



	}
	public IEnumerator Register(string fbid, string fbname, string fbemail, string userlevel, string fbimage, string fbbcoins)
	{
		
		List<IMultipartFormSection> form = new List<IMultipartFormSection>();
		Debug.Log("Register Run Before Form");
		Debug.Log("id"+ fbid);
		Debug.Log("name" + fbname); 
		Debug.Log("email" + fbemail); 
		Debug.Log("level" + userlevel);
		Debug.Log("userimage" + fbimage);
		Debug.Log("usercoins" + fbbcoins);
		form.Add(new MultipartFormDataSection("id", fbid));
		form.Add(new MultipartFormDataSection("name", fbname));
		form.Add(new MultipartFormDataSection("email", fbemail));
		form.Add(new MultipartFormDataSection("level", userlevel));
		form.Add(new MultipartFormDataSection("userimage", fbimage));
		form.Add(new MultipartFormDataSection("usercoins", fbbcoins));
		

		//form.Add(new MultipartFormDataSection("score", "5"));
		//form.Add(new MultipartFormDataSection("score",  score.text));
		//Debug.Log(fbid);
		//Debug.Log("Register Run Before Form 2nd");
		UnityWebRequest www = UnityWebRequest.Post("http://localhost/foodcrush/crushphp.php", form);
		//StartCoroutine(GetRequest());
		//Debug.Log("Register Run Before Form 3rd");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("" + www.error);
		}
		else
		{
			FbData.UserisLoggedin = true;
			// Debug.Log("You are Register Successfully. " );
			string responseText = www.downloadHandler.text;
			//Debug.Log("Response :" + responseText);
			string data = responseText;
			Debug.Log(data);
			if (data == "1")
			{
				Debug.Log("Befor Link is Work");
				StartCoroutine(GetRequest(fbid));
			}
			else if (data == "0")
			{
				Debug.Log("Befor Link is Work");
				FbData.fbcoins = 0;
				FbData.fblevel = 0;
				stringfbcoins = "0";
				stringlevel = "0";
				PlayerPrefs.SetInt("SetDataBaseLevel", 0);
				PlayerPrefs.SetInt("SetDataBaseCoins", 0);
				PlayerPrefs.SetInt("update", 1);
				
				UnityWebRequest wwwinsert = UnityWebRequest.Post("http://localhost/foodcrush/insertData.php", form);
				//StartCoroutine(GetRequest());
				yield return wwwinsert.SendWebRequest();

				if (wwwinsert.isNetworkError || wwwinsert.isHttpError)
				{
					Debug.Log("" + wwwinsert.error);
				}
				else
				{
					 Debug.Log("You are Register Successfully. " );
					string insertresponseText = wwwinsert.downloadHandler.text;
					//Debug.Log("Response :" + responseText);
					string insertdata = insertresponseText;
					Debug.Log(insertdata);
				}


				//UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			}

		}
	}

	public IEnumerator GetRequest(string ID)
	{
		List<IMultipartFormSection> form = new List<IMultipartFormSection>();
		form.Add(new MultipartFormDataSection("repeatunityid", ID));
		UnityWebRequest wwwGet = UnityWebRequest.Post("http://localhost/foodcrush/SelectData.php", form);
		yield return wwwGet.SendWebRequest();
		if (wwwGet.isNetworkError || wwwGet.isHttpError)
		{
			Debug.Log("" + wwwGet.error);
		}
		else
		{
			string responseText = wwwGet.downloadHandler.text;
			Debug.Log("Response :" + responseText);

			//Debug.Log("Response :" + responseText);
			string data = responseText;
			string[] split = data.Split('|');
			FbData.ID = split[0];
			FbData.Name = split[1];
			FbData.email = split[2];
			stringfbcoins = split[3];
			stringlevel = split[4];
			//Debug.Log("Load DataBAse Level is " + stringlevel);
			//Debug.Log("Load DataBAse coins is " + stringfbcoins);
			FbData.imgaestring = split[5];
			int.TryParse(stringfbcoins, out FbData.fbcoins);
			int.TryParse(stringlevel, out FbData.fblevel);

			PlayerPrefs.SetInt("SetDataBaseLevel", FbData.fblevel);
			PlayerPrefs.SetInt("SetDataBaseCoins", FbData.fbcoins);
			PlayerPrefs.SetInt("update", 0);
			//SetCoins.numberOfCoins = fbcoins;
			//board.level = fblevel;
		}

	}

	public IEnumerator GetPrefID(string ID)
	{
		List<IMultipartFormSection> form = new List<IMultipartFormSection>();
		form.Add(new MultipartFormDataSection("repeatunityid", ID));
		UnityWebRequest wwwGet = UnityWebRequest.Post("http://localhost/foodcrush/SelectData.php", form);
		yield return wwwGet.SendWebRequest();
		if (wwwGet.isNetworkError || wwwGet.isHttpError)
		{
			Debug.Log("" + wwwGet.error);
			FbData.Name = PlayerPrefs.GetString("FBNAME");
			FbData.email = PlayerPrefs.GetString("FBEMAIL");
			FbData.imgaestring = PlayerPrefs.GetString("FBIMAGE");
			FbData.fbcoins = PlayerPrefs.GetInt("FBCOINS");
			FbData.fblevel = PlayerPrefs.GetInt("FBLEVEL");
			FB_userName.text = FbData.Name;
			imgbyte = Convert.FromBase64String(FbData.imgaestring);
			Texture2D newTexture = new Texture2D(1, 1);
			newTexture.LoadImage(imgbyte);
			newTexture.Apply();
			Sprite balanceone = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
			image.sprite = balanceone;
		}
		else
		{
			string responseText = wwwGet.downloadHandler.text;
			Debug.Log("Response :" + responseText);

			//Debug.Log("Response :" + responseText);
			string data = responseText;
			string[] split = data.Split('|');
			FbData.ID = split[0];
			FbData.Name = split[1];
			FbData.email = split[2];
			stringfbcoins = split[3];
			stringlevel = split[4];
			//Debug.Log("Load DataBAse Level is " + stringlevel);
			//Debug.Log("Load DataBAse coins is " + stringfbcoins);
			FbData.imgaestring = split[5];
			int.TryParse(stringfbcoins, out FbData.fbcoins);
			int.TryParse(stringlevel, out FbData.fblevel);
			FbData.Name = PlayerPrefs.GetString("FBNAME");
			FbData.email = PlayerPrefs.GetString("FBEMAIL");
			FbData.imgaestring = PlayerPrefs.GetString("FBIMAGE");
			//PlayerPrefs.SetInt("SetDataBaseLevel", FbData.fblevel);
			//PlayerPrefs.SetInt("SetDataBaseCoins", FbData.fbcoins);
			//PlayerPrefs.SetInt("update", 0);
			//SetCoins.numberOfCoins = fbcoins;
			//board.level = fblevel;
			FB_userName.text = FbData.Name;
			imgbyte = Convert.FromBase64String(FbData.imgaestring);
			Texture2D newTexture = new Texture2D(1, 1);
			newTexture.LoadImage(imgbyte);
			newTexture.Apply();
			Sprite balanceone = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
			image.sprite = balanceone;
		}

	}

	public void updateFbData()
	{


	}
	public void updateGameData()
	{
		if (PlayerPrefs.HasKey("coins"))
		{
			tempcoins = PlayerPrefs.GetInt("coins");
			string stringfbtempcoins = tempcoins.ToString();
		}
		else
		{
			tempcoins = 0;
			string stringfbtempcoins = tempcoins.ToString();

		}
		if (PlayerPrefs.HasKey("preflevel"))
		{
			templevel = 1 + (PlayerPrefs.GetInt("preflevel"));
			string stringtemplevel = templevel.ToString();
		}
		else
		{
			templevel = 0;
			string stringtemplevel = templevel.ToString();
		}
	}
	public void SaveFBData()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fileFb = File.Create(Application.persistentDataPath + "playerFb.dat");
		SaveDataFb data = new SaveDataFb();
		data = FbData;
		formatter.Serialize(fileFb, data);
		fileFb.Close();


	}
	public void LoadFbData()
	{
		if (File.Exists(Application.persistentDataPath + "playerFb.dat"))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "playerFb.dat", FileMode.Open);
			FbData = formatter.Deserialize(file) as SaveDataFb;
*//*			if (FbData.ID != null && FbData.Name != null && FbData.email != null)
			{
				FB_userName.text = FbData.Name;
				imgbyte = Convert.FromBase64String(FbData.imgaestring);
				Texture2D newTexture = new Texture2D(1, 1);
				newTexture.LoadImage(imgbyte);
				newTexture.Apply();
				Sprite balanceone = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
				image.sprite = balanceone;


			}*//*
		}
		else
		{
			FbData = new SaveDataFb();
		}
	}
	private void OnApplicationQuit()
	{
		Debug.Log("FbLoggedIn" + FbData.UserisLoggedin);
		if (FbData.UserisLoggedin)
		{
			PlayerPrefs.SetString("FBID", FbData.ID);
			PlayerPrefs.SetString("FBNAME", FbData.Name);
			PlayerPrefs.SetString("FBEMAIL", FbData.email);
			PlayerPrefs.SetString("FBIMAGE", FbData.imgaestring);
			PlayerPrefs.SetInt("FBCOINS", FbData.fbcoins);
			PlayerPrefs.SetInt("FBLEVEL", FbData.fblevel);

		}
		if (PlayerPrefs.HasKey("FBID"))
		{
			if (PlayerPrefs.GetString("FBID") != null)
			{

				if (tempcoins > PlayerPrefs.GetInt("FBCOINS") || templevel > PlayerPrefs.GetInt("FBLEVEL"))
				{
					int coins = tempcoins;
					string newcoins = coins.ToString();
					int level = templevel;
					string newlevel = level.ToString();

					StartCoroutine(UpdateQuery(PlayerPrefs.GetString("FBID"), newcoins, newlevel));

				}
			}
		}
		else
		{
			Debug.Log("FBID Does Not Exist");
		}
	}
	public IEnumerator UpdateQuery(string fbid, string updatecoins, string updatelevel)
	{
		List<IMultipartFormSection> form = new List<IMultipartFormSection>();
		Debug.Log("FBID is " + fbid + ", UpDate Coins is" + updatecoins);
		form.Add(new MultipartFormDataSection("userid", fbid));
		form.Add(new MultipartFormDataSection("updatelevel", updatelevel));
		form.Add(new MultipartFormDataSection("updatecoins", updatecoins));


		//form.Add(new MultipartFormDataSection("score", "5"));
		//form.Add(new MultipartFormDataSection("score",  score.text));
		//Debug.Log(fbid);
		UnityWebRequest www = UnityWebRequest.Post("http://localhost/foodcrush/UpdateData.php", form);
		//StartCoroutine(GetRequest());
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("" + www.error);
		}
		else
		{

			string responseText = www.downloadHandler.text;
			Debug.Log("Response :" + responseText);
			PlayerPrefs.SetInt("FBCOINS", tempcoins);
			PlayerPrefs.SetInt("FBLEVEL", templevel);
		}
	}

}
*/