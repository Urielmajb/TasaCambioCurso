using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using System.Web;
using DAL.WsTasaDeCambio;
using System.Web.Services;
using System.ServiceModel;
using System.Xml.Linq;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Entity;

namespace DAL
{
    [Table("TasaCambio")]
    public partial class TasaCambio
    {

        [Key]

        public int ID { get; set; }
       
        public string Valor { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public string Fecha { get; set; }

        public static IEnumerable<TasaCambio> ObtenerTasaCambio()
        {
            try
            {
                using (WsTasaDeCambio.Tipo_Cambio_BCNSoapClient TipoCambio = new Tipo_Cambio_BCNSoapClient())
                {
                    var root = TipoCambio.RecuperaTC_Mes(DateTime.Today.Year, DateTime.Today.Month);


                    var Result = root.Elements().Select(x => new TasaCambio
                    {
                        Fecha = (x.Element("Fecha").Value),
                        Valor = (x.Element("Valor").Value.ToString())

                    }).ToArray();

                    return Result;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message); ;
            }

        }

        public static List<TasaCambio> GetTasa(int Year, int Month)
        {
            try
            {
                using (WsTasaDeCambio.Tipo_Cambio_BCNSoapClient TipoCambio = new Tipo_Cambio_BCNSoapClient())
                {
                    var root = TipoCambio.RecuperaTC_Mes(Year, Month);

                    var Result = root.Elements().Select(x => new TasaCambio
                    {
                        Fecha = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(x.Element("Fecha").Value)),
                        Valor = (x.Element("Valor").Value.ToString())

                    }).ToList();

                    return Result;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

    }
}
