using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FikretGezer
{
    public interface ICardType
    {
        public ItemType cardType { get; set; }
    }
    public enum ItemType
    {
        gun,
        item
    }
    public enum GunLength
    {
        shortLength,
        mediumLength,
        longLength
    }
}
