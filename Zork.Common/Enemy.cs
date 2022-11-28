namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string LookDescription { get; }

        public string EnemyLocation { get; }

        public Enemy(string name, string lookDescription, World world, string enemyLocation)
        {
            _world = world;
            Name = name;
            LookDescription = lookDescription;
            EnemyLocation = enemyLocation;
        }

        private readonly World _world;
        private string enemyLocation;
        public override string ToString() => Name;
    }
}
