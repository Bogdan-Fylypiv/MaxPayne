using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

/*
Container class is used for player and enemy inventory
It mainly stored gun bullets.
*/

public class Container : MonoBehaviour
{
    //This inner class represents an item in the inventory
    [Serializable]
    public class ContainerItem
    {
        public Guid id;
        public string name;
        public int maximum;

        public int amountTaken;
        public ContainerItem()
        {
            id = Guid.NewGuid();
        }

        public int Remaining => maximum - amountTaken;

        public int Get(int amount)
        {
            if(amountTaken + amount > maximum){
                int exceded = (amountTaken + amount) - maximum;
                amountTaken = maximum;
                return amount - exceded;
            }

            amountTaken += amount;
            return amount;
       }

        internal void Set(int amount)
        {
            amountTaken = amountTaken - amount < 0 ? 0 : amountTaken - amount;  
        }
    }

        public List<ContainerItem> containerItems;
        public event Action OnContainerReady;

        void Awake()
        {
            containerItems = new List<ContainerItem>();
            OnContainerReady?.Invoke();
        }

        // Add new item to the inventory
        public Guid Add (string name, int maximum)
        {
            Guid id = Guid.NewGuid();
            containerItems.Add(new ContainerItem() { name = name, maximum = maximum, id = id});
            return id;
        }

        // Replenish some resource
        public void Put(string name, int amount)
        {
            var item = containerItems.Where(x => x.name.Equals(name)).FirstOrDefault();
            if (item == null)
                return;
            item.Set(amount);
        }

        public int Take(Guid id, int amount)
        {
            var item = GetContainerItem(id);
            return item == null ? -1 : item.Get(amount);
        }

        public int GetAmountLeft(Guid id)
        {
            var item = GetContainerItem(id);
            return item == null ? -1 : item.Remaining;
        }

        private ContainerItem GetContainerItem(Guid id)
        {
            var item = containerItems.Where(x => x.id == id).FirstOrDefault();
            return item;
        }
}
 