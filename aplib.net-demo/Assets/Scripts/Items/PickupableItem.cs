// This program has been developed by students from the bachelor Computer Science at Utrecht
// University within the Software Project course.
// Copyright Utrecht University (Department of Information and Computing Sciences)

using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Attach this class to make object pickupable.
    /// </summary>
    [RequireComponent(typeof(Item))]
    [RequireComponent(typeof(Collider))]
    public class PickupableItem : MonoBehaviour
    {
        /// <summary>
        /// Event that is triggered when the item is picked up.
        /// </summary>
        public event Action<Item> ItemPickedUp;

        private Item _item;
        private Inventory _inventory;

        private void Awake()
        {
            _item = GetComponent<Item>();
            GameObject inventoryObject = GameObject.Find("InventoryObject");
            _inventory = inventoryObject.GetComponent<Inventory>();

            if (!_inventory)
            {
                Debug.LogError("Inventory not found!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !other.material.name.Contains("PlayerPhysic"))
                return;

            // Not all items give points when picked up
            ItemPickedUp?.Invoke(_item);

            Destroy(gameObject);
            _inventory.PickUpItem(_item);
        }
    }
}
