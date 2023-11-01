using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager Instance;
    [SerializeField] BattleUIDisplay uIDisplay;
    [SerializeField] GameObject blockInput;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void BlockInput(bool isBlockInput)
    {
        blockInput.SetActive(isBlockInput);
    }

}
