using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer sprite;

    SpriteRenderer playerSprite;

    // Vị trí và góc xoay cho tay trái/phải khi lật hướng
    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35f);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135f);

    private void Awake()
    {
        // Lấy SpriteRenderer của Player (thư mục cha)
        // [0] là chính nó, [1] là SpriteRenderer của Player
        playerSprite = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = playerSprite.flipX;

        if (isLeft) // Tay trái (Vũ khí cận chiến)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            sprite.flipY = isReverse;
            sprite.sortingOrder = isReverse ? 4 : 6;
        }
        else // Tay phải (Vũ khí tầm xa / Súng)
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            sprite.flipX = isReverse;
            sprite.sortingOrder = isReverse ? 6 : 4;
        }
    }
}