using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;

Dictionary<string, int> allWeapons = new Dictionary<string, int>()
{
    {"Палка", 50 },
    {"Дубина", 75 },
    {"Железный меч", 100 },
    {"ПМ", 125 },
    {"Убийца драконов", 200 }
};

Dictionary<string, int> allArmors = new Dictionary<string, int>()
{
    {"Лохмотья", 20 },
    {"Одежда крестьянина", 25 },
    {"Бронижилет", 40 },
    {"Броня паладина", 60 },
    {"Доспехи берсерка", 85 }
};

class Game
{
    public Hero player = new Hero("Палка", "Лохмотья");
    public Random random = new Random();
    public Dictionary<string, int> weapons { get; private set; }
    public Dictionary<string, int> armors { get; private set; }
    public int move { get; private set; }

    public Game(Dictionary<string, int> weapons, Dictionary<string, int> armors)
    {
        this.weapons = weapons;
        this.armors = armors;
    }

    public void Start_game()
    {
        Console.WriteLine("Здравствуй игрок, вот твой герой:");
        player.PrintInfo(weapons, armors);
        move = 0;

        while (player.hp > 0)
        {
            move++;
            int currentMove = random.Next(1, 3);

            switch (currentMove)
            {
                case 1:
                    Console.WriteLine("Вы нашли сундук!");
                    int loot = random.Next(1, 4);
                    switch (loot)
                    {
                        case 1:
                            Console.WriteLine("Вы нашли зелье лечения!");
                            player.Heal();
                            break;
                        case 2:
                            string randomWeapon = weapons.Keys.ElementAt(random.Next(weapons.Count));
                            Console.WriteLine($"Вы нашли: {randomWeapon}\n(урон: {weapons[randomWeapon]})");
                            Console.WriteLine("Хотите поменять с вашим? (1 - Да; 2 - Нет)");
                            Console.WriteLine($"У вас: {player.currentWeapon}\n(урон: {weapons[player.currentWeapon]})");
                            int choiceW = Convert.ToInt32(Console.ReadLine());
                            switch (choiceW)
                            {
                                case 1:
                                    player.currentWeapon = randomWeapon;
                                    break;
                                case 2:
                                    break;
                            }
                            break;
                        case 3:
                            string randomArmor = armors.Keys.ElementAt(random.Next(armors.Count));
                            Console.WriteLine($"Вы нашли: {randomArmor}\n(защита: {armors[randomArmor]})");
                            Console.WriteLine("Хотите поменять с вашим? (1 - Да; 2 - Нет)");
                            Console.WriteLine($"У вас: {player.currentArmor}\n(урон: {armors[player.currentArmor]})");
                            int choiceA = Convert.ToInt32(Console.ReadLine());
                            switch (choiceA)
                            {
                                case 1:
                                    player.currentArmor = randomArmor;
                                    break;
                                case 2:
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}

class Hero
{
    public double hp { get; private set; }
    public string currentWeapon; 
    public string currentArmor; 

    public Hero(string weapon, string armor)
    {
        hp = 250;
        currentWeapon = weapon;
        currentArmor = armor;
    }

    public void Heal()
    {
        hp = 250;
        Console.WriteLine("Вы востановили все HP!");
    }

    public void GetDamage(double damage)
    {
        hp -= damage;
        Console.WriteLine($"Вы получили {damage} урона\nТеперь у вас {hp} HP");
    }

    public void PrintInfo(Dictionary<string, int> armors, Dictionary<string, int> weapons)
    {
        Console.WriteLine("Характеристики вашего персонажа:");
        Console.WriteLine($"Здоровье: {hp}");
        Console.WriteLine($"Оружие: {currentWeapon} (урон: {weapons[currentWeapon]})");
        Console.WriteLine($"Броня: {currentArmor} (защита: {armors[currentArmor]})");
    }
}

class Monster
{
    public string name;
    public double hp = 100;
    public int damage = 50;
    public int defence = 30;

    public Monster(string name, int hp, int damage, int defence)
    {
        this.name = name;
        this.hp = hp;
        this.damage = damage;
        this.defence = defence;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Характеристики {name}: ");
        Console.WriteLine($"Здоровье: {hp}");
        Console.WriteLine($"Урон: {damage}");
        Console.WriteLine($"Броня: {defence}");
    }
}

class Goblin : Monster
{
    public int chanceKrit = 40;

    public Goblin(string name, int hp, int damage, int defence, int chanceKrit)
        : base(name, hp, damage, defence)
    {
        this.chanceKrit = chanceKrit;
    }
}

class Skeleton : Monster
{
    public Skeleton(string name, int hp, int damage, int defence)
        : base(name, hp, damage, defence)
    {

    }
}

class Mage : Monster
{
    public int freez = 40;

    public Mage(string name, int hp, int damage, int defence, int freez)
        : base(name, hp, damage, defence)
    {
        this.freez = freez;
    }
}

class CreateEnemy
{
    public static Monster RandMonster()
    {
        Random random = new Random();
        switch (random.Next(0, 3))
        {
            case 0:
                return new Goblin("Гоблин", 100, 50, 30, 40);
            case 1:
                return new Skeleton("Скелет", 100, 50, 30);
            case 2:
                return new Mage("Маг", 100, 50, 30, 40);

            default: return new Goblin("Гоблин", 100, 50, 30, 40);
        }

    }

    public static Monster RandBoss()
    {
        Random random = new Random();
        switch (random.Next(0, 4))
        {
            case 0:
                return new Goblin("ВВГ", 200, 75, 36, 50);
            case 1:
                return new Skeleton("Ковальский", 250, 65, 42);
            case 2:
                return new Mage("Архимаг C++", 180, 80, 33, 50);
            case 3:
                return new Mage("Пестов С--", 150, 90, 3, 55);

            default: return new Goblin("ВВГ", 200, 75, 36, 50);
        }

    }

}