using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUIPage
{
    NONE = 0,
    CONNECTION =1,
    GAMEPLAY = 2,
}

public class UIManager : MonoBehaviour
{

    public GameObject ConnectionUI, GameplayUI;

    private static Dictionary<EUIPage, GameObject> UIPageDict;

    private static EUIPage CurrentUIPage = EUIPage.NONE;

    public static UnityPageEvent OnChangePage = new UnityPageEvent();

    // Use this for initialization
    void Start()
    {
        UIPageDict = new Dictionary<EUIPage, GameObject>();
        UIPageDict.Add(EUIPage.CONNECTION, ConnectionUI);
        UIPageDict.Add(EUIPage.GAMEPLAY, GameplayUI);

        DomeNetworkManager.OnClientDisconnected.AddListener(() => SetUI(EUIPage.CONNECTION));
        DomeNetworkManager.OnClientConnected.AddListener(() => SetUI(EUIPage.GAMEPLAY));

        SetUI(EUIPage.CONNECTION);
    }

    void SetUI(EUIPage eUIPage)
    {
        if (eUIPage != CurrentUIPage)
        {
            foreach (KeyValuePair<EUIPage, GameObject> entry in UIPageDict)
            {
                entry.Value.SetActive(entry.Key.Equals(eUIPage));
            }

            CurrentUIPage = eUIPage;
            OnChangePage.Invoke(CurrentUIPage);
        }



    }


}
