using System;
using System.Collections.Generic;
using System.IO;

namespace Zoologico
{
    public class GestorAreas
    {
        //Lista AreasDoZoo
        static List<AreasDoZoo> ListaAreasDoZoo = new List<AreasDoZoo>();
        static List<string> ListaFronteiras;


        //Carregamento do ficheiro da AreasDoZoo
        public static void FicheiroAreas()
        {
            try
            {
                string[] ArrayAreas = File.ReadAllLines("AreasDoZoo.txt");

                //Percorrer todo o array  
                for (int i = 0; i < ArrayAreas.Length; i++)
                {
                    //Transformar cada index do array numa string
                    string LinhaEmStrings = ArrayAreas[i];
                    //Indexar cada palavra separadamente num array, retirando os espaços
                    string[] InfoArea = LinhaEmStrings.Split(' ');
                    //Atribuir cada index ao respetivo parâmetro das AreasDoZoo
                    string ID = InfoArea[0];
                    Enum.TryParse(InfoArea[1], out Habitates habitat);
                    Double.TryParse(InfoArea[2], out double capacidade);
                    int.TryParse(InfoArea[3], out int nFronteiras);

                    if (nFronteiras > 0)
                    {
                        ListaFronteiras = new List<string>();
                        for (int j = 4; j < InfoArea.Length; j++)
                        {
                            string nomeFronteita = InfoArea[j];
                            ListaFronteiras.Add(nomeFronteita);
                        }
                    }

                    //Criar nova AreaDoZoo
                    AreasDoZoo NovaArea = new AreasDoZoo(ID, habitat, capacidade, ListaFronteiras);

                    //Adicionar "NovaArea" á ListaFronteiras
                    ListaAreasDoZoo.Add(NovaArea);
                }
            }
            catch
            {
                Console.WriteLine("ERRO AO CARREGAR INFORMAÇÕES DO ZOO");
            }
        }//Fim FicheiroAreas


        //Imprime Areas do Zoo na Consola=======================================
        public static void ImprimirAreas()
        {
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,7} | {1,-15} | {2,6} | {3,7} | {4,-20} ", "ID", "HABITATE", "CAPAC.", "N.FRONT", "FRONTEIRAS"));
            Console.WriteLine("-----------------------------------------------------------------");

            foreach (AreasDoZoo area in ListaAreasDoZoo)
            {
                Console.WriteLine(area.ReturnAreasConsola());
            }
            Console.WriteLine("-----------------------------------------------------------------");

            Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
            Console.ReadLine();
            Console.Clear();
            MainClass.Menu();
        }



        //Criar NovaArea========================================================
        public static void CriarArea()
        {
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(string.Format("\t\t\tCRIAR NOVAS AREAS"));
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,7} | {1,-15} | {2,6} | {3,7} | {4,-20} ", "ID", "HABITATE", "CAPAC.", "N.FRONT", "FRONTEIRAS"));
            Console.WriteLine("-----------------------------------------------------------------");


            foreach (AreasDoZoo area in ListaAreasDoZoo)
            {
                Console.WriteLine(area.ReturnAreasConsola());
            }
            Console.WriteLine("-----------------------------------------------------------------");
            //VERIFICA A ULTIMA LETRA DA ULTIMA AREA CRIADA E INCREMENTA
            int numeroAreas = ListaAreasDoZoo.Count;
            string ID = ListaAreasDoZoo[numeroAreas - 1].getNomeArea();
            char.TryParse(ListaAreasDoZoo[numeroAreas - 1].getNomeArea().Remove(0, ID.Length - 1), out char ultimoChar);

            if (ultimoChar == 'Z' || ultimoChar == 'z')
            {
                ultimoChar = 'A';
                ID += ultimoChar;
            }
            else
            {
                ID = ID.Remove(ID.Length - 1) + ++ultimoChar;
            }


