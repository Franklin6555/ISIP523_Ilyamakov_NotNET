using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;



Dictionary<string, int> allWeapons = new Dictionary<string, int>()
{
    {"Палка", 50 },
    {"Дубина", 75 },
    {"Железный меч", 100 },
    {"ПМ", 125 },
    {"Убийца драконов", 170 }
};

Dictionary<string, double> allArmors = new Dictionary<string, double>()
{
    {"Лохмотья", 0.2 },
    {"Одежда крестьянина", 0.25 },
    {"Бронижилет", 0.4 },
    {"Броня паладина", 0.6 },
    {"Доспехи берсерка", 0.85 }
};

Game GAME = new(allWeapons, allArmors);
GAME.Start_game();

class Game
{
    public Hero Player = new("Палка", "Лохмотья");
    public Random Rnd = new();
    public Dictionary<string, int> Weapons { get; private set; }
    public Dictionary<string, double> Armors { get; private set; }
    public int Move { get; private set; }

    public Game(Dictionary<string, int> weapons, Dictionary<string, double> armors)
    {
        Weapons = weapons;
        Armors = armors;
    }

    public void Start_game()
    {
        Console.WriteLine("Здравствуй игрок, вот твой герой:");
        Player.PrintInfo(Armors, Weapons);
        Move = 0;

        while (Player.Hp > 0)
        {
            Move++;
            int currentMove = Rnd.Next(1, 3);

            switch (currentMove)
            {
                case 1:
                    Console.WriteLine("Вы нашли сундук!");
                    int loot = Rnd.Next(1, 4);
                    switch (loot)
                    {
                        case 1:
                            Console.WriteLine("Вы нашли зелье лечения!");
                            Player.Heal();
                            break;
                        case 2:
                            string randomWeapon = Weapons.Keys.ElementAt(Rnd.Next(Weapons.Count));
                            Console.WriteLine($"Вы нашли: {randomWeapon}\n(урон: {Weapons[randomWeapon]})");
                            Console.WriteLine("Хотите поменять с вашим? (1 - Да; 2 - Нет)");
                            Console.WriteLine($"У вас: {Player.CurrentWeapon}\n(урон: {Weapons[Player.CurrentWeapon]})");
                            int choiceW = Convert.ToInt32(Console.ReadLine());
                            switch (choiceW)
                            {
                                case 1:
                                    Player.CurrentWeapon = randomWeapon;
                                    break;
                                case 2:
                                    break;
                            }
                            break;
                        case 3:
                            string randomArmor = Armors.Keys.ElementAt(Rnd.Next(Armors.Count));
                            Console.WriteLine($"Вы нашли: {randomArmor}\n(защита: {Armors[randomArmor]})");
                            Console.WriteLine("Хотите поменять с вашим? (1 - Да; 2 - Нет)");
                            Console.WriteLine($"У вас: {Player.CurrentArmor}\n(защита: {Armors[Player.CurrentArmor]})");
                            int choiceA = Convert.ToInt32(Console.ReadLine());
                            switch (choiceA)
                            {
                                case 1:
                                    Player.CurrentArmor = randomArmor;
                                    break;
                                case 2:
                                    break;
                            }
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("Вы встретили мостра!");
                    bool frost = false;
                    bool dodge = false;
                    Monster monster = CreateEnemy.RandMonster();
                    monster.PrintInfo();
                    while (Player.Hp > 0 && monster.Hp > 0)
                    {
                        Console.WriteLine("Ваш ход:");
                        if (frost == true)
                        {
                            frost = false;
                            Console.WriteLine("ЗАМОРОЖЕНЫ");
                        }
                        else
                        {
                            Console.WriteLine("Выберите дейсвие:\n1. Атака\n2. Защита");
                            int choseF = Convert.ToInt32(Console.ReadLine());
                            switch (choseF)
                            {
                                case 1:
                                    double damage = Weapons[Player.CurrentWeapon] * (1 - monster.Defence);
                                    monster.GetDamage(damage);
                                    Console.WriteLine($"Вы нанесли {damage} урона");
                                    if (monster.Hp <= 0)
                                    {
                                        Console.WriteLine("Вы победили!");
                                        break;
                                    }
                                    Console.WriteLine($"У {monster.Name} теперь {monster.Hp} ХП");
                                    break;
                                case 2:
                                    dodge = Rnd.Next(0, 10) <= 4 ? true : false;
                                    break;
                            }
                        }

                        Console.WriteLine("ХОД МОНСТРА");
                        if (dodge)
                        {
                            Console.WriteLine("Вы увернулись от атаки!");
                            dodge = false;
                        }
                        else Player.GetDamage(monster.Attack(Armors[Player.CurrentArmor]));
                        if (monster.Freez != 0 && Rnd.Next(0, 100) <= monster.Freez)
                        {
                            frost = true;
                            Console.WriteLine("ЗАМОРОЗКА");
                        }
                    }
                    break;
            }
        }
    }
}

class Hero
{
    public double Hp { get; private set; }
    public string CurrentWeapon; 
    public string CurrentArmor; 

