using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class login : MonoBehaviour
{
    public TextMeshProUGUI messRG;
    public TextMeshProUGUI messLG;
    public GameObject Loading;

    public int DIEM;
    public InputField i_username, i_password;
    private string apiUrl = "https://apigame-una1.onrender.com/api/checklogin";


    public InputField usernamerg, passwordrg,confimpasswordrg;
    private string apiUrlRG = "https://apigame-una1.onrender.com/api/add-user";
    public GameObject loginpanel;
    public GameObject Regispanel;


    public void btnRegister()
    {
        Regispanel.SetActive(true);
        loginpanel.SetActive(false);
    }
    public void btnback()
    {
        loginpanel.SetActive(true);
        Regispanel.SetActive(false);


    }


    public void checkLogin()
    {
        var u = i_username.text;
        var p = i_password.text;
        string jsonData = "{\"username\":\"" + u + "\",\"password\":\"" + p + "\"}";
        StartCoroutine(PostRequest(jsonData));
        Loading.SetActive(true);


    }

    public void register()
    {
        var urg = usernamerg.text;
        var prg = passwordrg.text;
        var cfprg = confimpasswordrg.text;

        if(prg != cfprg ) {

            messRG.text = "Không trùng Password";
            Debug.Log("không trùng pass");
        
        }
        else 
        {
            string jsonRegister = "{\"username\":\"" + urg + "\",\"password\":\"" + prg + "\"}";
            StartCoroutine(PostRegister(jsonRegister));
        }
        Loading.SetActive(true);





    }




    IEnumerator GetDataFromAPI()
    {
        // Tạo request để gọi API
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Gửi request và đợi phản hồi
        yield return request.SendWebRequest();

        // Kiểm tra xem có lỗi không
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Lỗi khi gọi API: " + request.error);
        }
        else
        {
            // Lấy dữ liệu từ phản hồi của API
            string responseData = request.downloadHandler.text;

            // Xử lý dữ liệu ở đây
            Debug.Log("Dữ liệu từ API: " + responseData);
        }
    }
    IEnumerator PostRequest(string jsonData)
    {
        // Tạo request
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            // Gửi dữ liệu JSON
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Đợi phản hồi từ server
            yield return request.SendWebRequest();
            Loading.SetActive(true);


            // Kiểm tra lỗi
            if (request.result != UnityWebRequest.Result.Success)
            {
                Loading.SetActive(false);

                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Xử lý phản hồi thành công
                Loading.SetActive(false);

                //  Debug.Log("Response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;
               // Debug.Log("Response: " + responseJson);
                try
                {
                    ResponseData responseData = JsonUtility.FromJson<ResponseData>(json);

                    if (responseData.status == 200)
                    {

                        Loading.SetActive(false);

                        // Load Scene 1 (assuming you have a scene named "Scene_1")
                        SceneManager.LoadScene(1);

                        userData[] manguser = responseData.data;
                        userData user = manguser[0];
                        Debug.Log(user.username);


                        DIEM = user.score;
                        Debug.Log(DIEM);

                    }
                    else
                    {
                        Loading.SetActive(false);

                        messLG.text = "Sai username hoặc Password";

                        Debug.LogWarning("API response status is not 200: " + responseData.status);
                     
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing JSON response: " + e.Message);
                }



            }
            request.Dispose();
        }
    }


    IEnumerator PostRegister(string jsonRegister)
    {
        // Tạo request
        using (UnityWebRequest request = new UnityWebRequest(apiUrlRG, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            // Gửi dữ liệu JSON
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRegister);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Đợi phản hồi từ server
            yield return request.SendWebRequest();
            Loading.SetActive(true);

            // Kiểm tra lỗi
            if (request.result != UnityWebRequest.Result.Success)
            {
                Loading.SetActive(false);

                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Xử lý phản hồi thành công
                Loading.SetActive(false);

                //  Debug.Log("Response: " + request.downloadHandler.text);
                string json = request.downloadHandler.text;
                // Debug.Log("Response: " + responseJson);
                try
                {
                    ResponseData responseData = JsonUtility.FromJson<ResponseData>(json);

                    if (responseData.status == 200)
                    {
                        Loading.SetActive(false);

                        loginpanel.SetActive(true);
                        Regispanel.SetActive(false);


                    }
                    else
                    {
                        Loading.SetActive(false);

                        messRG.text = "Tên đăng nhập đã được sử dụng";
                        Debug.LogWarning("API response status is not 200: " + responseData.status);

                    }
                }
                catch (System.Exception e)
                {
                    Loading.SetActive(false);

                    Debug.LogError("Error parsing JSON response: " + e.Message);
                }



            }
            request.Dispose();
        }
    }



}

[System.Serializable]
public class ResponseData
{
    public int status;
    public string message;
    public userData[] data;

  

}

[System.Serializable]
public class userData
{
    public string _id, username, password, createdAt, updatedAt;
    public int score, x, y, hp, __v;


}

