using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class FunnelBehavior : MonoBehaviour
    {
        [SerializeField] private BloodBankManager bloodManager;
        [SerializeField] private int cultistBloodValue = 10;

        private void OnEnable()
        {
            if (bloodManager == null)
            {
                bloodManager = FindObjectOfType<BloodBankManager>();
            }
        }

        void AddCultistBlood(Cultist _cultist)
        {
            bloodManager.IncreaseBloodOfType(_cultist.blood, cultistBloodValue);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<CultistBehavior>())
            {
                Cultist current = collision.gameObject.GetComponent<CultistBehavior>().cultist;
                if (bloodManager.CanIncrease(current.blood, cultistBloodValue))
                {
                    AddCultistBlood(current);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}

