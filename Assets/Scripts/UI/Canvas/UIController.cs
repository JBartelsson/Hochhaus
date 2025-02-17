using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UI.AnimationCommands;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class UIController : UIBase, ISubscriber
{
    List<SingleCardUI> singleCardUIs = new List<SingleCardUI>();

    [SerializeField] private SingleCardUI cardUIPrefab;
    [SerializeField] private Transform cardsSpawnTarget;
    [SerializeField] ScoreUI scoreUI;
    [SerializeField] TowerVisual towerVisual;
    [SerializeField] EffectDisplay effectDisplay;
    [SerializeField] private VisualSettings _visualSettings;
    
    public VisualSettings VisualSettings => _visualSettings;

    public EffectDisplay EffectDisplay => effectDisplay;

    public TowerVisual TowerVisual => towerVisual;

    public ScoreUI ScoreUI => scoreUI;

    [SerializeField] List<UIBase> baseUIs = new List<UIBase>();

    private AnimationCommandInvoker animationCommandInvoker = new ();

    public AnimationCommandInvoker AnimationCommandInvoker => animationCommandInvoker;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void InitSubscriptions(UIController uiController)
    {
        base.InitSubscriptions(uiController);
        animationCommandInvoker.InitSubscriptions(this);
        towerVisual.InitSubscriptions(this);

        Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
        env.CardSystem.OnCardSystemChanged += EnvOnOnCardSystemChanged;
        foreach (var baseUI in baseUIs)
        {
            baseUI.InitSubscriptions(this);
        }
    }

    public override void ResetSubscriptions()
    {
        foreach (var baseUI in baseUIs)
        {
            baseUI.ResetSubscriptions();
        }
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
        for (int i = 0; i < cardSystem.Hand.Count; i++)
        {
            SingleCardUI singleCardUI = Instantiate(cardUIPrefab, cardsSpawnTarget);
            singleCardUI.SetCardUI(cardSystem.Hand[i], cardsSpawnTarget.parent, this);
            singleCardUIs.Add(singleCardUI);
        }
   
    }
    
    
}