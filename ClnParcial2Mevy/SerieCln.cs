using CadParcial2Mevy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnParcial2Mevy
{
    public class SerieCln
    {
        public static int Insertar(Serie serie)
        {
            using (var context = new Parcial2MevyEntities1())
            {
                context.Serie.Add(serie);
                context.SaveChanges();
                return serie.id;
            }
        }

        public static int Actualizar(Serie serie)
        {
            using (var context = new Parcial2MevyEntities1())
            {
                var existente = context.Serie.Find(serie.id);
                if (existente != null)
                {
                    existente.titulo = serie.titulo;
                    existente.sinopsis = serie.sinopsis;
                    existente.director = serie.director;
                    existente.episodios = serie.episodios;
                    existente.fecha_estreno = serie.fecha_estreno;
                    existente.estado = serie.estado;
                    existente.idioma_principal = serie.idioma_principal;
                    return context.SaveChanges();
                }
                return 0;
            }
        }
        public static int Eliminar(int id, string usuario)
        {
            using (var context = new Parcial2MevyEntities1())
            {
                var serie = context.Serie.Find(id);
                if (serie != null)
                {
                    serie.estado = -1;
                    return context.SaveChanges();
                }
                return 0;
            }
        }
        public static Serie ObtenerUno(int id)
        {
            using (var context = new Parcial2MevyEntities1())
            {
                return context.Serie.Find(id);
            }
        }

        public static List<Serie> Listar()
        {
            using (var context = new Parcial2MevyEntities1())
            {
                return context.Serie.Where(x => x.estado != -1).ToList();
         
            }
        }

        public static List<paSerieListar_Result> ListarPa(string parametro)
        {
            using (var context = new Parcial2MevyEntities1())
            {
                return context.paSerieListar(parametro).ToList();
            }
        }
    }
}
