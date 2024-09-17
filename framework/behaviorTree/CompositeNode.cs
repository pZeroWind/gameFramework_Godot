using System.Collections.Generic;

namespace Framework;

public abstract class CompositeNode : BehaviorNode
{
    protected override bool EnableChildren => true;
}