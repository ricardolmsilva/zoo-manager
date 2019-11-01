using System;
using System.Collections.Generic;
using System.IO;

namespace Zoologico
{
    //Class identificadora das Especies
    class Especies
    {
        string IDEspecie;
        List<Habitates> EspecieHabitates;

        //Construtor da class Especies
        public Especies(string IDEspecie, List<Habitates> EspecieHabitates)
        {
            this.IDEspecie = IDEspecie;
            this.EspecieHabitates = EspecieHabitates;
        }


        public string GetID()
        {
            return this.IDEspecie;
        }


        public string GetHabitates()
        {
            string stringHabitates = "";

            foreach (Habitates s in EspecieHabitates)
            {
                stringHabitates += s.ToString() + " ";
            }
            if (stringHabitates.Length > 0)
            {
                stringHabitates = stringHabitates.Remove(stringHabitates.LastIndexOf(" "));
            }

            return string.Format("{1,-15}", stringHabitates);
        }


        public string ImprimirEspecie()
        {
            string stringEspecie = "";

            foreach (Habitates s in EspecieHabitates)
            {
                stringEspecie += s.ToString() + " ";
            }

            if (stringEspecie.Length > 0)
            {
                stringEspecie = stringEspecie.Remove(stringEspecie.LastIndexOf(" "));
            }

            return string.Format("{0,7} | {1,-15}", IDEspecie, stringEspecie);
        }



        public void AdicionaHabitat(List<Habitates> h)
        {
            foreach (Habitates a in h)
            {
                if (!(EspecieHabitates.Contains(a)))
                {
                    EspecieHabitates.Add(a);
                }
            }
        }

    }
}