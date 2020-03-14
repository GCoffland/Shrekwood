using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE { PLAY, PAUSE, INVISIBLE };
public interface Selectable
{
    void select(STATE s);
}
