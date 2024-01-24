using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealItem : MonoBehaviour
{
    public float distance = 2;

    public List<Image> itemSlots = new List<Image>();

    public ParticleSystem stealItem;

    private List<Sprite> stolenItemSprites = new List<Sprite>();

    public AudioSource grabItem;

    public bool StealedItem = false;

    public bool hasCard0;
    public bool hasCard1;
    public bool hasCard2;

    public GunfireController RPG;

    public int money;

    public Transform target;

    public GameObject AK47;

    public GameObject ammoText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Steal();
        }        
        if (Input.GetKeyDown(KeyCode.O))
        {
            AK47.SetActive(true);
            ammoText.SetActive(true);
        }
    }

    public void EndGame()
    {
        transform.position = target.position;
    }
    public void Steal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                Destroy(hit.collider.gameObject);

                Item itemScript = hit.collider.GetComponent<Item>();

                if (itemScript != null && itemScript.image != null)
                {
                    stolenItemSprites.Add(itemScript.image);

                    if (stolenItemSprites.Count <= itemSlots.Count)
                    {
                        StealedItem = true;

                        grabItem.Play();

                        itemSlots[stolenItemSprites.Count - 1].sprite = itemScript.image;

                        ParticleSystem particleSystemInstance = Instantiate(stealItem, hit.point, Quaternion.Euler(-90f, 0f, 0f));

                        var moneyAdded = Random.Range(10000, 50000);

                        money += moneyAdded;

                    }
                    else
                    {
                        Debug.LogWarning("No available slots for the stolen item.");
                    }
                }
            }

            if (hit.collider.CompareTag("Card"))
            {
                Destroy(hit.collider.gameObject);

                Card itemScript = hit.collider.GetComponent<Card>();

                if (itemScript != null)
                {
                    switch (itemScript.cardLevel)
                    {
                        case 0:
                            hasCard0 = true;
                            break;
                        case 1:
                            hasCard1 = true;
                            break;

                        case 2:
                            hasCard2 = true;
                            break;
                        default:
                            break;
                    }

                }
            }

            if (hit.collider.CompareTag("RPG") && RPG.rocketActivated == false)
            {
                RPG.FireWeapon();
            }
        }
    }
}
