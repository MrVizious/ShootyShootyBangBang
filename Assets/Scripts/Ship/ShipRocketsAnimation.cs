using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugExtension;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShipRocketsAnimation : MonoBehaviour
{
    public ParticleSystem[] forwardRockets;
    public ParticleSystem[] backwardRockets;
    public ParticleSystem[] leftRockets;
    public ParticleSystem[] rightRockets;

    public ShipMovement shipMovement;
    public float padding = 0.2f;

    private void Start()
    {
        for (int i = 0; i < forwardRockets.Length; i++)
        {
            forwardRockets[i].Stop();
        }
        for (int i = 0; i < backwardRockets.Length; i++)
        {
            backwardRockets[i].Stop();
        }
        for (int i = 0; i < leftRockets.Length; i++)
        {
            leftRockets[i].Stop();
        }
        for (int i = 0; i < rightRockets.Length; i++)
        {
            rightRockets[i].Stop();
        }
    }

    private void Update()
    {
        float forwardStrength = Vector2.Dot(transform.up, Vector2.ClampMagnitude(shipMovement.playerInput, 1));
        if (forwardStrength > padding)
        {
            for (int i = 0; i < forwardRockets.Length; i++)
            {
                if (!forwardRockets[i].isPlaying)
                {
                    forwardRockets[i].Play();
                }
            }
            for (int i = 0; i < backwardRockets.Length; i++)
            {
                backwardRockets[i].Stop();
            }
        }
        else if (forwardStrength < -padding)
        {
            for (int i = 0; i < backwardRockets.Length; i++)
            {
                if (!backwardRockets[i].isPlaying)
                {
                    backwardRockets[i].Play();
                }
            }
            for (int i = 0; i < forwardRockets.Length; i++)
            {
                forwardRockets[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < forwardRockets.Length; i++)
            {
                forwardRockets[i].Stop();
            }
            for (int i = 0; i < backwardRockets.Length; i++)
            {
                backwardRockets[i].Stop();
            }
        }

        float rightStrength = Vector2.Dot(transform.right, Vector2.ClampMagnitude(shipMovement.playerInput, 1));
        if (rightStrength > padding)
        {
            for (int i = 0; i < rightRockets.Length; i++)
            {
                if (!rightRockets[i].isPlaying)
                {
                    rightRockets[i].Play();
                }
            }
            for (int i = 0; i < leftRockets.Length; i++)
            {
                leftRockets[i].Stop();
            }
        }
        else if (rightStrength < -padding)
        {
            for (int i = 0; i < leftRockets.Length; i++)
            {
                if (!leftRockets[i].isPlaying)
                {
                    leftRockets[i].Play();
                }
            }
            for (int i = 0; i < rightRockets.Length; i++)
            {
                rightRockets[i].Stop();
            }
        }
        else
        {
            for (int i = 0; i < rightRockets.Length; i++)
            {
                rightRockets[i].Stop();
            }
            for (int i = 0; i < leftRockets.Length; i++)
            {
                leftRockets[i].Stop();
            }
        }
        //Debug.Log("Forward: " + forwardStrength);
        //DebugExtension.DrawArrow.ForDebug(transform.position, transform.up, Color.red);
        //DebugExtension.DrawArrow.ForDebug(transform.position, shipMovement.playerInput, Color.green);
        //DebugExtension.DrawArrow.ForDebug(transform.position, ((Vector2)transform.up + shipMovement.playerInput).normalized * forwardStrength, Color.blue);
    }

    public void StartRockets()
    {
        for (int i = 0; i < forwardRockets.Length; i++)
        {
            forwardRockets[i].Play();
        }
        for (int i = 0; i < backwardRockets.Length; i++)
        {
            backwardRockets[i].Play();
        }
        for (int i = 0; i < leftRockets.Length; i++)
        {
            leftRockets[i].Play();
        }
        for (int i = 0; i < rightRockets.Length; i++)
        {
            rightRockets[i].Play();
        }
    }


}


#if UNITY_EDITOR
[CustomEditor(typeof(ShipRocketsAnimation))]
public class ShipRocketsAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ShipRocketsAnimation script = (ShipRocketsAnimation)target;

        if (GUILayout.Button("Play"))
        {
            script.StartRockets();
        }
    }
}
#endif