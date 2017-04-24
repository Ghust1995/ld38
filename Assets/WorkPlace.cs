using UnityEngine;
public abstract class WorkPlace : UIContextData
{
    public abstract bool CanHire();
    public abstract void Hire(GameObject commonCitizen);
}