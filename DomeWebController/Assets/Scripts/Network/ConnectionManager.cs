using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConnectionManager : MonoBehaviour
{

    public Button ConnectBtn;
    public TMP_InputField NameIF;

    private void Awake()
    {
        UIManager.OnChangePage.AddListener(OnStart);
    }

    void OnStart()
    {
        ConnectBtn.onClick.AddListener(Connect);
        InvokeRepeating("CheckIsReadyToConnect", 0, 0.5f);

    }

    void OnStart(EUIPage currentPage)
    {
        if (currentPage == EUIPage.CONNECTION)
        {
            OnStart();
        }
    }

    void End()
    {
        CancelInvoke();
        ConnectBtn.onClick.RemoveAllListeners();
    }

    private void CheckIsReadyToConnect()
    {
        ConnectBtn.interactable = IsReadyToConnect();
    }

    private bool IsReadyToConnect()
    {
        return NameIF.text.Length > 1;
    }

    private void Connect()
    {
        if (IsReadyToConnect())
        {
            End();
            StartCoroutine(DomeNetworkManager.TryConnect(OnEndTryConnect));
            Debug.Log("Connect");
        }
    }

    private void OnEndTryConnect(bool isConnected)
    {
        if (isConnected)
            End();
        else
            OnStart();

    }


}
