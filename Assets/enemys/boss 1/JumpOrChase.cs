using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrChase : BTnode
{
    public override IEnumerator Run(BehaviorTree bt)
    {


        Print();
        yield break;
    }
}
