using Qanalytics.Data.Access.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerUltSalidaLABC
/// </summary>
public class TrailerUltSalidaLABC : TrailerUltSalidaLATable
{
    readonly SqlTransaccionMovimiento tran = new SqlTransaccionMovimiento();

    public TrailerUltSalidaLABC()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public bool ProcesoSalida(TrailerUltSalidaLABC t, DataTable detalle, string locales, int id_usuario, out string resultado)
    {
        return tran.TrailerUltEstado_ProcesoSalidaLoAguirre(t, detalle, locales, id_usuario, out resultado);
    }

}