using System;
using System.Collections.Generic;
using System.IO;
namespace Zoologico
{
    public class GestorAnimais
    {
        static List<Animais> ListaAnimais = new List<Animais>();
        static List<string> ListaProgenitores;
        static List<string> ListaFilhos;


        public static void CriarAnimal()
        {
            if (GestorEspecies.ListaVazia())
            {
                Console.WriteLine("AINDA NAO NENHUMA ESPECIE CRIADA, PARA QUE POSSA CRIAR ANIMAL");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }
            else
            {
                string nomeAnimal;
                ListaProgenitores = new List<string>();
                ListaFilhos = new List<string>();
                do
                {
                    Console.WriteLine("INSIRA O NOME DO ANIMAL");
                    nomeAnimal = Console.ReadLine();
                    if (nomeAnimal.Length < 1)
                    {
                        Console.WriteLine("INSIRA UM NOME PARA O ANIMAL");
                    }
                    else
                    {

                        if (verificaNomeNaLista(nomeAnimal))
                        {
                            Console.WriteLine("JA EXISTE UM ANIMAL COM O MESMO NOME");
                        }
                    }

                } while (verificaNomeNaLista(nomeAnimal) || nomeAnimal.Length < 1);

                int peso;
                do
                {
                    Console.WriteLine("INDIQUE O PESO DO ANIMAL");
                    int.TryParse(Console.ReadLine(), out peso);

                    if (peso <= 0)
                    {
                        Console.WriteLine("PESO INVÁLIDO");
                    }
                } while (peso <= 0);


                string especie;
                do
                {

                    Console.WriteLine("INDIQUE A ESPECIE DESTE ANIMAL");
                    especie = Console.ReadLine();
                    if (!GestorEspecies.verificaEspecieExiste(especie))
                    {
                        Console.WriteLine("A ESPECIE NãO EXISTE");
                    }
                } while (!GestorEspecies.verificaEspecieExiste(especie));

                string localizacao;
                bool verificaArea = true;
                do
                {
                    Console.WriteLine("INDIQUE A AREA PARA ESTE ANIMAL");
                    localizacao = Console.ReadLine();
                    if (GestorAreas.verificaAreaExiste(localizacao))
                    {
                        string habitateDestaArea = GestorAreas.GetHabitateArea(localizacao);

                        if (GestorEspecies.verificaEspecieHabitateExiste(especie, habitateDestaArea))
                        {
                            verificaArea = true;
                        }
                        else
                        {
                            Console.WriteLine("ESTA AREA NAO TEM O HABITATE APROPRIADA Á ESPECIE ESCOLHIDA PARA ESTE ANIMAL");
                            verificaArea = false;
                        }

                    }
                    else
                    {
                        Console.WriteLine("A AREA NAO EXISTE");
                        verificaArea = false;
                    }
                } while (verificaArea == false);

                int simuladorLista = ListaAnimais.Count;
                if (ListaAnimais.Count > ListaProgenitores.Count)
                {

                    char temPais;
                    do
                    {
                        Console.WriteLine("O ANIMAL TEM PAIS NO ZOO? Y/N");
                        char.TryParse(Console.ReadLine(), out temPais);

                        bool verificaAnimal = false;
                        if (temPais == 'y')
                        {
                            string nomePai;
                            do
                            {

                                Console.WriteLine("INDIQUE O NOME DO/A PROGENITOR/A");
                                do
                                {
                                    nomePai = Console.ReadLine();
                                    foreach (Animais animal in ListaAnimais)
                                    {
                                        if (nomePai == animal.getNomeAnimal())
                                        {
                                            ListaProgenitores.Add(nomePai);
                                            simuladorLista++;
                                            verificaAnimal = true;
                                            break;
                                        }
                                    }

                                    if (verificaAnimal == false)
                                    {
                                        Console.WriteLine("ESSE ANIMAL NAO EXISTE NO ZOO");
                                    }

                                } while (verificaAnimal == false);


                                while (ListaProgenitores.Count < ListaAnimais.Count && ListaProgenitores.Count < 2)
                                {
                                    Console.WriteLine("O ANIMAL TEM MAIS ALGUM PROGENITOR NO ZOO? Y/N");
                                    char.TryParse(Console.ReadLine(), out temPais);

                                    if (temPais != 'n' && temPais != 'y')
                                    {
                                        Console.WriteLine("ESSA TECLA E INVALIDA");
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (!(ListaProgenitores.Count < ListaAnimais.Count && ListaProgenitores.Count < 2))
                                {
                                    temPais = 'n';
                                }

                            } while (temPais == 'y');

                        }
                        else if (temPais != 'n')
                        {
                            Console.WriteLine("ESSA TECLA E INVALIDA");
                        }
                    } while (temPais == 'y' || temPais != 'n');
                }

                if (ListaAnimais.Count > ListaProgenitores.Count)
                {
                    char temFilhos;
                    Console.WriteLine("O ANIMAL TEM FILHOS NO ZOO? Y/N");
                    char.TryParse(Console.ReadLine(), out temFilhos);

                    do
                    {
                        if (temFilhos == 'y')
                        {
                            Console.WriteLine("QUANTOS FILHOS TEM O ANIMAL?");
                            int.TryParse(Console.ReadLine(), out int nFilhos);
                            //do{
                            if (((ListaAnimais.Count - (ListaFilhos.Count + ListaProgenitores.Count)) >= nFilhos) && nFilhos > 0)
                            {
                                bool verificaAnimal = false;
                                for (int i = 0; i < nFilhos; i++)
                                {
                                    do
                                    {
                                        Console.WriteLine("INDIQUE O NOME DO FILHO " + i + 1);

                                        string nomeFilho = Console.ReadLine();
                                        foreach (Animais animal in ListaAnimais)
                                        {
                                            if (nomeFilho == animal.getNomeAnimal())
                                            {
                                                ListaFilhos.Add(nomeFilho);
                                                verificaAnimal = true;
                                            }
                                        }
                                        if (verificaAnimal == false)
                                        {
                                            Console.WriteLine("ESSE ANIMAL NAO EXISTE NO ZOO");
                                        }
                                    } while (verificaAnimal == false);
                                    temFilhos = 'n';
                                }
                            }
                            else if (nFilhos < 1)
                            {
                                Console.WriteLine("INTRODUZA MAIOR QUE 0");
                            }
                            else
                            {
                                Console.WriteLine("SO EXISTEM {0} FILHOS DISPONIVEIS", ListaAnimais.Count - (ListaFilhos.Count + ListaProgenitores.Count));
                            }
                            //}while () ;
                        }
                        else if (temFilhos != 'n')
                        {
                            Console.WriteLine("ESSA TECLA E INVALIDA");
                        }
                    } while (temFilhos != 'n');
                }


                Animais novoAnimal = new Animais(nomeAnimal, peso, especie, localizacao, ListaProgenitores, ListaFilhos);
                ListaAnimais.Add(novoAnimal);
                Console.WriteLine("O ANIMAL FOI CRIADO COM SUCESSO");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }
        }//FIM CRIAR ANIMAL


        public static void NascerAnimal()
        {
            Console.WriteLine("Quantos progenitores tem o animal (1 ou 2)?");
            int.TryParse(Console.ReadLine(), out int nParentes);

            if (nParentes < 1 || nParentes > 2)
            {
                Console.WriteLine("Numero de parentes invalido. Registo de nasimento cancelado.");
                return;
            }


            // recolher a lista com o nome dos pais (1 ou 2)
            List<string> pais = new List<string>();
            string pai = "";
            do
            {
                for (int i = 0; i < nParentes; i++)
                {
                    Console.Write("O nome do parente {0} -> ", i + 1);
                    pai = Console.ReadLine();
                    if (GestorAnimais.verificaNomeNaLista(pai))
                        pais.Add(pai);
                    else
                    {
                        i--;
                        Console.WriteLine("Erro: o animal indicado não existe.");
                    }
                }
            } while (!GestorAnimais.verificaNomeNaLista(pai));


            // inicio do codigo para apanhar a localizacao dos pais e verificar se estao dentro da mesma area
            string localPais = "";

            foreach (Animais a in ListaAnimais)
            {
                if (a.getNomeAnimal() == pais[0])
                    localPais = a.getLocalizacaoAnimal();
            }

            if (pais.Count > 1)
            {
                foreach (Animais a in ListaAnimais)
                {
                    if (a.getNomeAnimal() == pais[1])
                    {
                        if (a.getLocalizacaoAnimal() != localPais)
                        {
                            Console.WriteLine("Os pais indicados encontram-se em localizacoes diferentes." +
                                              "\nRegisto de nascimento cancelado.");
                            return;
                        }
                    }
                }
            }

            // inicio do codigo para apanhar a especie dos pais e verificar se sao da mesma especie
            string especiePais = "";

            foreach (Animais a in ListaAnimais)
            {
                if (a.getNomeAnimal() == pais[0])
                    especiePais = a.getEspecieAnimal();
            }

            if (pais.Count > 1)
            {
                foreach (Animais a in ListaAnimais)
                {
                    if (a.getNomeAnimal() == pais[1])
                    {
                        if (especiePais != a.getEspecieAnimal())
                        {
                            Console.WriteLine("Os pais indicados sao de especies diferentes." +
                                              "\nRegisto de nascimento cancelado.");
                            return;
                        }
                    }
                }
            }



            // recolher 20% do peso de cada pai -> peso do novo animal
            double pesoNovoAnimal = 0;
            foreach (Animais a in ListaAnimais)
            {
                foreach (string s in pais)
                {
                    if (a.getNomeAnimal() == s)
                    {
                        pesoNovoAnimal += a.GetPeso() * 0.2;
                    }
                }
            }

            string nomeNovoAnimal;
            do
            {
                Console.WriteLine("Insira o nome do animal que vai nascer:");
                nomeNovoAnimal = Console.ReadLine();
                if (GestorAnimais.verificaNomeNaLista(nomeNovoAnimal))
                    Console.WriteLine("Já existe um animal com esse nome.");

            } while (GestorAnimais.verificaNomeNaLista(nomeNovoAnimal) || nomeNovoAnimal.Length < 1);

            // criada lista de filhos vazia. o animal acabou de nascer, logo nao tem filhos.
            List<string> filhos = new List<string>();

            Animais newAnimal = new Animais(nomeNovoAnimal, pesoNovoAnimal, especiePais, localPais, pais, filhos);
            ListaAnimais.Add(newAnimal);


        }


        public static void EliminarAnimal()
        {

            int indexArea = 0;
            string animal;
            do
            {
                do
                {
                    Console.WriteLine("INDIQUE O ANIMAL A ELIMINAR");
                    animal = Console.ReadLine();
                    try
                    {
                        foreach (char c in animal)
                        {
                            if (char.IsDigit(c))
                            {
                                int.TryParse(animal, out indexArea);
                                animal = ListaAnimais[indexArea - 1].getNomeAnimal();
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("O ANIMAL NÃO SE ENCONTRA NA AREAS DA LISTA");

                    }

                } while (!verificaNomeNaLista(animal));

                if (verificaNomeNaLista(animal))
                {
                    foreach (Animais a in ListaAnimais)
                    {
                        string nome = a.getNomeAnimal();
                        if (nome == animal)
                        {
                            ListaAnimais.Remove(a);
                            Console.WriteLine("ANIMAL APAGADO DO ZOO");
                            Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                            Console.ReadLine();
                            Console.Clear();
                            MainClass.Menu();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("O ANIMAL NAO SE ENCONTRA NA LISTA");
                }

            } while (!verificaNomeNaLista(animal));
        }//FIM ELIMINAR ANIMAL

        public void TransferirAnimal()
        {
            string animal;
            string nomeArea;
            do
            {
                Console.WriteLine("INDIQUE O ANIMAL A TRANSFEIR");
                animal = Console.ReadLine();
                try
                {
                    foreach (char c in animal)
                    {
                        if (char.IsDigit(c))
                        {
                            int.TryParse(animal, out int indexArea);
                            animal = ListaAnimais[indexArea - 1].getNomeAnimal();
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("O ANIMAL NÃO SE ENCONTRA NA LISTA");

                }

            } while (!verificaNomeNaLista(animal));


            do
            {
                Console.WriteLine("INDIQUE A AREA PARA ONDE DESEJA TRANSFERIR O ANIMAL");
                nomeArea = Console.ReadLine();


            } while (!GestorAreas.verificaAreaExiste(nomeArea));


            string areaAnimal = "";
            foreach(Animais a in ListaAnimais){
                if(a.getNomeAnimal()== animal){
                    areaAnimal = a.getLocalizacaoAnimal();
                }
            }




        }




        public static void ImprimirAnimais()
        {
            foreach (Animais animal in ListaAnimais)
            {
                Console.WriteLine(animal.ImprimirConsola());
            }
            Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
            Console.ReadLine();
            Console.Clear();
            MainClass.Menu();
        }


        public static void getEspecie()
        {
            if (ListaAnimais.Count <= 0)
            {
                Console.WriteLine("NAO EXISTEM ANIMAIS NA LISTA");
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }else{
                foreach (Animais a in ListaAnimais)
                {
                    string especieDoAnimal = a.getEspecieAnimal();
                    GestorEspecies.ImprimeEspecie(especieDoAnimal);
                }
                Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
                Console.ReadLine();
                Console.Clear();
                MainClass.Menu();
            }

        }


        static bool verificaNomeNaLista(string nomeAnimal)
        {
            foreach (Animais animal in ListaAnimais)
            {
                if (animal.getNomeAnimal() == nomeAnimal)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool verificaExisteNaArea(string area)
        {
            foreach (Animais a in ListaAnimais)
            {
                string nomeArea = a.getLocalizacaoAnimal();
                if (nomeArea == area)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool verificaEspecie(string especie)
        {
            foreach (Animais a in ListaAnimais)
            {
                string especieDoAnimal = a.getEspecieAnimal();
                if (especie == especieDoAnimal)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
