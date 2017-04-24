using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContextManager : MonoBehaviour
{
    [Serializable]
    public class UIContextPair
    {
        public UIContextType type;
        public UIContextView view;
    }


    Player player
    {
        get
        {
            return FindObjectOfType<Player>();
        }
    }

    public List<UIContextPair> UIContextViews;
    public PlayerContextView PlayerContextView;

    UIContextData selectedContext;
    private void Start()
    {
        PlayerContextView.Bind(FindObjectOfType<PlayerDataObject>());
    }
    void Update()
    {
        selectedContext = player.GetNearestContext();

        foreach (var uicp in UIContextViews)
        {
            if (selectedContext != null && selectedContext.ContextVisible)
            {
                if (uicp.type == selectedContext.type)
                {
                    uicp.view.gameObject.SetActive(true);
                    uicp.view.Bind(selectedContext);
                }
                else
                {
                    uicp.view.gameObject.SetActive(false);
                }
            }
            else
            {
                uicp.view.gameObject.SetActive(false);
            }
        }
    }
}
