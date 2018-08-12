using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : BasePickup {

    // Use this for initialization
    public HealthUp()
    {
        Name = "Health Pack";
        Description = "Adds to your health";
    }

    public override void PowerupEffect()
    {
        var gm = FindObjectOfType<GameManager>();
        if (gm.CurrentPlayerHealth < gm.MaxPlayerHealth)
        {
            gm.CurrentPlayerHealth+=1;
        }
    }


}
