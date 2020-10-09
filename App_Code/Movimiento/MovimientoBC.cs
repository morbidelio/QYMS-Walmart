using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de MovimientoBC
/// </summary>
public class MovimientoBC : MovimientoTable
{
    readonly SqlTransaccionMovimiento tran = new SqlTransaccionMovimiento();
	public MovimientoBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
    }

    public DataTable obtenerTodoMovEstSubTipo()
    {
        return tran.MovEstSubTipo_ObtenerTodo();
    }

    public DataTable obtenerXEstadoMovEstSubTipo()
    {
        return tran.MovEstSubTipo_ObtenerXEstado(this.ID_ESTADO, true);
    }

    public DataTable obtenerXEstadoMovEstSubTipo(int moes_id)
    {
        return tran.MovEstSubTipo_ObtenerXEstado(moes_id);
    }

    public DataTable obtenerXEstadoMovEstSubTipo(int moes_id, bool moet_activo)
    {
        return tran.MovEstSubTipo_ObtenerXEstado(moes_id, moet_activo);
    }

    public DataTable obtenerTodoMovimiento()
    {
        return tran.Movimiento_ObtenerTodos();
    }

    public DataTable obtenerMovimientoControl(int site_id)
    {
        return tran.Movimiento_ObtenerControl(site_id);
    }

    public MovimientoBC obtenerXID(int id)
    {
        return tran.Movimiento_ObtenerXId(id);
    }

    public DataTable obtenerXParametro(string placa, bool externo)
    {
        return tran.Movimiento_ObtenerXParametro(placa, externo);
    }

    public MovimientoBC obtenerXPlaca(string placa)
    {
        return tran.Movimiento_ObtenerXPlaca(placa);
    }

    public bool ProcesoEntrada(MovimientoBC mov, TrailerUltEstadoBC traiUE, TrailerBC trai, int usua_id, out string resultado)
    {
        return tran.Movimiento_Entrada(mov, traiUE, trai, usua_id,out resultado);
    }

    public bool MOVIMIENTO(MovimientoBC mov, TrailerUltEstadoBC traiUE, TrailerBC trai, int usua_id, out string resultado)
    {
        return tran.Movimiento_Interno(mov, traiUE, trai,usua_id, out resultado);
    }    
    
    public bool MOVIMIENTO(MovimientoBC mov, int site_id, int usua_id, out string resultado)
    {
        return tran.Movimiento_Interno(mov, site_id, usua_id, out resultado);
    }

    public bool MOVIMIENTO_automatico_estacinamiento(MovimientoBC mov, int site_id, int usua_id, out string resultado)
    {
        return tran.Movimiento_automatico_estacionamiento(mov, site_id, usua_id, out resultado);
    }

    public bool MOVIMIENTO(int site_id, int usua_id, out string resultado)
    {
        return tran.Movimiento_Interno(this, site_id, usua_id, out resultado);
    }
    
    public bool Modificar(MovimientoBC movimiento)
    {
        return tran.Movimiento_Modificar(movimiento);
    }

    public bool ModificarDestino(int id, int id_destino, int usua_id, out string resultado)
    {
        return tran.Movimiento_ModificarDestino(id, id_destino,  usua_id, out  resultado);
    }

    public bool Eliminar(int id)
    {
        return tran.Movimiento_Eliminar(id);
    }

    public bool Anular(int id, int moet_id,string o, int usua_id,out string resultado)
    {
        //return tran.Movimiento_Anular(id, moet_id);
        return tran.Movimiento_Confirmar(id, usua_id, o, 0, moet_id, 60, out resultado);
    }

    public bool Confirmar(int id, int usua_id, string o, int luga_id, int moet_id,  out string resultado)
    {
        return tran.Movimiento_Confirmar(id,usua_id,o,luga_id,moet_id, 40,out resultado);
    }

    public bool CambiarOrden(int id, bool subir)
    {
        return tran.Movimiento_CambiarOrden(id, subir);
    }

    public bool CambiarOrden(bool subir)
    {
        return tran.Movimiento_CambiarOrden(this.ID, subir);
    }

    public int MinOrden(int site_id)
    {
        return tran.Movimiento_MinOrden(site_id);
    }

    public int MaxOrden(int site_id)
    {
        return tran.Movimiento_MaxOrden(site_id);
    }

    public DataTable ObtenerTipos()
    {
        return tran.Movimiento_ObtenerTipos();
    }
}