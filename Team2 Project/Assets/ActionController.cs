using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range = 3.0f;  // 아이템 습득이 가능한 최대 거리 (기본값 설정)

    private bool pickupActivated = false;  // 아이템 습득 가능 여부

    private RaycastHit hitInfo;  // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어의 오브젝트만 습득 가능

    [SerializeField]
    private TMP_Text actionText;  // 행동 안내 텍스트

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null && hitInfo.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);

        // 안전한 접근을 위해 null 체크 추가
        var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
        if (itemPickUp != null)
        {
            actionText.text = "ITEM GET " + "<color=yellow>(E)</color>";
            //actionText.text = itemPickUp.item.itemName + " Get " + "<color=yellow>(E)</color>";
        }
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
                if (itemPickUp != null)
                {
                    Debug.Log(itemPickUp.item.itemName + " Picked up.");
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();
                }
            }
        }
    }
}