            //HABITATE
            Habitates nHabitate;
            do
            {
                Console.WriteLine("TIPO DE HABITATE:");
                Enum.TryParse(Console.ReadLine(), out nHabitate);
                if (nHabitate <= 0 || ((int)(nHabitate)) > Enum.GetValues(typeof(Habitates)).Length)
                {
                    Console.WriteLine("O HABITATE NAO EXISTE, INSIRA UM HABITATE VALIDO");
                }
            } while (nHabitate <= 0 || nHabitate == 0 || ((int)(nHabitate)) > Enum.GetValues(typeof(Habitates)).Length);


            //PESO MÁXIMO DA ÁREA
            Console.WriteLine("CAPACIDADE:");
            double capacidade;
            do
            {
                double.TryParse(Console.ReadLine(), out capacidade);
                if (capacidade <= 0)
                {
                    Console.WriteLine("A CAPACIDADE NECESSITA DE SER POSITIVA");
                }
            } while (capacidade <= 0);


            //QUANTIDADE DE FRONTEIRAS
            int nFronteiras;
            do
            {
                Console.WriteLine("QUANTIDADE DE FRONTEIRAS:");
                int.TryParse(Console.ReadLine(), out nFronteiras);

                if (nFronteiras > ListaAreasDoZoo.Count && ListaAreasDoZoo.Count <= 3)
                {
                    int nAreas = ListaAreasDoZoo.Count;
                    Console.WriteLine("O NUMERO DE AREAS É MENOR QUE O NÚMERO DE FRONTEIRAS REQUERIDAS\n" +
                                      "\nNUMERO DE AREAS DISPONIVEIS -- > {0}\n", nAreas);
                }

                if (nFronteiras <= 0 || nFronteiras > 3)
                {
                    Console.WriteLine("O NUMERO DE FRONTEIRAS NECESSITA ESTAR ENTRE 1 E 3\n");
                }

            } while (nFronteiras <= 0 || nFronteiras > 3 || nFronteiras > ListaAreasDoZoo.Count);


            //FRONTEIRAS
            ListaFronteiras = new List<string>();
            for (int i = 0; i < nFronteiras; i++)
            {
                Console.WriteLine("INSIRA A {0}ª ÁREA ", i + 1);
                int.TryParse(Console.ReadLine(), out int fronteira);
                while (fronteira > ListaAreasDoZoo.Count || fronteira <= 0)
                {
                    Console.WriteLine("A ÁREA NAO EXISTE NA LISTA DE AREAS, INSIRA UMA FRONTEIRA VALIDA");
                    Console.WriteLine("INSIRA A {0}ª ÁREA ", i + 1);
                    int.TryParse(Console.ReadLine(), out fronteira);
                }

                string nomeFronteira = ListaAreasDoZoo[fronteira - 1].getNomeArea();
                if (maxFronteiras(nomeFronteira))
                {
                    Console.WriteLine("ESSE AREA JA TEM O LIMITE FRONTEIRAS");
                    i--;
                }
                else if (ListaFronteiras.Contains(nomeFronteira))
                {
                    Console.WriteLine("JÁ INSERIU ESSA FRONTEIRA, ESCOLHA OUTRA FRONTEIRA VÁLIDA");
                    i--;
                }
                else
                {
                    ListaFronteiras.Add(nomeFronteira);
                }
            }

            //Cria novo objeto de AreaDoZoo
            AreasDoZoo NovaArea = new AreasDoZoo(ID, nHabitate, capacidade, ListaFronteiras);

            //Adiciona NovaArea Á ListaAreasDoZoo
            ListaAreasDoZoo.Add(NovaArea);

            //Escreve para ficheiro a NovaArea criada
            Console.WriteLine("\nNOVA AREA CRIADA:\n{0}", NovaArea.ReturnAreas());
            StreamWriter EscreveAreas = File.AppendText("AreasDoZoo.txt");

