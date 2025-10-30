
using System.Collections.Generic;
using System.Security.Cryptography;
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
    Console.WriteLine("2. Вывести информацию о всех судентах");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("3. Добавить преподавателя");
    Console.WriteLine("4. Вывести информацию о всех преподавателях");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("5. Добавить курс");
    Console.WriteLine("6. Вывести информацию о всех курсах");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("7. Записать студента на курс");
    Console.WriteLine("8. Узнать, на какие курсы записан студент");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine("0. Выход");
    Console.WriteLine("-------------------------------------------");
    int choice = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("-------------------------------------------");

    switch(choice)
    {
        case 1:
            bool tas = AddSudent(ref students, ref studentGId);
            if (tas) Console.WriteLine("Успешно");
            else Console.WriteLine("Попробуйте снова");
            break;
        case 2:
            for (int i = 0; i < students.Count; i++)
            {
                students[i].PrintInfo();
            }
            break;
        case 3:
            bool tat = AddTeacher(ref teachers, ref teacherGId);
            if (tat) Console.WriteLine("Успешно");
            else Console.WriteLine("Попробуйте снова");
            break;
        case 4:
            for (int i = 0; i < teachers.Count; i++)
            {
                teachers[i].PrintInfo();
            }
            break;
        case 5:
            bool tac = AddCourse(ref courses, ref courseGId, teachers, students);
            if (tac) Console.WriteLine("Успешно");
            else Console.WriteLine("Попробуйте снова");
            break;
        case 6:
            for (int i = 0; i < courses.Count; i++)
            {
                courses[i].PrintInfo();
            }
            break;
        case 7:
            Console.WriteLine("Введите ID курса");
            int cId = Convert.ToInt32(Console.ReadLine());
            Course course;
            try
            {
                course = courses.Find(c => c.id == cId);
            }
            catch
            {
                Console.WriteLine("Нет курса с таким ID");
                break;
            }
            bool trs = RegisterStudent(ref course, students);
            if (trs) Console.WriteLine("Успешно");
            else Console.WriteLine("Попробуйте снова");
            break;
        case 8:
            bool tcsc = ChekSutentCourses(students, courses);
            if (tcsc) Console.WriteLine("Успешно");
            else Console.WriteLine("Попробуйте снова");
            break;
    }

}

static bool AddSudent(ref List<Student> students, ref int gId)
{
    Console.WriteLine("Введите ФИО:");
    string fio = Console.ReadLine();
    if (fio == null)
    {
        Console.WriteLine("ФИО не может быть пустым");
        return false;
    }
    Console.WriteLine("Введите Дату рождения (формат ГГГГ,ММ,ДД)");
    DateOnly date;
    bool isDate = DateOnly.TryParse(Console.ReadLine(), out date);
    if (isDate == false)
    {
        Console.WriteLine("Не верный формат даты либо попытка внести пустое значение");
        return false;
    }
    Console.WriteLine("Введите пол:");
    string s = Console.ReadLine();
    if (s == null)
    {
        Console.WriteLine("Пол не может быть пустым");
        return false;
    }
    Console.WriteLine("Введите курс обучения");
    string strys = Console.ReadLine();
    if (strys == null)
    {
        Console.WriteLine("Курс обучения не может быть пустым");
        return false;
    }
    int ys = Convert.ToInt32(strys);
    Console.WriteLine("Введите группу здоровья: ");
    string hg = Console.ReadLine();
    if (hg == null)
    {
        Console.WriteLine("Группа здоровья не может быть пустой ");
        return false;
    }
    students.Add(new Student(gId++, fio, date, s, ys, hg));
    return true;
}

static bool AddTeacher(ref List<Teacher> teachers, ref int gId)
{
    Console.WriteLine("Введите ФИО:");
    string fio = Console.ReadLine();
    if (fio == null)
    {
        Console.WriteLine("ФИО не может быть пустым");
        return false;
    }
    Console.WriteLine("Введите Дату рождения (формат ГГГГ,ММ,ДД)");
    DateOnly date;
    bool isDate = DateOnly.TryParse(Console.ReadLine(), out date);
    if (isDate == false)
    {
        Console.WriteLine("Не верный формат даты либо попытка внести пустое значение");
        return false;
    }
    Console.WriteLine("Введите пол:");
    string s = Console.ReadLine();
    if (s == null)
    {
        Console.WriteLine("Пол не может быть пустым");
        return false;
    }
    Console.WriteLine("Введите опыт работы (кол-во полных лет)");
    string x = Console.ReadLine();
    if (x == null)
    {
        Console.WriteLine("Опыт работы не может быть пустым");
        return false;
    }
    int xp = Convert.ToInt32(x);
    Console.WriteLine("Введите предметную область: ");
    string sa = Console.ReadLine();
    if (sa == null)
    {
        Console.WriteLine("Предметная область не может быть пустой ");
        return false;
    }
    teachers.Add(new Teacher(gId++, fio, date, s, xp, sa));
    return true;
}

