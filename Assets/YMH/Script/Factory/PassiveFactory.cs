using UnityEngine;

public static class PassiveFactory
{
    public static PassiveBase Create(PassiveData data)
    {
        if (data == null)
            return null;

        switch (data.Type)
        {
            case PassiveType.Attack:
                return new AttackPassive(data);

            case PassiveType.Buff:
                return new BuffPassive(data);

            case PassiveType.Control:
                return new ControlPassive(data);

            case PassiveType.Special:
                return new SpecialPassive(data);
        }

        return null;
    }
}