            EscreveAreas.WriteLine("{0}", NovaArea.ReturnAreas());
            EscreveAreas.Close();

            Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
            Console.ReadLine();
            Console.Clear();
            MainClass.Menu();
        }//FIM CriarArea()






        public static void EliminarAreas()
        {

            int indexArea = 0;
            string nomeArea;
            do
            {
                do
                {
                    Console.WriteLine("INDIQUE A ÁREA A ELIMINAR");
                    nomeArea = Console.ReadLine();
                    try
                    {
                        foreach (char c in nomeArea)
                        {
                            if (char.IsDigit(c))
                            {
                                int.TryParse(nomeArea, out indexArea);
                                nomeArea = ListaAreasDoZoo[indexArea - 1].getNomeArea();
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("A ÁREA NÃO SE ENCONTRA NA AREAS DA LISTA");

                    }

                } while (!verificaAreaExiste(nomeArea));

                if (verificaAreaExiste(nomeArea))
                {
                    indexArea = 1;
                    foreach (AreasDoZoo a in ListaAreasDoZoo)
                    {
                        string area = a.getNomeArea();
                        if (area == nomeArea)
                        {
                            if (GestorAnimais.verificaExisteNaArea(nomeArea))
                            {
                                Console.WriteLine("ESTA AREA TEM ANIMAIS E NAO PODE SER ELIMINADA");
                                break;
                            }
                            indexArea++;
                        }
                    }

                }else{
                    Console.WriteLine("A ÁREA NÃO SE ENCONTRA NA AREAS DA LISTA");
                }

            } while (verificaAreaExiste(nomeArea) == false);

            if (verificaAreaExiste(nomeArea))
            {
                char valor;
                do
                {
                    Console.Clear();
                    Console.WriteLine("TEM A CERTEZA QUE PRETENDE ELIMINAR ESTA AREA?\n");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine(string.Format("{0,7} | {1,-15} | {2,6} | {3,7} | {4,-20} ", "ID", "HABITATE", "CAPAC.", "N.FRONT", "FRONTEIRAS"));
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine(ListaAreasDoZoo[indexArea].ReturnAreasConsola());
                    Console.WriteLine("-----------------------------------------------------------------\n");
                    Console.WriteLine("Y/N?");
                    char.TryParse(Console.ReadLine(), out valor);
                } while (!(valor == 'y' || valor == 'Y' || valor == 'n' || valor == 'N'));


                if (valor == 'y' || valor == 'Y')
                {
                    nomeArea = ListaAreasDoZoo[indexArea].getNomeArea();
                    eliminarFronteira(nomeArea);
                    ListaAreasDoZoo.RemoveAt(indexArea);
                    Console.WriteLine("{0} ELIMINADA COM SUCESSO", nomeArea);

                    //REESCREVER NO FICHEIRO!
                    StreamWriter writer = new StreamWriter("AreasDoZoo.txt");

                    foreach (AreasDoZoo area in ListaAreasDoZoo)
                    {
                        writer.WriteLine("{0}", area.ReturnAreas());
                    }
                    writer.Close();
                }
                else
                {
                    Console.WriteLine("OPERAÇÃO CANCELADA");
                }
            }
            Console.WriteLine("\n<ENTER PARA VOLTAR AO MENU");
            Console.ReadLine();
            Console.Clear();
            MainClass.Menu();

        }//FIM ELIMINAR AREAS



        public static void eliminarFronteira(string eliminarFronteira)
        {
            foreach (AreasDoZoo area in ListaAreasDoZoo)
            {
                area.eliminarFronteira(eliminarFronteira);
            }
        }

        public static bool maxFronteiras(string areaID)
        {
            foreach (AreasDoZoo i in ListaAreasDoZoo)
            {
                if (i.maxFronteiras(areaID))
                    return true;
            }
            return false;
        }


        public static bool verificaAreaExiste(string nomeArea)
        {
            foreach (AreasDoZoo area in ListaAreasDoZoo)
            {
                if (area.getNomeArea() == nomeArea)
                {
                    return true;
                }
            }
            return false;
        }


        public static string GetHabitateArea(string area)
        {
            foreach (AreasDoZoo a in ListaAreasDoZoo)
            {
                if (a.getNomeArea() == area)
                {
                    return a.getHabitateArea();
                }
            }
            return null;
        }

        public static bool VerificaSeAreaEAdjacente(string area){
            foreach(AreasDoZoo a in ListaAreasDoZoo)
            {
                if (a.VerificaSeAreaEAdjacente(area))
                    return true;
            }
            return false;
        }

    }//FIM CLASSE

}

