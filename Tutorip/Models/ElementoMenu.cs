using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorip.Models
{
    public class ElementoMenu
    {
        public String pathImmagine { get; set; }
        public String testo { get; set; }
        public ElementoMenu(String pathImmagine, String testo)
        {
            this.pathImmagine = pathImmagine;
            this.testo = testo;
        }
    }
}
