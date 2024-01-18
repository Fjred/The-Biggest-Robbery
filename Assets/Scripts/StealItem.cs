using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealItem : MonoBehaviour
{
    public float distance = 2;

    // List of Image components where stolen items will be placed
    public List<Image> itemSlots = new List<Image>();

    public ParticleSystem stealItem;

    private List<Sprite> stolenItemSprites = new List<Sprite>();

    public AudioSource grabItem;

    public bool hasCard0;
    public bool hasCard1;
    public bool hasCard2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Steal();
        }
    }

    public void Steal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Item"))
            {
                // Destroy the GameObject that was hit
                Destroy(hit.collider.gameObject);

                // Get the Item script from the stolen item
                Item itemScript = hit.collider.GetComponent<Item>();

                if (itemScript != null && itemScript.image != null)
                {
                    // Add the stolen item's sprite to the list
                    stolenItemSprites.Add(itemScript.image);

                    // Check if there are available slots to place the stolen item
                    if (stolenItemSprites.Count <= itemSlots.Count)
                    {
                        grabItem.Play();

                        itemSlots[stolenItemSprites.Count - 1].sprite = itemScript.image;

                        ParticleSystem particleSystemInstance = Instantiate(stealItem, hit.point, Quaternion.Euler(-90f, 0f, 0f));

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
        }
    }
}
