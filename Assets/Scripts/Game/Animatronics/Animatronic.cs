using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animatronic : MonoBehaviour
{
    protected string animatronicName;
    protected Room[] route;
    protected int iALevel;
    protected int randomNumber;

    IEnumerator RNG(float duration)
    {
        while(!GameManager.Instance.gameOver)
        {
            randomNumber = Random.Range(0,21);
            yield return new WaitForSeconds(duration);
        }
    }
}
