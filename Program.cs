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

class Student : Person
{
    private int studentId;
    private int yearOfStudy;
    private string healthGroup;
    public Student(int id, string fio, DateOnly dateOfBirth, string sex, int yearOfStudy, string healthGroup)
        : base(fio, dateOfBirth, sex)
    {
        studentId = id;
        this.yearOfStudy = yearOfStudy;
        this.healthGroup = healthGroup;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("Student");
        base.PrintInfo();
        Console.WriteLine($"ID студента: {studentId}\nГод обучения: {yearOfStudy}\nГруппа здоровья: {healthGroup}");
    }
}


class Teacher : Person
{
    private int teacherID;
    private int experience;
    private string subjectArea;

    public Teacher(int id, string fio, DateOnly dateOfBirth, string sex, int experience, string subjectArea)
        : base(fio, dateOfBirth, sex)
    {
        teacherID = id;
        this.experience = experience;
        this.subjectArea = subjectArea;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("Teacher");
        base.PrintInfo();
        Console.WriteLine($"ID: {teacherID}\nОпыт работы: {experience}\nПредметная область: {subjectArea}");
    }
}