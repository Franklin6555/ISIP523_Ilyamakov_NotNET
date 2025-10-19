class Person
{
    private string fio;
    private DateOnly dateOfBirth;
    private string sex;

    public Person(string fio, DateOnly dateOfBirth, string sex)
    {
        this.fio = fio;
        this.dateOfBirth = dateOfBirth;
        this.sex = sex;
    }
    public virtual void PrintInfo()
    {
        Console.WriteLine($"ФИО: {fio}\nДата рождения: {dateOfBirth}\nПол: {sex} ");
    }
}