using Final_Recetas_Prog2.Datos.Implementaciones;
using Final_Recetas_Prog2.Datos.Interfaces;
using Final_Recetas_Prog2.Dominio;
using Final_Recetas_Prog2.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Recetas_Prog2.Servicios.Implementaciones
{
    internal class ServicioReceta : IServicio
    {
        private IRecetaDao dao;

        public ServicioReceta()
        {
            dao = new RecetaDao();
        }

        public bool ConfirmarReceta(Receta nuevo)
        {
            return dao.Save(nuevo);
        }

        public List<Ingrediente> ObtenerIngredientes()
        {
            return dao.ToGetIngrediente();
        }

        public int ProximaReceta()
        {
            return dao.ObtenerProximoID();
        }
    }
}
