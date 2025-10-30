var weapons = new Dictionary<string, int>()
{
    {"Палка", 50 },
    {"Дубина", 75 },
    {"Железный меч", 100 },
    {"ПМ", 125 },
    {"Убийца драконов", 200 }
};

var armors = new Dictionary<string, int>()
{
    {"Лохмотья", 20 },
    {"Одежда крестьянина", 25 },
    {"Бронижилет", 40 },
    {"Броня паладина", 60 },
    {"Доспехи берсерка", 85 }
};

class CreateEnemy
{
    public static Monster RandMonster()
    {
        Random random = new Random();
        switch (random.Next(0, 3))
        {
            case 0:
                return new Goblin("Гоблин", 100, 50, 30, 40);
                break;
            case 1:
                return new Skeleton("Скелет", 100, 50, 30);
                break;
            case 2:
                return new Mage("Маг", 100, 50, 30, 40);
                break;

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
                break;
            case 1:
                return new Skeleton("Ковальский", 250, 65, 42);
                break;
            case 2:
                return new Mage("Архимаг C++", 180, 80, 33, 50);
                break;
            case 3:
                return new Mage("Пестов С--", 150, 90, 3, 55);
                break;

            default: return new Goblin("ВВГ", 200, 75, 36, 50);
        }

    }

}

class Hero
{
    private double hp = 250;
    public Dictionary<string, int> weapon; 
    public Dictionary<string, int> armor; 

    public Hero(Dictionary<string, int> weapon, Dictionary<string, int> armor)
    {
        this.weapon = new Dictionary<string, int>(weapon);
        this.armor = new Dictionary<string, int>(armor);
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

    public void PrintInfo()
    {
        Console.WriteLine("Характеристики вашего персонажа:");
        Console.WriteLine($"Здоровье: {hp}");
        Console.WriteLine($"Оружие: {weapon.Keys} (урон: {weapon.Values})");
        Console.WriteLine($"Броня: {armor.Keys} (защита: {armor.Values})");
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