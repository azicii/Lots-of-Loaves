using System.Linq;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] int abilityIndex;
    public GameObject[] abilities;

    void Start()
    {
        SetAbilityActive();
    }

    void Update()
    {
        int previousAbility = abilityIndex;

        ProcessEandQkey();

        if (previousAbility != abilityIndex)
        {
            SetAbilityActive();
        }
    }

    void ProcessEandQkey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (abilityIndex >= abilities.Length - 1)
            {
                abilityIndex = 0;
            }
            else
            {
                abilityIndex++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (abilityIndex == 0)
            {
                abilityIndex = abilities.Length - 1;
            }
            else
            {
                abilityIndex--;
            }
        }
    }

    void SetAbilityActive()
    {
        int currentAbility = 0;

        foreach (GameObject ability in abilities)
        {
            if (abilityIndex == currentAbility)
            {
                ability.SetActive(true);
            }
            else
            {
                ability.SetActive(false);
            }
            currentAbility++;
        }
    }
    
    //method below is not very good performance wise. It adds a new item to the 
    //array when it is called by concatenating a new array to the old abilities
    //and storing the new concatenated array as abilities. Since there are only 3 
    //abilities in the game it is not a problem. 
    public void AddNewAbility(GameObject ability)
    {
        abilities = abilities.Concat(new GameObject[] { ability }).ToArray();
        abilityIndex = abilities.Length - 1;
        SetAbilityActive();
    }

    //Additional ability switch input. Uncomment if becomes necessary.
    ////If ever want to implement numberkey input ability switch functionality,
    ////uncomment the function below
    //void ProcessKeyInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1) && abilities.Length >= 1)
    //    {
    //        abilityIndex = 0;
    //        SetAbilityActive();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2) && abilities.Length >= 2)
    //    {
    //        abilityIndex = 1;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3) && abilities.Length >= 3)
    //    {
    //        abilityIndex = 2;
    //    }
    //}

    ////If ever want to implement scroll wheel ability switch functionality,
    ////uncomment the function below
    //void ProcessScrollWheel()
    //{
    //    if (Input.GetAxis("Mouse ScrollWheel") < 0)
    //    {
    //        if (abilityIndex >= abilities.Length - 1)
    //        {
    //            abilityIndex = 0;
    //        }
    //        else
    //        {
    //            abilityIndex++;
    //        }
    //    }
    //    if (Input.GetAxis("Mouse ScrollWheel") > 0)
    //    {
    //        if (abilityIndex == 0)
    //        {
    //            abilityIndex = abilities.Length - 1;
    //        }
    //        else
    //        {
    //            abilityIndex--;
    //        }
    //    }
    //}
}