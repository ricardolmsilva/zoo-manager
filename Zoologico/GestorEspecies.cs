using System;
using System.Collections.Generic;
using System.IO;

namespace Zoologico
{
    public class GestorEspecies
    {
        static List<Especies> ListaEspecies = new List<Especies>();
        static List<Habitates> EspecieHabitates;

        public static void ImprimirEspecies()
        {
            if (ListaEspecies.Count <= 0)
            {
                Console.WriteLine("NAO EXISTEM ESPECIES NA LISTA");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }
            else
            {
                foreach (Especies e in ListaEspecies)
                {
                    Console.WriteLine(e.ImprimirEspecie());
                }
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }

        }

        public static void CriarEspecie()
        {
            bool existe = false;
            string especie;
            do
            {
                do
                {
                    Console.WriteLine("INSIRA O NOME DA ESPECIE");
                    especie = Console.ReadLine();
                    existe = false;
                        foreach (char c in especie)
                        {
                        if (char.IsDigit(c) || char.IsSymbol(c) || char.IsPunctuation(c) || especie.Length<1)
                            {
                            Console.WriteLine("O nome nao pode conter digitos ou caracteres especiais");
                            existe = true;
                            break;
                            }
                        }

                } while (existe);


                foreach (Especies nomeEspecie in ListaEspecies)
                {
                    existe = false;
                    if (nomeEspecie.GetID() == especie)
                    {
                        Console.WriteLine("ESSA ESPECIE JA EXISTE NA LISTA DE ESPECIES");
                        existe = true;
                        break;
                    }
                }
            } while (existe);


            Habitates nHabitate;
            char valor = ' ';
            EspecieHabitates = new List<Habitates>();
            do
            {
                if (EspecieHabitates.Count < Enum.GetValues(typeof(Habitates)).Length)
                {
                    Console.WriteLine("INSIRA O HABITATE IDEAL PARA ESTA ESPÉCIE\n");
                    Enum.TryParse(Console.ReadLine(), out nHabitate);


                    if (nHabitate <= 0 || ((int)(nHabitate)) > Enum.GetValues(typeof(Habitates)).Length)
                    {
                        Console.WriteLine("O HABITATE NAO ESTA DISPONIVEL NA LISTA DE HABITARES");
                    }

                    else if (EspecieHabitates.Contains(nHabitate))
                    {
                        Console.WriteLine("O HABITATE JA EXISTE NA LISTA DE HABITATES DESTA ESPECIE");
                    }
                    else
                    {
                        EspecieHabitates.Add(nHabitate);
                        Console.WriteLine("O HABITATE FOI ADICIONADO AOS HABITATES DESTA ESPECIE\n");
                        if (EspecieHabitates.Count < Enum.GetValues(typeof(Habitates)).Length)
                        {
                            Console.WriteLine("DESEJA ADICIONAR MAIS HABITATES A ESTA ESPECIE? y/n");
                        }
                        else
                        {
                            Especies novaEspecie = new Especies(especie, EspecieHabitates);
                            ListaEspecies.Add(novaEspecie);
                            Console.WriteLine("NAO EXISTEM MAIS HABITATES DISPONIVEIS NA LISTA");
                            Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                        do
                        {
                            char.TryParse(Console.ReadLine().ToLower(), out valor);

                            if (valor == 'n')
                            {
                                Especies novaEspecie = new Especies(especie, EspecieHabitates);
                                ListaEspecies.Add(novaEspecie);
                                Console.WriteLine("A NOVA ESPECIE FOI ADICIONADA COM SUCESSO");
                                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                                Console.ReadLine();
                                Console.Clear();
                                MainClass.Menu();
                            }
                            else if (valor != 'n' && valor != 'y')
                            {
                                Console.WriteLine("VALOR INVALIDO\n" +
                                                          "ESCOLHA 'y' - YES | 'n' - NO");
                            }
                        } while (valor != 'n' && valor != 'y');
                    }
                }
                else
                {
                    Console.WriteLine("PARA CRIAR UMA NOVA ESPECIE NECESSITA QUE EXISTAM HABITATES");
                    break;
                }

            } while (valor == 'y');

        }//FIM CRIAR ESPECIES


        public static void addHabitate()
        {
            if (ListaEspecies.Count <= 0)
            {
                Console.WriteLine("NAO EXISTEM ESPECIES NA LISTA");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }
            else
            {
                int indexEspecie;

                do
                {
                    Console.WriteLine("ESCOLHA A ESPECIE QUE DESEJA ADICIONAR UM HABITATE");
                    int.TryParse(Console.ReadLine(), out indexEspecie);

                    if (indexEspecie > ListaEspecies.Count || indexEspecie <= 0)
                    {
                        Console.WriteLine("ESSA ESPECIE NAO EXISTE NO ZOO");
                    }
                    else
                    {
                        Habitates nHabitate;
                        char valor = ' ';
                        EspecieHabitates = new List<Habitates>();
                        do
                        {
                            Console.WriteLine("INSIRA O HABITATE IDEAL PARA ESTA ESPÉCIE\n");
                            Enum.TryParse(Console.ReadLine(), out nHabitate);


                            if (nHabitate <= 0 || ((int)(nHabitate)) > Enum.GetValues(typeof(Habitates)).Length)
                            {
                                Console.WriteLine("O HABITATE NAO ESTA DISPONIVEL NA LISTA DE HABITARES");
                            }

                            else if (EspecieHabitates.Contains(nHabitate))
                            {
                                Console.WriteLine("O HABITATE JA EXISTE NA LISTA DE HABITATES DESTA ESPECIE");
                            }
                            else
                            {
                                EspecieHabitates.Add(nHabitate);
                                Console.WriteLine("O HABITATE FOI ADICIONADO AOS HABITATES DESTA ESPECIE\n" +
                                                  "DESEJA ADICIONAR MAIS HABITATES A ESTA ESPECIE? y/n");
                                do
                                {
                                    char.TryParse(Console.ReadLine().ToLower(), out valor);

                                    if (valor == 'n')
                                    {
                                        foreach (Especies e in ListaEspecies)
                                        {
                                            if (e.GetID() == ListaEspecies[indexEspecie - 1].GetID())
                                            {
                                                e.AdicionaHabitat(EspecieHabitates);
                                            }
                                        }
                                        Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                                        Console.ReadLine();
                                        Console.Clear();
                                        MainClass.Menu();
                                    }
                                    else if (valor != 'n' && valor != 'y')
                                    {
                                        Console.WriteLine("VALOR INVALIDO\n" +
                                                          "ESCOLHA 'y' - YES | 'n' - NO");
                                    }

                                } while (valor != 'n' && valor != 'y');
                            }

                        } while (((int)(nHabitate)) > Enum.GetValues(typeof(Habitates)).Length || valor == 'y');
                    }
                } while (indexEspecie > ListaEspecies.Count || indexEspecie <= 0);
            }
        }//FIM ADD ESPECIE


        public static void ApagarEspecie()
        {
            if (ListaEspecies.Count <= 0)
            {
                Console.WriteLine("NAO EXISTEM ESPECIES NA LISTA");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }
            else
            {
                string especie;
                do
                {
                    Console.WriteLine("QUAL A ESPECIE QUE DESEJA APAGAR?");
                    especie = Console.ReadLine();
                    if (verificaEspecieExiste(especie))
                    {
                        foreach (Especies e in ListaEspecies)
                        {
                            string especieLista = e.GetID();
                            if (especieLista == especie)
                            {
                                if (GestorAnimais.verificaEspecie(especie))
                                {
                                    Console.WriteLine("EXISTEM ANIMAIS DESTA ESPECIE, A ESPECIE NAO PODE SER APAGADA");
                                    break;
                                }
                                else
                                {
                                    ListaEspecies.Remove(e);
                                    Console.WriteLine("ESPECIE REMOVIDA COM SUCESSO");
                                    Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                                    Console.ReadLine();
                                    Console.Clear();
                                    MainClass.Menu();
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("NAO EXISTE ESSA ESPECIE NA LISTA DE ESPECIES");
                    }
                } while (!verificaEspecieExiste(especie));
            }
        }//FIM APAGAR ESPECIE


        public static void ApagarHabitateEspecie()
        {
            if (ListaEspecies.Count <= 0)
            {
                Console.WriteLine("NAO EXISTEM ESPECIES NA LISTA");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }
            else
            {
                string especie;
                int indexArea;

                do
                {
                    Console.WriteLine("INSIRA O NOME DA ESPECIE");
                    especie = Console.ReadLine();
                    try
                    {
                        foreach (char c in especie)
                        {
                            if (char.IsDigit(c))
                            {
                                int.TryParse(especie, out indexArea);
                                especie = ListaEspecies[indexArea - 1].GetID();
                            }
                        }
                    }
                    catch
                    {
                       Console.WriteLine("A ESPECIE NAO SE ENCONTRA NA LISTA");
                    }
                    if (!verificaEspecieExiste(especie) && !int.TryParse(especie, out indexArea))
                        Console.WriteLine("A ESPECIE NAO SE ENCONTRA NA LISTA");
                    
                } while (!verificaEspecieExiste(especie));

                Habitates nHabitate;
                do
                {
                    Console.WriteLine("QUAL O HABITATE QUE DESEJA APAGAR?");
                    Enum.TryParse(Console.ReadLine(), out nHabitate);
                    if (((int)(nHabitate)) < Enum.GetValues(typeof(Habitates)).Length)
                    {
                        if (verificaEspecieHabitateExiste(especie, nHabitate.ToString()))
                        {
                            if (GestorAnimais.verificaEspecie(especie))
                            {
                                Console.WriteLine("ESSE HABITATE TEM UM ANIMAL ASSOCIADO");
                                break;
                            }
                            else
                            {
                                foreach(Especies e in ListaEspecies){
                                    if(e.GetID() == especie){
                                        ApagaHabitate(nHabitate);
                                        Console.WriteLine("HABITATE APAGADO");
                                        Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                                        Console.ReadLine();
                                        Console.Clear();
                                        MainClass.Menu();
                                    }
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("ESTA ESPECIE NAO TEM ESTE HABITATE");
                        }

                    }
                    else
                    {
                        Console.WriteLine("O HABITATE NAO EXISTE");
                    }
                } while (((int)(nHabitate)) > Enum.GetValues(typeof(Habitates)).Length || !(verificaEspecieHabitateExiste(especie, nHabitate.ToString())));

            }
        }//FIM APAGAR HABITATE ESPECIE


        public static bool verificaEspecieExiste(string nomeEspecie)
        {
            foreach (Especies especie in GestorEspecies.ListaEspecies)
            {
                if (especie.GetID() == nomeEspecie)
                {
                    return true;
                }
            }
            return false;
        }//FIM VERIFICA ESPECIE EXISTE



        public static bool verificaEspecieHabitateExiste(string nomeEspecie, string habitate)
        {
            foreach (Especies especie in ListaEspecies)
            {
                if (nomeEspecie == especie.GetID())
                {
                    foreach (Habitates h in EspecieHabitates)
                    {
                        if (habitate == h.ToString())
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }//FIM VERIFICA ESPECIE HABITATE EXISTE



        public static bool ListaVazia()
        {
            if (ListaEspecies.Count < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//FIM VERIFICA LISTA ESPECIES VAZIA


        public static void ImprimeEspecie(string especie)
        {
            foreach (Especies e in ListaEspecies)
            {
                if (especie == e.GetID())
                {
                    Console.WriteLine(e.ImprimirEspecie());
                }
            }
        }//FIM OBTER ID ESPECIE


        static void ApagaHabitate(Habitates nHabitate){
            foreach(Habitates h in EspecieHabitates){
                if(nHabitate == h){
                    EspecieHabitates.Remove(nHabitate);
                    break;
                }
            }
        }
    }
}