using UnityEngine;

public enum UIContextType
{
    player,
    tree,
    store,
    woodshop,
}

public abstract class UIContextData : MonoBehaviour
{
    public UIContextType type;
    public bool ContextVisible = true;
    public int AnimationTime = 30;

    public virtual void Initialize()
    {
        // Do nothing
    }

}