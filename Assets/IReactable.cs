using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReactable
{
    void React(Bomb.Element element);
    void OnReaction();
}
