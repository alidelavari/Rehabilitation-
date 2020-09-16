using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int level { get; }
    public int angle { get;}
    public float consistency { get;}
    public int accepted_error { get; }
    public int return_policy { get; }
    public bool current { get;}

    public Level(int level, int angle, float consistency, int accepted_error, int return_policy, bool current)
    {
        this.level = level;
        this.angle = angle;
        this.consistency = consistency;
        this.accepted_error = accepted_error;
        this.return_policy = return_policy;
        this.current = current;
    }
}
