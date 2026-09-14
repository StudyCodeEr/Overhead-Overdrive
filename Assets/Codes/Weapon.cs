using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        // Gán tham chiếu Player an toàn qua GameManager
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        //..Test Code..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;

        if (id == 0)
            Batch();
    }

    public void Init(ItemData data)
    {
        // 1. Gán vị trí và tên cho Weapon
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // 2. Gán các chỉ số từ ItemData vào Weapon
        this.id = data.itemId;
        this.damage = data.baseDamage;
        this.count = data.baseCount;

        // 3. Tìm prefabId tương ứng trong PoolManager
        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150f;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        // 4. Kích hoạt và gán Sprite bàn tay (Thêm kiểm tra an toàn tránh tràn mảng hands)
        int handIndex = (int)data.itemType;
        if (player.hands != null && handIndex < player.hands.Length)
        {
            Hand hand = player.hands[handIndex];
            hand.sprite.sprite = data.hand;
            hand.gameObject.SetActive(true);
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
            }

            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // Đổi 360 thành 360f để phép chia trả về kiểu float chính xác
            Vector3 rotVec = Vector3.forward * 360f * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}