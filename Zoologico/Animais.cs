using System;
using System.Collections.Generic;
using System.IO;

namespace Zoologico
{
    //Class identificadora dos Animais
    class Animais
    {
        int IDAnimal;
        string Nome;
        double Peso;
        string IDEspécie;
        string localizacao;

        List<string> ListaPais;
        List<string> ListaFilhos;

        static int ContadorAnimal = 0;

        //Construtor da class Animais
        public Animais(string Nome, double Peso, string IDEspécie, string localizacao, List<string> ListaPais, List<string> ListaFilhos )
        {
            this.IDAnimal = ++ContadorAnimal;   //Incrementação do IDAnimal
            this.Nome = Nome;
            this.Peso = Peso;
            this.IDEspécie = IDEspécie;
            this.localizacao = localizacao;
            this.ListaPais = ListaPais;
            this.ListaFilhos = ListaFilhos;
        }

        public string getNomeAnimal()
        {
            return this.Nome;
        }

        public string getEspecieAnimal()
        {
            return this.IDEspécie;
        }

        public string getLocalizacaoAnimal()
        {
            return this.localizacao;
        }

        public double GetPeso(){
            return this.Peso;
        }

        public string ImprimirConsola()  //Método para Imprir Areas na Consola
        {

            string stringAnimal = "";

            foreach (string s in ListaPais)
            {
                stringAnimal += s + " ";
            }
            foreach (string s in ListaFilhos)
            {
                stringAnimal += s + " ";
            }

            if (stringAnimal.Length > 0)
            {
                stringAnimal = stringAnimal.Remove(stringAnimal.LastIndexOf(" "));
            }

            return string.Format("{0,7} | {1,-15} | {2,6} | {3,7} | {4,7} | {5,-20} ", IDAnimal, Nome, Peso, IDEspécie, localizacao, stringAnimal);

        }//Fim ImprimirAreas na Consola
    }
}