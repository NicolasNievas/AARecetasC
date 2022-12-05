using Final_Recetas_Prog2.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Recetas_Prog2.Servicios.Interfaces
{
    public interface IServicio
    {
        int ProximaReceta();
        List<Ingrediente> ObtenerIngredientes();
        bool ConfirmarReceta(Receta nuevo);
    }
}
