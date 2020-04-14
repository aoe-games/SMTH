using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCtrl : MonoBehaviour
{
    public KeyCode m_debugKeyCode = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActorUpdate(EncounterCtrl encounterCtrl)
    {
        if (Input.GetKeyDown(m_debugKeyCode))
        {
            encounterCtrl.EnqueueActorTurn(this);
        }
    }

    public IEnumerator ProcessTurn(EncounterCtrl encounterCtrl)
    {
        Debug.Log("Actor processing turn: " + m_debugKeyCode);
        yield return null;
    }
}
