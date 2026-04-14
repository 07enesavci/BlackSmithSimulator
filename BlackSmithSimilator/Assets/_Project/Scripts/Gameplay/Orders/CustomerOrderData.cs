using System;

namespace BlacksmithSimulator.Gameplay.Orders
{
    [Serializable]
    public sealed class CustomerOrderData
    {
        public string OrderId;
        public OrderType OrderType;
        public string RequestedItemId;
        public string DetailsText;
    }
}
