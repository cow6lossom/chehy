using UnityEngine;

public abstract class PassiveBase // 플레이어가 가지고 있는 패시브의 정보
{
    protected PassiveData _data;

    public PassiveData Data => _data;
    public int Level = 1;

    public PassiveBase(PassiveData data)
    {
        _data = data;
    }

    public void LevelUp()
    {
        if (Level >= 3)
            return;

        Level++;
    }

    public abstract void Apply();
    public abstract void Remove();
}