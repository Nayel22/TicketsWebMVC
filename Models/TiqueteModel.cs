namespace TicketsWebMVC.Models
{
    public class TiqueteModel
    {
        public int ti_identificador { get; set; }
        public string ti_asunto { get; set; }
        public int ti_ca_id { get; set; }
        public int ti_us_id_asigna { get; set; }
        public int ti_ur_id { get; set; }
        public int ti_im_id { get; set; }
        public string ti_estado { get; set; }
        public string? ti_solucion { get; set; }
        public DateTime ti_fecha_adicion { get; set; }
        public string ti_adicionado_por { get; set; }
        public DateTime? ti_fecha_modificacion { get; set; }
        public string? ti_modificado_por { get; set; }
    }
}
