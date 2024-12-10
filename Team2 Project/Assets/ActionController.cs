using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range = 10.0f;  // 아이템 습득이 가능한 최대 거리 (기본값 설정)

    private bool pickupActivated = false;  // 아이템 습득 가능 여부

    private RaycastHit hitInfo;  // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어의 오브젝트만 습득 가능

    [SerializeField]
    private TMP_Text actionText;  // 행동 안내 텍스트

    [SerializeField]
    private TMP_Text itemPickedText;

    [SerializeField]
    private Inventory theInventory;

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

        if (Input.GetKeyDown(KeyCode.F)) {
            itemPickedText.gameObject.SetActive(false);
        }
    }

    private void CheckItem()
    {
        float sphereRadius = 0.3f;

        if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hitInfo, range, layerMask)) {
            if (hitInfo.transform != null && hitInfo.transform.CompareTag("Item")) {
                ItemInfoAppear();
            }
        } else {
            ItemInfoDisappear();
        } // 이건 구를 발사해서 충돌을 감지하고 오브젝트를 확인하는 코드. ray 보다 범위가 넓음.
        
        // if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        // {
        //     if (hitInfo.transform != null && hitInfo.transform.CompareTag("Item"))
        //     {
        //         ItemInfoAppear();
        //     }
        // }
        // else
        // {
        //     ItemInfoDisappear();
        // } 이전의 코드. 카메라에서 나가는 광선을 기준으로 거리를 계산했는데 이게 카메라가 캐릭터 뒤에 있어서 잘 감지를 못함. 그래서 위에 코드로 수정함.
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);

        // 안전한 접근을 위해 null 체크 추가
        var itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
        if (itemPickUp != null)
        {
            actionText.text = "[ <color=yellow>" + itemPickUp.item.itemName + "</color> ] " + " [ E ] : 아이템 줍기";
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
                    itemPickedText.gameObject.SetActive(true);
                    string message = itemPickUp.item.itemName switch
                    {
                        "Energy Cell" => itemPickedText.text = "[ <color=yellow>" + itemPickUp.item.itemName + "</color> ] 을 획득했다! \n\"엔진을 가동시키려면 2개가 필요해.\" \nF 를 눌러 닫기",
                        "Fuel Tank" => itemPickedText.text = "[ <color=yellow>" + itemPickUp.item.itemName + "</color> ] 을 획득했다! \n\"이제 우주선을 출발시킬 수 있겠어.\" \nF 를 눌러 닫기",
                        "Kit" => itemPickedText.text = "[ <color=yellow>" + itemPickUp.item.itemName + "</color> ] 을 획득했다! \n\"예상치 못한 부상에도 걱정없겠군.\" \nF 를 눌러 닫기",
                        "Map" => itemPickedText.text = "[ <color=yellow>" + itemPickUp.item.itemName + "</color> ] 을 획득했다! \n\"집을 어떻게 찾아가야하나 걱정했는데.\" \nF 를 눌러 닫기"
                    };
                    Debug.Log(itemPickUp.item.itemName + " Picked up.");
                    theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                    Destroy(hitInfo.transform.gameObject);
                }
            }
        }
    }
}
