using System;

namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string LookDescription { get; }

        public int EnemyHP { get; }

        public Enemy(string name, string lookDescription, int enemyHP)
        {
            Name = name;
            LookDescription = lookDescription;
            EnemyHP = enemyHP;
        }

        public override string ToString() => Name;
    }
}
