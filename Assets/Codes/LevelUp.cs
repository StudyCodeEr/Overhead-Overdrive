using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items; // Khai báo mảng chứa các thẻ Item

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        // Bắt buộc parameter true để quét được cả những Item đang bị ẩn (Inactive)
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. Tắt tất cả các thẻ Item hiện tại
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. Random chọn 3 item khác nhau trong danh sách
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];

            // 3. Hiển thị 3 item được chọn lên bảng
            if (ranItem.level == ranItem.data.damages.Length){
                items[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}