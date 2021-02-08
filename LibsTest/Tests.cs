using static System.Console;
using Lib;
using System;

namespace LibsTest
{
    static class Tests
    {
        public static void TestLab1()
        {
            WriteLine("Task 1");
            var students = "Дмитренко Олександр - ІП-84; Матвійчук Андрій - ІВ-83; Лесик Сергій - ІО-82; Ткаченко Ярослав - ІВ-83; Аверкова Анастасія - ІО-83; Соловйов Даніїл - ІО-83; Рахуба Вероніка - ІО-81; Кочерук Давид - ІВ-83; Лихацька Юлія- ІВ-82; Головенець Руслан - ІВ-83; Ющенко Андрій - ІО-82; Мінченко Володимир - ІП-83; Мартинюк Назар - ІО-82; Базова Лідія - ІВ-81; Снігурець Олег - ІВ-81; Роман Олександр - ІО-82; Дудка Максим - ІО-81; Кулініч Віталій - ІВ-81; Жуков Михайло - ІП-83; Грабко Михайло - ІВ-81; Іванов Володимир - ІО-81; Востриков Нікіта - ІО-82; Бондаренко Максим - ІВ-83; Скрипченко Володимир - ІВ-82; Кобук Назар - ІО-81; Дровнін Павло - ІВ-83; Тарасенко Юлія - ІО-82; Дрозд Світлана - ІВ-81; Фещенко Кирил - ІО-82; Крамар Віктор - ІО-83; Іванов Дмитро - ІВ-82";
            var studentGroups = Contents.StudentsGroups(students);
            foreach (var group in studentGroups)
            {
                WriteLine(group.Key + " : " + string.Join(", ", group.Value));
            }
            WriteLine();
            WriteLine("Task 2");

            var points = new int[] { 12, 12, 12, 12, 12, 12, 12, 16 };

            var studentPoints = Contents.StudentsPoints(points, students);

            foreach (var group in studentPoints)
            {
                WriteLine(group.Key + " : ");
                foreach (var student in group.Value)
                {
                    WriteLine(student.Key + "[" + string.Join(", ", student.Value) + "]");
                }
            }

            WriteLine();
            WriteLine("Task 3");

            var sumPoints = Contents.SumPoints(studentPoints);

            foreach (var group in sumPoints)
            {
                WriteLine(group.Key + " : ");
                foreach (var student in group.Value)
                {
                    WriteLine(student.Key + " " + student.Value);
                }
            }

            WriteLine();
            WriteLine("Task 4");

            var groupAvg = Contents.GroupAvg(sumPoints);

            foreach (var group in groupAvg)
            {
                WriteLine(group.Key + " " + group.Value);
            }

            WriteLine();
            WriteLine("Task 5");

            var passedPerGroup = Contents.PassedPerGroup(sumPoints);

            foreach (var group in passedPerGroup)
            {
                WriteLine(group.Key + " : [ " + string.Join(", ", group.Value) + " ]");
            }
        }

        public static void TestTimeTI()
        {
            var zero = new TimeTI();

            WriteLine("Zero : " + zero.ToString());

            var manual = new TimeTI(14, 33, 42);

            WriteLine("Manual : " + manual.ToString());

            try
            {
                var exc = new TimeTI(99, 99, 99);
            }
            catch
            {
                WriteLine("Catch error when trying to create invalid object TimeTI(99, 99, 99)");
            }

            var fromDataTime = new TimeTI(new DateTime(2020, 8, 21, 12, 54, 23));

            WriteLine("From DataTime : " + fromDataTime.ToString());

            var first = new TimeTI(18, 21, 12);
            var second = new TimeTI(15, 12, 59);

            var sum = TimeTI.Sum(
                first,
                second
                );
            WriteLine("Sum : " + sum.ToString());

            first.Add(second);

            WriteLine("Sum method : " + first.ToString());

            first = new TimeTI(18, 21, 12);
            second = new TimeTI(19, 12, 19);

            var diff = TimeTI.Diff(
                first,
                second
                );

            WriteLine("Sub : " + diff.ToString());

            first.Sub(second);

            WriteLine("Sub method : " + first.ToString());

        }
    }
}
