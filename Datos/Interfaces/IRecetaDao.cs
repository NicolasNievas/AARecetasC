using Final_Recetas_Prog2.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Recetas_Prog2.Datos.Interfaces
{
    public interface IRecetaDao
    {
        int ObtenerProximoID();
        List<Ingrediente> ToGetIngrediente();
        bool Save(Receta nuevo);
    }
}
