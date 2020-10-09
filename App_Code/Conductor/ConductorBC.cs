using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ConductorBC
/// </summary>
public class ConductorBC : ConductorTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

    public ConductorBC()
    {

    }

    public ConductorBC(int id)
    {
        this.ID = id;
        ConductorBC c = ObtenerXId();
        this.RUT = c.RUT;
        this.NOMBRE = c.NOMBRE;
        this.TRAN_ID = c.TRAN_ID;
        this.IMAGEN = c.IMAGEN;
        this.BLOQUEADO = c.BLOQUEADO;
        this.ACTIVO = c.ACTIVO;
        this.COND_EXTRANJERO = c.COND_EXTRANJERO;
    }

    public ConductorBC(string rut)
    {
        this.RUT = rut;
        ConductorBC c = ObtenerXRut();
        this.ID = c.ID;
        this.NOMBRE = c.NOMBRE;
        this.TRAN_ID = c.TRAN_ID;
        this.IMAGEN = c.IMAGEN;
        this.BLOQUEADO = c.BLOQUEADO;
        this.ACTIVO = c.ACTIVO;
        this.COND_EXTRANJERO = c.COND_EXTRANJERO;
    }

    public DataTable ObtenerXParametro(string rut = null, string nombre = null, int tran_id = 0)
    {
        return tran.Conductor_ObtenerXParametro(rut, nombre, tran_id);
    }

    public ConductorBC ObtenerXId(int id)
    {
        return tran.Conductor_ObtenerXId(id);
    }

    public ConductorBC ObtenerXId()
    {
        return tran.Conductor_ObtenerXId(this.ID);
    }

    public ConductorBC ObtenerXRut(string rut)
    {
        return tran.Conductor_ObtenerXRut(rut);
    }

    public ConductorBC ObtenerXRut()
    {
        return tran.Conductor_ObtenerXRut(this.RUT);
    }
    
    public ConductorBC ObtenerXRutSAP(string rut)
    {
        return tran.Conductor_ObtenerXRutSAP(rut);
    }

    public ConductorBC ObtenerXRutSAP()
    {
        return tran.Conductor_ObtenerXRutSAP(this.RUT);
    }

    public bool Agregar(ConductorBC c)
    {
        return tran.Conductor_Agregar(c);
    }

    public bool Agregar()
    {
        return tran.Conductor_Agregar(this);
    }

    public int AgregarIdentity(ConductorBC c)
    {
        return tran.Conductor_AgregarIdentity(c);
    }

    public int AgregarIdentity()
    {
        return tran.Conductor_AgregarIdentity(this);
    }

    public bool Modificar(ConductorBC c)
    {
        return tran.Conductor_Modificar(c);
    }

    public bool Modificar()
    {
        return tran.Conductor_Modificar(this);
    }

    public bool Eliminar()
    {
        return tran.Conductor_Eliminar(this.ID);
    }

    public bool Eliminar(int id)
    {
        return tran.Conductor_Eliminar(id);
    }

    public bool Bloquear(int id, string motivo, int id_user)
    {
        return tran.Conductor_Bloquear(id, motivo, id_user);
    }

    public bool Bloquear(int id_user)
    {
        return tran.Conductor_Bloquear(this.ID, this.MOTIVO_BLOQUEO, id_user);
    }

    public bool Activar(int id)
    {
        return tran.Conductor_Activar(id);
    }

    public bool Activar()
    {
        return tran.Conductor_Activar(this.ID);
    }

    public bool AgregarFoto(int id, string imagen)
    {
        return tran.Conductor_AgregarFoto(id, imagen);
    }

    public bool AgregarFoto()
    {
        return tran.Conductor_AgregarFoto(this.ID, this.IMAGEN);
    }
}