    public Hero(string weapon, string armor)
    {
        Hp = 250;
        CurrentWeapon = weapon;
        CurrentArmor = armor;
    }

    public void Heal()
    {
        Hp = 250;
        Console.WriteLine("Вы востановили все HP!");
    }

    public void GetDamage(double damage)
    {
        Hp -= damage;
        Console.WriteLine($"Вы получили {damage} урона\nТеперь у вас {Hp} HP");
    }

    public void PrintInfo(Dictionary<string, double> armors, Dictionary<string, int> weapons)
    {
        Console.WriteLine("Характеристики вашего персонажа:");
        Console.WriteLine($"Здоровье: {Hp}");
        Console.WriteLine($"Оружие: {CurrentWeapon} (урон: {weapons[CurrentWeapon]})");
        Console.WriteLine($"Броня: {CurrentArmor} (защита: {armors[CurrentArmor]})");
    }
}

class Monster
{
    public string Name;
    public double Hp { get; private set; }
    public int Damage;
    public double Defence;
    public int Freez;

    public Monster(string name, int hp, int damage, double defence, int freez)
    {
        Name = name;
        Hp = hp;
        Damage = damage;
        Defence = defence;
        Freez = freez;
    }
    public virtual void GetDamage(double damage)
    {
        Hp -= damage;
    }
    public virtual double Attack(double playerDefence)
    {
        return Damage * (1 - playerDefence);
    }
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Характеристики {Name}: ");
        Console.WriteLine($"Здоровье: {Hp}");
        Console.WriteLine($"Урон: {Damage}");
        Console.WriteLine($"Защита: {Defence}");
    }
}

class Goblin : Monster
{
    public int Crit = 15;

    public Goblin(string name, int hp, int damage, double defence, int crit, int freez)
        : base(name, hp, damage, defence, freez) => Crit = crit;
    public override void GetDamage(double damage)
    {
        base.GetDamage(damage);
    }
    public override double Attack(double playerDefence)
    {
        Random rnd = new();
        double dam = base.Attack(playerDefence);
        bool isCrit = rnd.Next(0, 100) <= Crit;
        if (isCrit)
        {
            Console.WriteLine("Критический урон!");
            return dam * (1 + Crit); 
        }
        else return dam;
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
    }
}

class Skeleton : Monster
{
    public Skeleton(string name, int hp, int damage, double defence, int freez)
        : base(name, hp, damage, defence, freez) { }
    public override void GetDamage(double damage)
    {
        base.GetDamage(damage);
    }
    public override double Attack(double playerDefence)
    {
        Console.WriteLine("Скелет игнорирует твою броню!");
        return base.Attack(0);
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
    }
}

class Mage : Monster
{
    public Mage(string name, int hp, int damage, double defence, int freez)
        : base(name, hp, damage, defence, freez) { };
    public override void GetDamage(double damage)
    {
        base.GetDamage(damage);
    }
    public override double Attack(double playerDefence)
    {
        Console.WriteLine("Атака игнорирует вашу броню");
        return base.Attack(playerDefence);
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
    }
}

class CreateEnemy
{
    public static Monster RandMonster()
    {
        Random Rnd = new();
        return Rnd.Next(0, 3) switch
        {
            0 => new Goblin("Гоблин", 100, 50, 0.3, 40, 0),
            1 => new Skeleton("Скелет", 100, 50, 0.3, 0),
            2 => new Mage("Маг", 100, 50, 0.3, 40),
            _ => new Goblin("Гоблин", 100, 50, 0.3, 40, 0),
        };
    }

    public static Monster RandBoss()
    {
        Random Rnd = new();
        return Rnd.Next(0, 4) switch
        {
            0 => new Goblin("ВВГ", 200, 75, 0.36, 50, 0),
            1 => new Skeleton("Ковальский", 250, 65, 0.42, 0),
            2 => new Mage("Архимаг C++", 180, 80, 0.33, 50),
            3 => new Mage("Пестов С--", 150, 90, 0.3, 55),
            _ => new Goblin("ВВГ", 200, 75, 0.36, 50, 0),
        };
    }

}