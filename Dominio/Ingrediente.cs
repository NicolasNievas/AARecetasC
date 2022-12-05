using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Recetas_Prog2.Dominio
{
    public class Ingrediente
    {
        public Ingrediente()
        {
        }

        public Ingrediente(int ingredienteID, string nombre, string unidadMedida)
        {
            IngredienteID = ingredienteID;
            Nombre = nombre;
            UnidadMedida = unidadMedida;
        }
        public int IngredienteID { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; }

        public override string ToString()
        {
            return Nombre; 
        }
    }
}
