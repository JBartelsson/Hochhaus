using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour, ISubscriber
{
    List<SingleCardUI> singleCardUIs = new List<SingleCardUI>();

    [SerializeField] private SingleCardUI cardUIPrefab;
    [SerializeField] private Transform cardsSpawnTarget;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitSubscriptions()
    {
        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        Debug.Log(env.name);
        env.CardSystem.OnCardSystemChanged += EnvOnOnCardSystemChanged;
    }

    public void ResetSubscriptions()
    {
        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        env.CardSystem.OnCardSystemChanged -= EnvOnOnCardSystemChanged;
    }

    private void EnvOnOnCardSystemChanged(object sender, CardSystem cardSystem)
    {
        foreach (var singleCardUI in singleCardUIs)
        {
            Destroy(singleCardUI.gameObject);
        }
        singleCardUIs.Clear();
        Debug.Log("OnCardSystemChanged");
        for (int i = 0; i < cardSystem.Hand.Count; i++)
        {
            SingleCardUI singleCardUI = Instantiate(cardUIPrefab, cardsSpawnTarget);
            singleCardUI.SetCardUI(cardSystem.Hand[i], cardsSpawnTarget.parent, this);
            singleCardUIs.Add(singleCardUI);
        }
   
    }
    
    
}