public abstract class PassiveBase
{
    protected PassiveData _data;

    public PassiveBase(PassiveData data)
    {
        _data = data;
    }

    public abstract void Apply();
    public abstract void Remove();
}