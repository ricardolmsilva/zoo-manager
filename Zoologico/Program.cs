using System;
using System.Collections.Generic;
using System.IO;

namespace Zoologico
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            GestorAreas.FicheiroAreas();    //Chamada ao carregamento do FicheiroAreas
            Menu();             //Chamada ao Menu
        }


        public static void Menu()
        {
            int menu;
            do
            {
                Console.Clear();
                string boasvindas = "BEM-VINDO AO ZOOLOGICO - a21270211";
                Console.SetCursorPosition((Console.WindowWidth - boasvindas.Length) / 2, Console.CursorTop);
                Console.WriteLine(boasvindas);
                Console.WriteLine("\n1 - IMPRIMIR AREAS" +
                                  "\n2 - CRIAR AREA" +
                                  "\n3 - ELIMINAR AREA" +
                                  "\n\n4 - LISTAR TODAS ESPECIES" +
                                  "\n5 - LISTAR ESPECIES NO ZOO" +
                                  "\n6 - CRIAR ESPECIE" +
                                  "\n7 - ADD HABITATE A ESPECIE" +
                                  "\n8 - APAGAR ESPECIES" +
                                  "\n9 - APAGAR HABITATE A ESPECIE" +
                                  "\n\n10 - CRIAR ANIMAL" +
                                  "\n11 - IMPRIMIR ANIMAIS" +
                                  "\n12 - APAGAR ANIMAL" +
                                  "\n13 - NASCER ANIMAL" +
                                  "\n\nENTER - SAIR");

                Console.Write("\n");
                int.TryParse(Console.ReadLine(), out menu);

                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        GestorAreas.ImprimirAreas();
                        break;
                    case 2:
                        Console.Clear();
                        GestorAreas.CriarArea();
                        break;
                    case 3:
                        Console.Clear();
                        GestorAreas.EliminarAreas();
                        break;
                    case 4:
                        Console.Clear();
                        GestorEspecies.ImprimirEspecies();
                        break;
                    case 5:
                        Console.Clear();
                        GestorAnimais.getEspecie();
                        break;
                    case 6:
                        Console.Clear();
                        GestorEspecies.CriarEspecie();
                        break;
                    case 7:
                        Console.Clear();
                        GestorEspecies.addHabitate();
                        break;
                    case 8:
                        Console.Clear();
                        GestorEspecies.ApagarEspecie();
                        break;
                    case 9:
                        Console.Clear();
                        GestorEspecies.ApagarHabitateEspecie();
                        break;
                    case 10:
                        Console.Clear();
                        GestorAnimais.CriarAnimal();
                        break;
                    case 11:
                        Console.Clear();
                        GestorAnimais.ImprimirAnimais();
                        break;
                    case 12:
                        Console.Clear();
                        GestorAnimais.EliminarAnimal();
                        break;
                    case 13:
                        Console.Clear();
                        GestorAnimais.NascerAnimal();
                        break;
                    case 0:
                        return;
                }

            } while (menu < 0 || menu > 2);
        }
    }
}