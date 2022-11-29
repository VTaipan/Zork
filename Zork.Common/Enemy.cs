using System;

namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string LookDescription { get; set; }

        public int EnemyHP { get; set; }

        public string EnemyState { get; set; }

        public Enemy(string name, string lookDescription, int enemyHP, string enemyState)
        {
            Name = name;
            LookDescription = lookDescription;
            EnemyHP = enemyHP;
            EnemyState = enemyState;
        }

        public override string ToString() => Name;
    }
}
