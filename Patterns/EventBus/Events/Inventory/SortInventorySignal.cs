
using System;

public class SortInventorySignal
{
    public Type ItemType { get; }

    public SortInventorySignal(Type itemType)
    {
        ItemType = itemType;
    }
}
