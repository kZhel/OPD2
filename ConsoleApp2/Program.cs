using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.Migrate();

                while (true)
                {
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1 – Добавиль планету");
                    Console.WriteLine("2 – Посмотреть список планет");
                    Console.WriteLine("3 – Изменить данные о планете");
                    Console.WriteLine("4 - Удалить планету");
                    Console.WriteLine("0 - Выход из программы");
                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                    {
                        case '1': AddPlanet(); break;
                        case '2': ViewPlanets(); break;
                        case '3': UpdatePlanetInfo(); break;
                        case '4': DeletePlanet(); break;
                        case '0': return;
                        default: break;
                    }

                }
                void AddPlanet()
                {

                    string? name;
                    int numSatellite;
                    string? atmosphere;

                    Console.WriteLine("");
                    Console.WriteLine("Введите название планеты:");
                    name = Console.ReadLine();

                    Console.WriteLine("");
                    Console.WriteLine("Введите число спутников:");
                    string num = Console.ReadLine();
                    if (num == "")
                    {
                        numSatellite = 0;
                    }
                    else
                    {
                        numSatellite=Convert.ToInt32(num);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Введите состав атмосферы:");
                    atmosphere = Console.ReadLine();

                    Planet temp = new Planet { Name = name, NumSatellite = numSatellite, Atmosphere = atmosphere };
                    db.Planets.Add(temp);
                    db.SaveChanges();
                    Console.WriteLine("Планета успешно добавлена");

                    Console.WriteLine("");
                }
                void ViewPlanets()
                {
                    var planets = db.Planets.ToList();
                    Console.WriteLine("");
                    Console.WriteLine("Список планет:");
                    foreach (Planet p in planets)
                    {
                        Console.WriteLine($"{p.Id}.{p.Name} - Атмосфера: {p.Atmosphere}; Количество спутников: {p.NumSatellite}");
                    }

                    Console.WriteLine("");

                }
                void DeletePlanet()
                {

                    ViewPlanets();
                    Console.WriteLine("");
                    Console.WriteLine("Выберите ID планеты, которую хотите удалить:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Planet? planet = db.Planets.Where(p => p.Id == id).FirstOrDefault();
                    if (planet != null)
                    {
                        db.Planets.Remove(planet);
                        db.SaveChanges();
                        Console.WriteLine("Планета успешно удалена");
                    }
                    else
                    {
                        Console.WriteLine("Такого ID нет в списке");
                    }

                    Console.WriteLine("");
                    
                }
                void UpdatePlanetInfo()
                {


                    Console.WriteLine("");
                    Console.WriteLine("Выберите ID планеты, которую хотите изменить:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Planet? planet = db.Planets.Where(p => p.Id == id).FirstOrDefault();
                    if (planet == null)
                    {
                        Console.WriteLine("Такого ID нет в списке");
                    }
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("1 – Изменить название планеты");
                            Console.WriteLine("2 – Изменить количество спутников");
                            Console.WriteLine("3 – Изменить состав атмосферы");
                            Console.WriteLine("0 – Выход в главное меню");

                            switch (char.ToLower(Console.ReadKey(true).KeyChar))
                            {
                                case '1': Console.WriteLine("Ввод: "); planet.Name = Console.ReadLine(); db.Planets.Update(planet); db.SaveChanges(); break;
                                case '2': Console.WriteLine("Ввод: "); planet.NumSatellite = Convert.ToInt32(Console.ReadLine()); db.Planets.Update(planet); db.SaveChanges(); break;
                                case '3': Console.WriteLine("Ввод: "); planet.Atmosphere = Console.ReadLine(); db.Planets.Update(planet); db.SaveChanges(); break;
                                case '0': return;
                                default: break;
                            }

                        }
                    }
                }
            }
        }
    }
}