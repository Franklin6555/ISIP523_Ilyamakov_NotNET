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