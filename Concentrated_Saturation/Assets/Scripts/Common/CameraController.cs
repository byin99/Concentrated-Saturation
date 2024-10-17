using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Player player;

    public Vector3 direction;

    public float positionSpeed = 0.1f;

    readonly int Boss_Hash = Animator.StringToHash("Boss");
    Animator animator;

    private void Start()
    {
        player = GameManager.Instance.Player;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SpeedUp();
    }

    private Vector3 velocity;
    void SpeedUp()
    {
        Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.5f);
    }

    public IEnumerator CameraMove()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(2);
        animator.enabled = false;
    }

    public IEnumerator BossDie(Vector3 pos)
    {
        pos.z = -10;
        transform.position = pos;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2);
        Time.timeScale = 1;
    }
}
