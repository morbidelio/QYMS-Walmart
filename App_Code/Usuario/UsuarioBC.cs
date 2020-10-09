using System;
using System.Collections.Generic;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;
using System.Collections;


/// <summary>
/// Descripción breve de UsuarioBC
/// </summary>
public class UsuarioBC : UsuarioTable
{
    SqlTransactionUsuario tran = new SqlTransactionUsuario();
    private bool _logueado;
    public bool LOGUEADO
    {
        get { return _logueado; }
        set { _logueado = value; }
    }

    public UsuarioBC()
    {

    }

    public bool Crear(UsuarioBC usuario)
    {
        SqlTransactionUsuario tran = new SqlTransactionUsuario();
        return tran.UsuarioCrear(usuario);
    }

    public bool CambioPass(UsuarioBC usuario)
    {
        SqlTransactionUsuario tran = new SqlTransactionUsuario();
        return tran.CambioPass(usuario);
    }

    public DataTable ObtenerXSite(int site_id)
    {
        SqlTransactionUsuario tran = new SqlTransactionUsuario();
        return tran.UsuarioObtenerXSite(site_id);
    }


    public DataTable ObtenerTodos()
    {
        SqlTransactionUsuario tran = new SqlTransactionUsuario();
        return tran.UsuarioObtenerTodos();
    }

    public DataTable ObtenerPerfilesAutorizados()
    {
        return tran.Perfil_ObtenerAutorizados(this.ID);
    }

    public DataTable ObtenerAutorizados(int id)
    {
        return tran.Perfil_ObtenerAutorizados(id);
    }

    public UsuarioBC ObtenerPorId(int id)
    {
        return tran.UsuarioObtenerPorId(id);
    }

    public UsuarioBC ObtenerPorRut(string rut)
    {
        return tran.UsuarioObtenerPorRut(rut);
    }

    public UsuarioBC ObtenerPorUsername(string username)
    {
        return tran.obtenerPorUsername(username);
    }

    public bool Modificar(UsuarioBC usuario)
    {
        return tran.UsuarioModificar(usuario);
    }

    public bool ModificarPass(UsuarioBC usuario)
    {
        return tran.UsuarioModificarPass(usuario);
    }

    public bool Eliminar(int id)
    {
        return tran.EliminarUsuario(id);
    }

    public UsuarioBC Login(UsuarioBC usuario)
    {
        usuario.LOGUEADO = false;
        if (tran.LoguearUsuario(usuario.USERNAME, usuario.PASSWORD).Rows.Count > 0)
        {
            usuario = usuario.ObtenerPorId(int.Parse(tran.LoguearUsuario(usuario.USERNAME, usuario.PASSWORD).Rows[0]["USUA_ID"].ToString()));
            usuario.LOGUEADO = true;
        }
        return usuario;
    }

    public UsuarioBC Loginproveedor(UsuarioBC usuario)
    {
        usuario.LOGUEADO = false;
        if (tran.LoguearUsuarioproveedor(usuario.USERNAME, usuario.PASSWORD, usuario.PROVEEDOR).Rows.Count > 0)
        {
            usuario = usuario.ObtenerPorId(int.Parse(tran.LoguearUsuarioproveedor(usuario.USERNAME, usuario.PASSWORD, usuario.PROVEEDOR).Rows[0]["USUA_ID"].ToString()));
            usuario.LOGUEADO = true;
        }
        return usuario;
    }

    public bool Desactivar()
    {
        return tran.UsuarioDesactivar(this.ID);
    }
    
    public bool Desactivar(int id)
    {
        return tran.UsuarioDesactivar(id);
    }

    public bool Activar()
    {
        return tran.UsuarioActivar(this.ID);
    }
    
    public bool Activar(int id)
    {
        return tran.UsuarioActivar(id);
    }

    public DataTable ObtenerLugaresAsignados(int usua_id)
    {
        return tran.UsuarioObtenerAsignados(usua_id);
    }

    public DataTable ObtenerUsuariosRemolcador(int site_id = 0)
    {
        return tran.UsuarioObtenerXTipo(1020, site_id);
    }

    public DataTable ObtenerUsuariosGuardia(int site_id = 0)
    {
        return tran.UsuarioObtenerXTipo(15, site_id);
    }

    public DataTable ObtenerUsuariosXTIPO(int tipo, int site_id)
    {
        return tran.UsuarioObtenerXTipo(tipo, site_id);
    }



    public DataTable ObtenerXParametro(String rut, String nombre, String apellido, bool solo_activos, int id_tipo_usuario)
    {
        return tran.UsuarioObtenerXParametro(rut, nombre, apellido, solo_activos, id_tipo_usuario);
    }

    public bool AsignarLugar(int usua_id, string luga_id)
    {
        SqlTransactionUsuario tran = new SqlTransactionUsuario();
        return tran.UsuarioAsignarLugares(usua_id, luga_id);
    }



}

