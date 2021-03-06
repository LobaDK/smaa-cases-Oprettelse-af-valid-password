using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Oprettelse_af_valid_Password
{
    internal class Password : Passwordvalidate
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
                            //kald login funktion hvis filen ikke er tom
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
        static void Create() //opret Create metode
        {
            string passwordinput; //opret passwordinput string variable
            string brugernavn; //opret brugernavn string variable
            do
            {
                Console.Clear();
                Console.WriteLine("Velkommen til! Ingen bruger fundet, venlist opret en.");
                Console.Write("\nbrugernavn: "); //spørg om brugerens brugernavn
                brugernavn = Console.ReadLine().ToLower(); //gem brugernavn i lowercase så det er nemmere at sammenligne
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
                        Console.Write("\nSkriv venligts et nyt kodeord. Det skal mindste være 12 tegn, anvende både store og små, mindst et tal og specialtegn, ikke starte eller slutte med tal, ingen mellemrum og ikke matche brugernavnet: "); //spørg brugeren om et password
                        passwordinput = Console.ReadLine();
                        
                        if (!Password.PasswdCheck(passwordinput,brugernavn)) //brug opretet PasswdCheck metode fra Password klasse til at validere password
                        {
                            continue;
                        }
                        else
                        {
                            string[] login = { brugernavn, passwordinput }; //opret string array der heder login, med brugernavn og passwordinput variabler
                            try 
                            {
                                File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "password.txt"), login); //prøver at skrive indeholdet af array til password.txt fil
                                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "passwdhistory.txt"), passwordinput); //prøver at skrive password til historik fil
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Thread.Sleep(3000);
                                Console.Clear();
                                continue;
                            }
                            Console.WriteLine("\nPassword gemt!");
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
                string brugernavn = "", passwordlogin = "",passwordinput; //opret string variabler brugernavn og passwordlogin
                List<string> skrevetlogin = new List<string>(); //opret string list variable skrevetlogin
                string[] gemtlogin = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "password.txt")); //læs og gem hvert linje i tekstfil fra array
                Console.WriteLine("Velkommen!");
                Console.WriteLine("\nSkriv dit brugernavn og password for at logge ind.");
                Console.Write("\nBrugernavn: ");
                brugernavn = Console.ReadLine().ToLower(); //gem brugernavn i lowercase så det er nemmere at sammenligne
                Console.Write("\nPassword: ");
                passwordlogin = Console.ReadLine();
                skrevetlogin.Add(brugernavn); //tilføj brugernavn til list
                skrevetlogin.Add(passwordlogin); //tilføj password til list
                if (gemtlogin.SequenceEqual(skrevetlogin.ToArray())) //koverter list til array og sammenlign dem
                {
                    do
                    {
                        string appmenu;
                        Console.Clear();
                        Console.WriteLine("\nWelkommen ind!");
                        Console.Write("Tryk 1 for at starte app1\nTryk 2 for at starte app2\nTryk 3 for at ændre dit password: ");
                        appmenu = Console.ReadLine();
                        if (appmenu == "1") //starter app1
                        {
                            Console.WriteLine("\nApp1");
                            Thread.Sleep(2000);
                            Console.Clear();
                            continue;
                        }
                        else if (appmenu == "2") //starter app2
                        {
                            Console.WriteLine("\nApp2");
                            Thread.Sleep(2000);
                            continue;
                        }
                        else if (appmenu == "3") //starter password ændring
                        {
                            Console.Write("\nSkriv dit nye password: ");
                            passwordinput = Console.ReadLine();
                            if (!Password.PasswdCheck(passwordinput, brugernavn)) //brug opretet PasswdCheck metode fra Password klasse til at validere password
                            {
                                continue; //går tilbage hvis password ikke er valid
                            }
                            else if (File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "passwdhistory.txt")).Contains(passwordinput)) //tjek om password er i passwdhistory fil
                            {
                                Console.WriteLine("\nPassword må ikke matche tidligere brugte passwords");
                                Thread.Sleep(2000);
                                Console.Clear();
                                continue;
                            }
                            else
                            {
                                string[] login = { brugernavn, passwordinput }; //lav string array der heder login, med brugernavn og passwordinput variabler
                                try
                                {
                                    File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory(), "password.txt"), login); //prøver at skrive indeholdet af array til password.txt fil
                                    File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "passwdhistory.txt"), Environment.NewLine + passwordinput); //prøver at skrive password til historik fil
                                    Console.WriteLine("\nPassword ændret! Du bliver nu logget ud...");
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Thread.Sleep(3000);
                                    Console.Clear();
                                    continue;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("{0} er ikke en mulighed", appmenu);
                            Thread.Sleep(2000);
                            continue;
                        }
                    } while (true);
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
        static bool PasswdCheck(string passwordinput, string brugernavn) //opret PasswdCheck metode der retunere false eller true boolean
        {
            char lastchar = passwordinput.Last(); //gem sidste tegn fra password i char variable
            Passwordvalidate passwordvalidate = new Passwordvalidate(); //opret en nu instance af Passwordvalidate som passwordvalidate
            if (passwordvalidate.IsEmpty(passwordinput))
            {
                return false; //hvis password er tomt
            }
            if (passwordvalidate.MatchUsername(brugernavn, passwordinput))
            {
                return false; //hvis password matcher brugernavnet
            }
            else if (passwordvalidate.IsOver12(passwordinput))
            {
                return false; //hvis passwoder er under 12 tegn
            }
            else if (passwordvalidate.IsLowerAndUpper(passwordinput))
            {
                return false; //hvis password mangler store eller små bogstaver
            }
            else if (passwordvalidate.HasSpecialCharAndNumber(passwordinput))
            {
                return false; //hvis password mangler specielle tegn
            }
            else if (passwordvalidate.StartOrEndWithNumber(passwordinput, lastchar))
            {
                return false; //hvis password starter eller slutter med tal
            }
            else if (passwordvalidate.HasSpace(passwordinput))
            {
                return false; //hvis password har mellemrum
            }
            else return true;
        }
    }
    class Passwordvalidate //opret klasse Passwordvalidate
    {
        public bool IsEmpty(string passwordinput) //opret bool IsEmpty metode
        {
            if (string.IsNullOrWhiteSpace(passwordinput)) //tjek om password er tomt eller kun whitespaces
            {
                Console.WriteLine("\nPassword kan ikke være tomt");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
        public bool MatchUsername(string brugernavn, string passwordinput) //opret bool MatchUsername metode
        {
            if (brugernavn.ToLower() == passwordinput.ToLower()) //tjek om password matcher brugernavn
            {
                Console.WriteLine("\nPassword må ikke matche brugernavn");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
        public bool IsOver12(string passwordinput) //opret bool IsOver12 metode
        {
            if (passwordinput.Length < 12) //tjek om password er under 12 tegn
            {
                Console.WriteLine("\nPassword skal minimum være 12 tegn langt");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
        public bool IsLowerAndUpper(string passwordinput) //opret bool IsLowerandUpper metode
        {
            if (!passwordinput.Any(char.IsUpper) | !passwordinput.Any(char.IsLower)) //tjek om password ikke har store og små tegn
            {
                Console.WriteLine("\nPassword skal have både store og små tegn");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
        public bool HasSpecialCharAndNumber(string passwordinput) //opret bool HasSpecialCharAndNumber metode
        {
            if (passwordinput.All(char.IsLetterOrDigit) | !passwordinput.Any(char.IsNumber)) //tjek om password kun har normale tegn
            {
                Console.WriteLine("\nPassword skal have mindst et special tegn og tal");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
        public bool StartOrEndWithNumber(string passwordinput, char lastchar) //opret bool StartOrEndWithNumber metode
        {
            if (char.IsDigit(passwordinput[0]) | char.IsDigit(lastchar)) //tjek om password starter eller slutter med et tal
            {
                Console.WriteLine("\nPassword må ikke starte eller slutte med et tal");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
        public bool HasSpace(string passwordinput) //opret bool HasSpace metode
        {
            if (passwordinput.Any(char.IsWhiteSpace)) //tjek om password has et mellemrum/whitespace
            {
                Console.WriteLine("\nDer må ikke være mellemrum i passwordet");
                Thread.Sleep(3000);
                Console.Clear();
                return true;
            }
            else return false;
        }
    }
}
