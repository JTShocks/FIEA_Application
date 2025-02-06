using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueBall : MonoBehaviour, IReactable
{
    public void OnReaction()
    {
        throw new System.NotImplementedException();
    }

    public void React(Bomb.Element element)
    {
        if(element == Bomb.Element.Fire)
        {
            //React with fire explosion/ hot glue
        }

        switch(element)
        {
            case Bomb.Element.Ice:
                //Turn enemy into a ice cube
            break;
        }
    }
}
