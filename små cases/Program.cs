using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace små_cases
{
    internal class Password
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "password.txt"))) //tjekker om password fil eksisterer
                {
                    try //prøv at hente filens størelse
                    {
                        if (new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "password.txt")).Length != 0) //tjekker om password filen er tom
                        {
                            //kald input login funktion hvis filen ikke er tom
                            Password.Login();
                        }
                        else
                        {
                            //kald password create funktion hvis filen er tom
                            Password.Create();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        Thread.Sleep(3000);
                        Console.Clear();
                    }
                }
                else
                {
                    //kald password create funktion hvis filen ikke eksisterer
                    Password.Create();
                }
            }while (true);
            
        }
        static void Create()
        {
            string passwordinput; //opret passwordinput string variable
            string brugernavn; //opret brugernavn string variable
            char lastchar; //opret lastchar char variable
            do
            {
                Console.Clear();
                Console.Write("brugernavn: "); //spørg om brugerens brugernavn
                brugernavn = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(brugernavn)) //tjekker 
                {
                    Console.WriteLine("\nBrugernavn kan ikke være tomt");
                    Thread.Sleep(3000);
                    continue;
                }
                else
                {
                    do
                    {
                        Console.Write("Skriv venligts et nyt kodeord. Det skal mindste være 12 tegn, anvende både store og små, mindst et tal og specialtegn, ikke starte eller slutte med tal, ingen mellemrum og ikke matche brugernavnet: "); //spørg brugeren om et password
                        passwordinput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(passwordinput)) //tjek om password er tomt eller kun whitespaces
                        {
                            Console.WriteLine("\nPassword kan ikke være tomt");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        lastchar = passwordinput.Last();
                        if (brugernavn.ToLower() == passwordinput.ToLower()) //tjek om password matcher brugernavn
                        {
                            Console.WriteLine("\nPassword må ikke matche brugernavn");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        else if (passwordinput.Length < 12) //tjek om password er under 12 tegn
                        {
                            Console.WriteLine("\nPassword skal minimum være 12 tegn langt");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        else if (!passwordinput.Any(char.IsUpper) | !passwordinput.Any(char.IsLower)) //tjek om password ikke har store og små tegn
                        {
                            Console.WriteLine("\nPassword skal have både store og små tegn");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        else if (passwordinput.All(char.IsLetterOrDigit) | !passwordinput.Any(char.IsNumber)) //tjek om password kun har normale tegn
                        {
                            Console.WriteLine("\nPassword skal have mindst et special tegn og tal");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        else if (char.IsDigit(passwordinput[0]) | char.IsDigit(lastchar)) //tjek om password starter eller slutter med et tal
                        {
                            Console.WriteLine("\nPassword må ikke starte eller slutte med et tal");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        else if (passwordinput.Any(char.IsWhiteSpace)) //tjek om password has et mellemrum/whitespace
                        {
                            Console.WriteLine("\nDer må ikke være mellemrum i passwordet");
                            Thread.Sleep(3000);
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            string[] login = { brugernavn, passwordinput }; //lav string array der heder login, med brugernavn og passwordinput variabler
                            try //prøver at skrive indeholdet af array til password.txt fil
                            {
                                File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "password.txt"), login);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Thread.Sleep(3000);
                                Console.Clear();
                                continue;
                            }
                            Console.WriteLine("Password gemt!");
                            Thread.Sleep(3000);
                            Console.Clear();
                            return;

                        }

                    } while (true);

                }
            } while (true);
        }
        static void Login()
        {
            do
            {
                string brugernavn = "", passwordlogin = ""; //opret string variabler brugernavn og passwordlogin
                List<string> skrevetlogin = new List<string>(); //opret string list variable skrevetlogin
                string[] gemtlogin = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "password.txt")); //læs og gem hvert linje i tekstfil i array
                Console.WriteLine("Du er nu ved at logge ind");
                Console.Write("\nBrugernavn: ");
                brugernavn = Console.ReadLine().ToLower();
                Console.Write("\nPassword: ");
                passwordlogin = Console.ReadLine();
                skrevetlogin.Add(brugernavn); //tilføj brugernavn til list
                skrevetlogin.Add(passwordlogin); //tilføj password til list
                if (gemtlogin.SequenceEqual(skrevetlogin.ToArray())) //koverter list til array og sammenlign dem
                {
                    Console.WriteLine("\nWelkommen ind!");
                }
                else
                {
                    Console.WriteLine("\nBrugernavn eller password forkert");
                    Thread.Sleep(2000);
                    Console.Clear();
                    continue;
                }
            } while (true);
        }
    }
}
