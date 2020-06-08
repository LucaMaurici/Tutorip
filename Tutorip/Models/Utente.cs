using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    public class Utente
    {
        public Utente(string nome, string cognome)
        {
            this.nome = nome;
            this.cognome = cognome;
        }
        public int id { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public int età { get; set; }
        public Posizione posizione{ get; set; }
        public Scuola scuola { get; set; }

    }
}
