using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public ParticleSystem ps;
    public bool pause;
    public PlayerController pc;

    bool isJumping;

    private void Update()
    {
        var main = ps.main;
        var em = ps.emission;
        var sh = ps.shape;

        if (pc.onGround && pc.currentVelocity > 5f && pc.state == PlayerController.MovementState.roll)
        {
            em.rateOverTime = Random.Range(Mathf.CeilToInt(pc.currentVelocity * 1f), Mathf.CeilToInt(pc.currentVelocity * 1.5f));
        }
        else if (pc.onGround && pc.currentVelocity > 5f && pc.state == PlayerController.MovementState.none)
        {
            em.rateOverTime = Random.Range(Mathf.CeilToInt(pc.currentVelocity * 0.75f), Mathf.CeilToInt(pc.currentVelocity * 1f));
        }
        else if (pc.currentVelocity > 5f && pc.isRising && jumpTrailCounter > 0f)
        {
            em.rateOverTime = Mathf.CeilToInt(pc.currentVelocity);
        }
        else
        {
            em.rateOverTime = 0f;
        }

        if (pc.state == PlayerController.MovementState.crouch || pc.state == PlayerController.MovementState.roll)
        {
            sh.position = new Vector3(sh.position.x, sh.position.y, -0.8f);
        }
        else
        {
            sh.position = new Vector3(sh.position.x, sh.position.y, -1.6f);
        }

        jumpTrailCounter -= Time.deltaTime;
    }

    float jumpTrailCounter;
    void Jump()
    {
        var main = ps.main;
        var sh = ps.shape;


        main.startSpeed = 20f;
        sh.angle = 90f;
        main.startLifetime = 0.3f;

        ps.Emit(16);

        main.startSpeed = 2.25f;
        sh.angle = 80f;
        main.startLifetime = 0.5f;

        jumpTrailCounter = 0.3f;
    }

    void RollHop()
    {
        var main = ps.main;
        var sh = ps.shape;


        main.startSpeed = 12f;
        sh.angle = 90f;
        main.startLifetime = 0.3f;

        ps.Emit(8);

        main.startSpeed = 2.25f;
        sh.angle = 80f;
        main.startLifetime = 0.5f;
    }
}