static bool AddCourse(ref List<Course> course, ref int gId, List<Teacher> teachers, List<Student> students)
{
    Console.WriteLine("Введите название курса:");
    string n = Console.ReadLine();
    if (n == null)
    {
        Console.WriteLine("Название не может быть пустым");
        return false;
    }

    Console.WriteLine("Введите описание курса:");
    string d = Console.ReadLine();
    if (d == null)
    {
        Console.WriteLine("Описание не может быть пустым");
        return false;
    }

    Console.WriteLine("Введите ID преподавателя курса:");
    string tIdStr = Console.ReadLine();
    if (tIdStr == null)
    {
        Console.WriteLine("ID преподавателя не может быть пустым");
        return false;
    }
    int tId = Convert.ToInt32(tIdStr);
    try
    {
        teachers.Find(t => t.id == tId);
    }
    catch
    {
        Console.WriteLine("Нет преподавателя с таким ID");
        return false;
    }

    Console.WriteLine("Введите ID студентов курса через Enter. Когда закончите введите 0:");
    List<int> studentsOnCourse = new List<int>();
    bool enterStudents = true;
    while (enterStudents)
    {
        int sId = Convert.ToInt32(Console.ReadLine());
        try
        {
            students.Find(s => s.id == sId);
            studentsOnCourse.Add(sId);
        }
        catch
        {
            Console.WriteLine("Нет студента с таким ID");
        }
        if (sId == 0)
        {
            course.Add(new Course(gId, n, d, tId, studentsOnCourse));
            enterStudents = false;
        }
    }
    return true;
}

static bool RegisterStudent(ref Course course, List<Student> students)
{
    Console.WriteLine("Введите ID студента");
    int sId = Convert.ToInt32(Console.ReadLine());
    try
    {
        students.Find(s => s.id == sId);
        course.Register(sId);
    }
    catch
    {
        Console.WriteLine("Нет студента с таким ID");
        return false;
    }
    course.Register(sId);
    return true;
}

static bool ChekSutentCourses(List<Student> students, List<Course> courses)
{
    Console.WriteLine("Введите ID студента");
    int sId = Convert.ToInt32(Console.ReadLine());
    try
    {
        students.Find(s => s.id == sId);
    }
    catch
    {
        Console.WriteLine("Нет студента с таким ID");
        return false;
    }
    for (int x = 0; x < courses.Count; x++)
    {
        if (courses[x].studentId.Contains(sId))
        {
            Console.WriteLine(courses[x].name);
            Console.WriteLine(courses[x].description);
        }
    }
    return true;
}

// Классы
class Course
{
    public int id { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    private int teacherId;
    public List<int> studentId { get; private set; }

    public Course(int id, string name, string description, int teacherId, List<int> studentId)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.teacherId = teacherId;
        this.studentId = studentId;
    }

    public void Register(int sId)
    {
        studentId.Add(sId);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Название: {name}\nОписание: {description}\nID преподаввателя: {teacherId}");
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
    public Student(int id, string fio, DateOnly dateOfBirth, string sex, int yearOfStudy, string healthGroup)
        : base(fio, dateOfBirth, sex)
    {
        this.id = id;
        this.yearOfStudy = yearOfStudy;
        this.healthGroup = healthGroup;
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
    public Teacher(int id, string fio, DateOnly dateOfBirth, string sex, int experience, string subjectArea)
        : base(fio, dateOfBirth, sex)
    {
        this.id = id;
        this.experience = experience;
        this.subjectArea = subjectArea;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("Teacher");
        base.PrintInfo();
        Console.WriteLine($"ID: {id}\nОпыт работы: {experience}\nПредметная область: {subjectArea}");
    }
}

