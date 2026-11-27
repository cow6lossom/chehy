using Tyu.Scripts;
using UnityEngine;

[DefaultExecutionOrder(-1000)] //가장 먼저 실행될수 있게
public class GameSession : MonoSingleton<GameSession>
{
    [field: SerializeField]
    public CurrencyWallet CurrencyWallet { get; private set; }

    [field: SerializeField]
    public PassiveManager PassiveManager { get; private set; }

    private void Awake()
    {
        if (!MakeSingleton())
            return;

        DontDestroyOnLoad(gameObject);
    }
}
