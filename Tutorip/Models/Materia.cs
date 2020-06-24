using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Markup;

namespace Tutorip.Models
{
    public class Materia
    {
        public String nome { get; set; }

        public Materia (String nome)
        {
            this.nome = nome;
        }

        public String toString()
        {
            return this.nome;
        }
    }
}
