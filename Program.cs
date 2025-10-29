
using static System.Reflection.Metadata.BlobBuilder;

List <Student> students = new List<Student>();
List <Teacher> teachers = new List<Teacher>();
List <Course> courses = new List<Course>();

int studentGId = 0;
int teacherGId = 0;
int courseGId = 0;

bool inMenu = true;

while (inMenu)
{
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("Выберите пункт меню:");
    Console.WriteLine("1. Добавить студента");
    Console.WriteLine("2. Удалить студента по ID");
    Console.WriteLine("3. Вывести информацию о всех судентах");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("4. Добавить преподавателя");
    Console.WriteLine("5. Удалить преподавателя по ID");
    Console.WriteLine("6. Вывести информацию о всех преподавателях");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("7. Добавить курс");
    Console.WriteLine("8. Удалить курс по ID");
    Console.WriteLine("9. Вывести информацию о всех курсах");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("10. Записать студента на курс");
    Console.WriteLine("11. Удалить студента с курса");
    Console.WriteLine("12. Поменять преподователя на курсе");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("0. Выход");
    Console.WriteLine("-------------------------------------------");
    int choice = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("-------------------------------------------");
}

// Классы
class Course
{
    public int id { get; private set; }
    private string title;
    private string description;
    private int teacherId;
    public List<int> studentId;

    public Course(int id, string title, string description, int teacherId, List<int> studentId)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.teacherId = teacherId;
        this.studentId = studentId;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Название: {title}\nОписание: {description}\nID преподаввателя: {teacherId}");
        Console.WriteLine("Id студентов, записанных на курс:");
        foreach (var sId in studentId)
        {
            Console.WriteLine(sId);
        }
    }
}

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
        Console.WriteLine($"ФИО: {fio}\nДата рождения: {dateOfBirth}\nПол: {sex}");
    }
}

class Student : Person
{
    public int id { get; private set; }
    private int yearOfStudy;
    private string healthGroup;
    public List<Course> courses;
    public Student(int id, string fio, DateOnly dateOfBirth, string sex, int yearOfStudy, string healthGroup, List<Course> courses)
        : base(fio, dateOfBirth, sex)
    {
        this.id = id;
        this.yearOfStudy = yearOfStudy;
        this.healthGroup = healthGroup;
        this.courses = courses;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("Student");
        base.PrintInfo();
        Console.WriteLine($"ID студента: {id}\nГод обучения: {yearOfStudy}\nГруппа здоровья: {healthGroup}");
    }
}


class Teacher : Person
{
    public int id { get; private set; }
    private int experience;
    private string subjectArea;
    public List<Course> courses;
    public Teacher(int id, string fio, DateOnly dateOfBirth, string sex, int experience, string subjectArea, List<Course> courses)
        : base(fio, dateOfBirth, sex)
    {
        this.id = id;
        this.experience = experience;
        this.subjectArea = subjectArea;
        this.courses = courses;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("Teacher");
        base.PrintInfo();
        Console.WriteLine($"ID: {id}\nОпыт работы: {experience}\nПредметная область: {subjectArea}");
    }
}

