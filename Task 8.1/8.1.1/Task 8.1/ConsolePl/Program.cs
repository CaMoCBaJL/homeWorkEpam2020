﻿using System;
using System.Text;
using System.Reflection;
using BLL;
using Entities;
using System.Collections.Generic;

namespace ConsolePl
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Menu();

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    ChooseTheAction(input);
                }
                else
                    Console.WriteLine("Ошибка ввода!");

            } while (true) ;
        }

        static void Menu()
        {
            Console.WriteLine();

            Console.WriteLine("Выберите действие:");

            Console.WriteLine("1. Просмотреть список пользователей.");

            Console.WriteLine("2. Просмотреть список наград.");

            Console.WriteLine("3. Добавить пользователя.");

            Console.WriteLine("4. Добавить награду.");

            Console.WriteLine("5. Удалить пользователя.");

            Console.WriteLine("6. Удалить награду.");

            Console.WriteLine("7. Выход.");

            Console.WriteLine();
        }

        static void ChooseTheAction(int input)
        {
            switch (input)
            {
                case 1:
                    ShowEntities(EntityType.User);
                    break;
                case 2:
                    ShowEntities(EntityType.Award);
                    break;
                case 3:
                    AddEntityDialogue(EntityType.User);
                    break;
                case 4:
                    AddEntityDialogue(EntityType.Award);
                    break;
                case 5:
                    DeleteEntityDialogue(EntityType.User);
                    break;
                case 6:
                    DeleteEntityDialogue(EntityType.Award);
                    break;
                case 7:
                    Console.WriteLine("Конец работы.");
                    Environment.Exit(123123);
                    break;
                default:
                    Console.WriteLine("Введенный пункт отсутствует в меню");
                    break;
            }
        }

        static void ShowSomeStrings(List<string> stringsToShow)
        {
            int counter = 1;

            foreach (var entity in stringsToShow)
            {
                if (!BuisnessLogic.DoesStringContainsCommonParts(entity))
                {
                    Console.WriteLine(counter + ". " + entity);

                    Console.WriteLine();

                    counter++;
                }

                else
                    Console.WriteLine(entity);
            }
        }

        static void ShowEntities(EntityType entityType)
        {
            ShowSomeStrings(BuisnessLogic.GetListOfEntities(entityType));
        }

        static void AddEntityDialogue(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.User:
                    StringBuilder userInfo = new StringBuilder();
                    
                    Console.WriteLine("Введите имя пользователя:");

                    userInfo.Append(Console.ReadLine() + Environment.NewLine);

                    userInfo.Append(BuisnessLogic.CorrectInputTheParameter("Дата рождения(дд.мм.гг)", BuisnessLogic.birthDateRegexPattern) + Environment.NewLine);

                    userInfo.Append(BuisnessLogic.CorrectInputTheParameter("Возраст", BuisnessLogic.ageRegexPattern));

                    Console.WriteLine("Выберите список наград пользователя:");

                    Console.WriteLine(BuisnessLogic.AddEntity(entityType, userInfo.ToString(), AddAwards()));

                    break;
                case EntityType.Award:
                    Console.WriteLine("Введите название награды:");

                    Console.WriteLine(BuisnessLogic.AddEntity(entityType, Console.ReadLine(), new List<int>()));
                    break;
                case EntityType.None:
                default:
                    return;
            }
        }

        static List<int> AddAwards()
        {
            List<int> result = new List<int>();

            do
            {
                var remainingAwards = BuisnessLogic.GetListOfEntities(EntityType.Award, result);

                ShowSomeStrings(remainingAwards);

                Console.WriteLine("0. Конец.");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                        break;
                    else if (input > 0)
                        result.Add(BuisnessLogic.GetEntityId(remainingAwards[input]));
                    else
                        Console.WriteLine("Выбранный пункт отсутствует в списке.");
                }
                else
                    Console.WriteLine("Ввод неверен!");

            } while (true);

            return result;
        }

        static void DeleteEntityDialogue(EntityType entityType)
        {
            Console.WriteLine($"Выберите {entityType.ToString()} для удаления:");

            if (BuisnessLogic.GetListOfEntities(entityType).Count > 0)
            {
                do
                {
                    ShowEntities(entityType);

                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        Console.WriteLine(BuisnessLogic.RemoveEntity(entityType, result));

                        return;
                    }
                    else
                        Console.WriteLine("Выбранный вариант отсутствует в списке!");

                } while (true);
            }
            else
                Console.WriteLine("В базе отсутствуют " + entityType.ToString());
        }
    }
}
