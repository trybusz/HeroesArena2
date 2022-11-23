using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterParent : MonoBehaviour
{

    //THIS IS ACTUALLY A PARENT FOR ANYTHING THAT TAKES DAMAGE OR KNOCKBACK
    public int thisCharScore = 0;
    public float scoreTime = 0.0f;
    public bool onCtrlPoint = false;
    public bool isDead = false;
    public bool isStunned = false;
    public bool isSwitching = false;
    public bool isSwitching2 = false;
    public bool onSpawn = false;

    public virtual float GetHealth()
    {
        return 0.2f;
    }
    public virtual float GetPrimary()
    {
        return 0.2f;
    }
    public virtual float GetSpecial()
    {
        return 0.2f;
    }

    public virtual void TakeDamage(int damage)
    {

    }
    public virtual void TakeStun(float duration)
    {

    }
    public virtual void CharSwitching()
    {

    }
    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("ControlPoint"))
        {
            scoreTime = Time.timeSinceLevelLoad + 1.0f;
            onCtrlPoint = true;
        }
        if (collision.CompareTag("SpawnProtection"))
        {
            onSpawn = true;
        }

    }
    public void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("ControlPoint"))
        {
            scoreTime = 0.0f;
            onCtrlPoint = false;
        }
        if (collision.CompareTag("SpawnProtection"))
        {
            onSpawn = false;
        }
    }

    public virtual void TakeKnockback(float knockback, Vector3 KBPosition, float duration)
    {
        //Good base value to send are cp.TakeKnockback(20.0f, transform.parent.position, 0.1f);
    }
}

