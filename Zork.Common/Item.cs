namespace Zork.Common
{
    public class Item
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string InventoryDescription { get; }

        public string Type { get; }

        public int Damage { get; }

        public Item(string name, string lookDescription, string inventoryDescription, string type, int damage)
        {
            Name = name;
            LookDescription = lookDescription;
            InventoryDescription = inventoryDescription;
            Type = type;
            Damage = damage;
        }

        public override string ToString() => Name;
    }
}