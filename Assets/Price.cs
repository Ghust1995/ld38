using System;

public class Price
{
    public long value;
    public Resource currency;
    public Func<bool> condition = null;
    public bool visible
    {
        get {
            return condition == null ? true : condition();
        }
    }
    public bool OnDisplay
    {
        get
        {
            return visible;
        }
    }
}
