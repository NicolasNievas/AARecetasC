using Final_Recetas_Prog2.Datos.Interfaces;
using Final_Recetas_Prog2.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Recetas_Prog2.Datos.Implementaciones
{
    public class RecetaDao : IRecetaDao
    {
        public int ObtenerProximoID()
        {
            return HelperDao.ObtenerInstancia().ObtenerProximoID("SP_PROXIMO_ID", "@next");
        }

        public bool Save(Receta nuevo)
        {
            return HelperDao.ObtenerInstancia().GrabarReceta(nuevo, "SP_INSERTAR_RECETA", "SP_INSERTAR_DETALLES");
        }

        public List<Ingrediente> ToGetIngrediente()
        {
            List<Ingrediente> listIngrediente = new List<Ingrediente>();
            DataTable dt = HelperDao.ObtenerInstancia().cargarCombo("SP_CONSULTAR_INGREDIENTES");
            foreach(DataRow dr in dt.Rows)
            {
                Ingrediente i = new Ingrediente();
                i.IngredienteID = (int)dr["id_ingrediente"];
                i.Nombre = dr["n_ingrediente"].ToString();
                i.UnidadMedida = dr["unidad_medida"].ToString();
                listIngrediente.Add(i);
            }
            return listIngrediente;
        }
    }
}
