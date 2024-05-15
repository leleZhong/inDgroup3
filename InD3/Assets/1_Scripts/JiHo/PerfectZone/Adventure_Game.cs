using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventure_Game : MonoBehaviour
{
    protected AdventureSystem_Manager adventureSystem_Manager;

  
    private void Awake()
    {
        adventureSystem_Manager = GameObject.Find("PerfectZoneSystem_Manager").GetComponent<AdventureSystem_Manager>();
    }

    public virtual void  Play_MiniGame()
    {

    }    
    public virtual void StartSetting()
    {

    }       
 


}
