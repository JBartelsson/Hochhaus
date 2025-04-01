using CommandSystem.Commands;
using NUnit;
using UnityEngine;

namespace UI
{
    public class UIReroll :MonoBehaviour
    {
        public void UIRerollCall()
        {
            Environment env = EnvironmentManager.Instance.GetActiveEnvironment();
            Debug.Log("Reroll Shhop Btn");
            if (!env.BlockActions)
            {
                Debug.Log("Reroll Shop Triggered");
                RerollShopCommand rerollShopCommand = new RerollShopCommand(env, null);
                env.CommandInvoker.Execute(rerollShopCommand);
                
            }
            
        }
    }
}