using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VisualSoftErp.Clases
{
    public class articulosCL
    {
        #region Propiedades
        string strCnn = globalCL.gv_strcnn;
        public int intArticulosID { get; set; }
        public string strArticulo { get; set; }
        public string strNombre { get; set; }
        public string strNombreOC { get; set; }
        public int intFamiliasID { get; set; }
        public int intUnidadesdemedidaID { get; set; }
        public decimal dPtjeIva { get; set; }
        public decimal dPtjeIeps { get; set; }
        public int intManejaExistencia { get; set; }
        public string strClaveSat { get; set; }
        public string strImagen { get; set; }
        public int intActivo { get; set; }
        public int intTiempodeentrega { get; set; }
        public int intTiposdearticuloID { get; set; }
        public int intMaximo { get; set; }
        public int intMinimo { get; set; }
        public int intReorden { get; set; }
        public DateTime fFechaalta { get; set; }
        public string strFraccionArancelaria { get; set; }
        public string strUnidadAduana { get; set; }
        public decimal dKilosAduana { get; set; }
        public string strUM { get; set; }
        public string strUMclavesat { get; set; }
        public int intEsunkit { get; set; }

        public string strPathImagen { get; set; }
        public string strBreveDescripcion { get; set; }
        public int intTiendaonline { get; set; }
        public int intDisponibleTiendaonline { get; set; }
        public int intSubFamiliaID { get; set; }
        public decimal dPrecioventa { get; set; }

        public int intObsoleto { get; set; }
        public decimal dMargenarribadelcosto { get; set; }
        public int intProveedoresID { get; set; }
        public string strMonedasID { get; set; }
        public int intUnidaddemedidasecundaria { get; set; }
        public decimal decFactorUM2 { get; set; }
        public decimal dGrupodearticulos { get; set; }
        public int intDiasstock { get; set; }
        public string strEanAmece { get; set; }
        public string strUmAmece { get; set; }
        public int intExcluirdereportes { get; set; }
        public int intMarcasID { get; set; }
        public int intManejapedimentos { get; set; }
        public string strClasificacion { get; set; }
        public string strCodigodebarras { get; set; }
        public string strUbicación { get; set; }
        public decimal dFactorauxiliar { get; set; }
        public decimal dMetroslinealesunidadesporpieza { get; set; }
        public int intArticulobaseparacosteoID { get; set; }
        public int intCosteodirectoenfactura { get; set; }
        public int intRedondear { get; set; }
        public int intSeCompraEnRollos { get; set; }
        public int intClasificacionproduccion { get; set; }
        public int intTipodecorte { get; set; }
        public decimal dAncho { get; set; }
        public decimal dLargo { get; set; }
        public decimal intPtjeIva { get; set; }
        public decimal intPtjeIeps { get; set; }

        public string strArtBaseCosteoDX { get; set; }
        public string strArtBase { get; set; }
        public int iFlexArtArribosProveedor { get; set; }
        public int iFlexArtCompras { get; set; }
        public int iFlexArtMovimientos { get; set; }
        public int iFlexArtVentas { get; set; }

        public decimal dPesoKg { get; set; }
        public decimal dDimenLargo { get; set; }
        public decimal dDimenAlto { get; set; }
        public decimal dDimenAncho { get; set; }

        public decimal dcosto { get; set; }
        public decimal dtipodecambio { get; set; }
        public string strObs { get; set; }

        public int intActivos { get; set; }
        public int intLinea { get; set; }
        public int intFam { get; set; }
        public string strConsecutivo { get; set; }

        #endregion

        #region Constructor
        public articulosCL()
        {
            intLinea = 0;
            intFam=0;
            dcosto = 0;
            dtipodecambio = 0;
            strObs = string.Empty;

            intArticulosID = 0;
            strArticulo = string.Empty;
            strNombre = string.Empty;
            strNombreOC = string.Empty;
            intFamiliasID = 0;
            intUnidadesdemedidaID = 0;
            intManejaExistencia = 0;
            strClaveSat = string.Empty;
            strImagen = string.Empty;
            intActivo = 0;
            intTiempodeentrega = 0;
            intTiposdearticuloID = 0;
            intMaximo = 0;
            intMinimo = 0;
            intReorden = 0;
            DateTime fFechaalta = DateTime.Now;
            strFraccionArancelaria = string.Empty;
            strUnidadAduana = string.Empty;
            dKilosAduana = 0;
            strUM = string.Empty;

           strPathImagen = string.Empty;
            strBreveDescripcion = string.Empty;
            intTiendaonline = 0;
            intDisponibleTiendaonline = 0;
            intSubFamiliaID = 0;
            dPrecioventa = 0;
            strArtBaseCosteoDX = string.Empty;
            dPesoKg = 0;
            dDimenLargo = 0;
            dDimenAlto = 0;
            dDimenAncho = 0;
    }
        #endregion

        #region Metodos
        public DataTable articulosUbicaciones()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosListaParaUbicacion";
                cmd.Parameters.AddWithValue("@prmLinea", intLinea);
                cmd.Parameters.AddWithValue("@prmFam", intFam);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //articulosGrid
        public DataTable articulosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosGRID";
                cmd.Parameters.AddWithValue("@prmActivo", intActivo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception ex) { return dt; }
        } //articulosGrid

        public string ArticulosCrud()
        {
            try
            {
                string result = string.Empty;
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosCRUD";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmArticulo", strArticulo);
                cmd.Parameters.AddWithValue("@prmNombre", strNombre);
                cmd.Parameters.AddWithValue("@prmNombreOC", strNombreOC);
                cmd.Parameters.AddWithValue("@prmFamiliasID", intFamiliasID);
                cmd.Parameters.AddWithValue("@prmUnidadesdemedidaID", intUnidadesdemedidaID);
                cmd.Parameters.AddWithValue("@prmPtjeIva", dPtjeIva);
                cmd.Parameters.AddWithValue("@prmPtjeIeps", dPtjeIeps);
                cmd.Parameters.AddWithValue("@prmManejaExistencia", intManejaExistencia);
                cmd.Parameters.AddWithValue("@prmClaveSat", strClaveSat);
                cmd.Parameters.AddWithValue("@prmImagen", strImagen);
                cmd.Parameters.AddWithValue("@prmActivo", intActivo);
                cmd.Parameters.AddWithValue("@prmTiempodeentrega", intTiempodeentrega);
                cmd.Parameters.AddWithValue("@prmTiposdearticuloID", intTiposdearticuloID);
                cmd.Parameters.AddWithValue("@prmMaximo", intMaximo);
                cmd.Parameters.AddWithValue("@prmMinimo", intMinimo);
                cmd.Parameters.AddWithValue("@prmReorden", intReorden);
                cmd.Parameters.AddWithValue("@prmFechaalta", fFechaalta);
                cmd.Parameters.AddWithValue("@prmFraccionArancelaria", strFraccionArancelaria);
                cmd.Parameters.AddWithValue("@prmUnidadAduana", strUnidadAduana);
                cmd.Parameters.AddWithValue("@prmKilosAduana", dKilosAduana);
                cmd.Parameters.AddWithValue("@prmEsunkit", intEsunkit);
                cmd.Parameters.AddWithValue("@prmObsoleto", intObsoleto);
                cmd.Parameters.AddWithValue("@prmMargenarribadelcosto", dMargenarribadelcosto);
                cmd.Parameters.AddWithValue("@prmPathImagen", strPathImagen);
                cmd.Parameters.AddWithValue("@prmTiendaonline", intTiendaonline);
                cmd.Parameters.AddWithValue("@prmSubFamiliasID", intSubFamiliaID);
                cmd.Parameters.AddWithValue("@prmPrecioventa", dPrecioventa);
                cmd.Parameters.AddWithValue("@prmProveedoresID", intProveedoresID);
                cmd.Parameters.AddWithValue("@prmMonedasID", strMonedasID);
                cmd.Parameters.AddWithValue("@prmUnidaddemedidasecundaria", intUnidaddemedidasecundaria);
                cmd.Parameters.AddWithValue("@prmFactorUM2", decFactorUM2);
                cmd.Parameters.AddWithValue("@prmGrupodearticulos", dGrupodearticulos);
                cmd.Parameters.AddWithValue("@prmDiasstock", intDiasstock);
                cmd.Parameters.AddWithValue("@prmEanAmece", strEanAmece);
                cmd.Parameters.AddWithValue("@prmUmAmece", strUmAmece);
                cmd.Parameters.AddWithValue("@prmExcluirdereportes", intExcluirdereportes);
                cmd.Parameters.AddWithValue("@prmMarcasID", intMarcasID);
                cmd.Parameters.AddWithValue("@prmManejapedimentos", intManejapedimentos);
                cmd.Parameters.AddWithValue("@prmClasificacion", strClasificacion);
                cmd.Parameters.AddWithValue("@prmCodigodebarras", strCodigodebarras);
                cmd.Parameters.AddWithValue("@prmUbicación", strUbicación);
                cmd.Parameters.AddWithValue("@prmFactorauxiliar", dFactorauxiliar);
                cmd.Parameters.AddWithValue("@prmMetroslinealesunidadesporpieza", dMetroslinealesunidadesporpieza);
                cmd.Parameters.AddWithValue("@prmArticulobaseparacosteoID", intArticulobaseparacosteoID);
                cmd.Parameters.AddWithValue("@prmCosteodirectoenfactura", intCosteodirectoenfactura);
                cmd.Parameters.AddWithValue("@prmRedondear", intRedondear);
                cmd.Parameters.AddWithValue("@prmClasificacionproduccion", intClasificacionproduccion);
                cmd.Parameters.AddWithValue("@prmTipodecorte", intTipodecorte);
                cmd.Parameters.AddWithValue("@prmAncho", dAncho);
                cmd.Parameters.AddWithValue("@prmLargo", dLargo);
                cmd.Parameters.AddWithValue("@prmArtBaseCosteoDX", strArtBaseCosteoDX);
                cmd.Parameters.AddWithValue("@prmDisponibleentienda", intDisponibleTiendaonline);
                cmd.Parameters.AddWithValue("@prmKilos", dPesoKg);
                cmd.Parameters.AddWithValue("@prmDimenLargo", dDimenLargo);
                cmd.Parameters.AddWithValue("@prmDimenAlto", dDimenAlto);
                cmd.Parameters.AddWithValue("@prmDimenAncho", dDimenAncho);
                cmd.Parameters.AddWithValue("@prmSeCompraEnRollos", intSeCompraEnRollos);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return "ArticulosCrud: "+ex.Message;
            }
        } //ArticulosCrud

        public string articulosLlenaCajas()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articulosllenaCajas";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    intArticulosID = Convert.ToInt32(dr["ArticulosID"]);
                    strArticulo = dr["Articulo"].ToString();
                    strNombre = dr["Nombre"].ToString();
                    strNombreOC = dr["NombreOC"].ToString();
                    intFamiliasID = Convert.ToInt32(dr["FamiliasID"]);
                    intUnidadesdemedidaID = Convert.ToInt32(dr["UnidadesdemedidaID"]);
                    dPtjeIva = Convert.ToInt32(dr["PtjeIva"]);
                    dPtjeIeps = Convert.ToInt32(dr["PtjeIeps"]);
                    intManejaExistencia = Convert.ToInt32(dr["ManejaExistencia"]);
                    strClaveSat = dr["ClaveSat"].ToString();
                    strUMclavesat = dr["UmSat"].ToString();
                    strImagen = dr["Imagen"].ToString();
                    intActivo = Convert.ToInt32(dr["Activo"]);
                    intTiempodeentrega = Convert.ToInt32(dr["Tiempodeentrega"]);
                    intTiposdearticuloID = Convert.ToInt32(dr["TiposdearticuloID"]);
                    intMaximo = Convert.ToInt32(dr["Maximo"]);
                    intMinimo = Convert.ToInt32(dr["Minimo"]);
                    intReorden = Convert.ToInt32(dr["Reorden"]);
                    fFechaalta = Convert.ToDateTime(dr["Fechaalta"]);
                    strFraccionArancelaria = dr["FraccionArancelaria"].ToString();
                    strUnidadAduana = dr["UnidadAduana"].ToString();
                    dKilosAduana = Convert.ToDecimal(dr["KilosAduana"]);
                    intEsunkit = Convert.ToInt32(dr["Esunkit"]);
                    intObsoleto = Convert.ToInt32(dr["Obsoleto"]);
                    dMargenarribadelcosto = Convert.ToDecimal(dr["Margenarribadelcosto"]);
                    intProveedoresID = Convert.ToInt32(dr["ProveedoresID"]);
                    strMonedasID = dr["MonedasID"].ToString();
                    intUnidaddemedidasecundaria = Convert.ToInt32(dr["Unidaddemedidasecundaria"]);
                    decFactorUM2 = Convert.ToDecimal(dr["FactorUM2"]);
                    dGrupodearticulos = Convert.ToDecimal(dr["Grupodearticulos"]);
                    intDiasstock = Convert.ToInt32(dr["Diasstock"]);
                    strEanAmece = dr["EanAmece"].ToString();
                    strUmAmece = dr["UmAmece"].ToString();
                    intExcluirdereportes = Convert.ToInt32(dr["Excluirdereportes"]);
                    intMarcasID = Convert.ToInt32(dr["MarcasID"]);
                    intManejapedimentos = Convert.ToInt32(dr["Manejapedimentos"]);
                    strClasificacion = dr["Clasificacion"].ToString();
                    strCodigodebarras = dr["Codigodebarras"].ToString();
                    strUbicación = dr["Ubicación"].ToString();
                    dFactorauxiliar = Convert.ToDecimal(dr["Factorauxiliar"]);
                    dMetroslinealesunidadesporpieza = Convert.ToDecimal(dr["Metroslinealesunidadesporpieza"]);
                    intArticulobaseparacosteoID = Convert.ToInt32(dr["ArticulobaseparacosteoID"]);
                    intCosteodirectoenfactura = Convert.ToInt32(dr["Costeodirectoenfactura"]);
                    intRedondear = Convert.ToInt32(dr["Redondear"]);
                    intClasificacionproduccion = Convert.ToInt32(dr["Clasificacionproduccion"]);
                    intTipodecorte = Convert.ToInt32(dr["Tipodecorte"]);
                    dAncho = Convert.ToDecimal(dr["Ancho"]);
                    dLargo = Convert.ToDecimal(dr["Largo"]);
                    dPesoKg = Convert.ToDecimal(dr["Kilos"]);
                    dDimenLargo = Convert.ToDecimal(dr["DimenLargo"]);
                    dDimenAlto = Convert.ToDecimal(dr["DimenAlto"]);
                    dDimenAncho = Convert.ToDecimal(dr["DimenAncho"]);
                    strArtBase = dr["ArtBase"].ToString();
                    intSeCompraEnRollos = Convert.ToInt32(dr["SeCompraEnRollos"]);

                    strPathImagen = dr["PathImagen"].ToString();
                    
                    intTiendaonline = Convert.ToInt32(dr["Tiendaonline"]);
                    intDisponibleTiendaonline = Convert.ToInt32(dr["Disponibleentienda"]);
                    intSubFamiliaID = Convert.ToInt32(dr["SubFamiliasID"]);
                    dPrecioventa = Convert.ToDecimal(dr["Precioventa"]);

                    strUM = dr["UMStr"].ToString();
                    strArtBaseCosteoDX = dr["ArtBaseCosteoDX"].ToString();

                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return "articulosLlenaCajas: "+ex.Message;
            }
        } // public class LlenaCajas

        public string articulosEliminar()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "articulosEliminar";
                cmd.Parameters.AddWithValue("@prmArticulosID", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return "articulosEliminar: "+ex.Message;
            }
        } // Public Class Eliminar

        public string articulosActualizaUbicacion()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosActualizaUbicacion";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.Parameters.AddWithValue("@prmUbi", strUbicación);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return "articulosEliminar: " + ex.Message;
            }
        } // Public Class Eliminar


        //FlexArt
        public DataTable flexArt()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexArt";
                cmd.Parameters.AddWithValue("@prmDes", strNombre);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //FlexArt

        public DataTable ExistenciaPorAlmacen()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexArtExistenciaPorAlmacen";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.Parameters.AddWithValue("@prmFecha", fFechaalta);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //ExistenciaPorAlmacen
        public DataTable Pedidosporfacturar()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexArtPedidosPorFacturar";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //Pedidosporfacturar
        public DataTable ocporRecibir()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexArtOCPorRecibir";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //ocporRecibir
        public DataTable Movimientos()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InventariosMovimientosporarticulo";
                cmd.Parameters.AddWithValue("@prmEmp", 0);
                cmd.Parameters.AddWithValue("@prmSuc", 0);
                cmd.Parameters.AddWithValue("@prmArtID", intArticulosID);
                cmd.Parameters.AddWithValue("@prmAlmacen", 0);
                cmd.Parameters.AddWithValue("@prmFechaIn", DateTime.Now.AddDays(-7300));
                cmd.Parameters.AddWithValue("@prmFechaFin", DateTime.Now);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //Movimientos
        public DataTable Ventas()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexArtVentas";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //Ventas
        public DataTable Compras()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexArtCompras";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //Compras

        public string FlexArtUsuarios()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FlexartUsuarios";
                cmd.Parameters.AddWithValue("@prmUsuariosID", globalCL.gv_UsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    iFlexArtArribosProveedor = Convert.ToInt32(dr["FlexArtArribosProveedor"]);
                    iFlexArtCompras = Convert.ToInt32(dr["FlexArtCompras"]);
                    iFlexArtMovimientos = Convert.ToInt32(dr["FlexArtMovimientos"]);
                    iFlexArtVentas = Convert.ToInt32(dr["FlexArtVentas"]);

                    result = "OK";
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return "articulosEliminar: " + ex.Message;
            }
        } // Public Class Eliminar

        public string ActualizaCostos()
        {
            try
            {
                string result = string.Empty;

                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosActualizaCosto";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.Parameters.AddWithValue("@prmFecha", fFechaalta);
                cmd.Parameters.AddWithValue("@prmMoneda", strMonedasID);
                cmd.Parameters.AddWithValue("@prmCosto", dcosto);
                cmd.Parameters.AddWithValue("@prmTC", dtipodecambio);
                cmd.Parameters.AddWithValue("@prmObs", strObs);
                cmd.Parameters.AddWithValue("@prmUsu", globalCL.gv_UsuarioID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    result = dr["result"].ToString();
                }
                else
                {
                    result = "no read";
                }

                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return "articulosEliminar: " + ex.Message;
            }
        } // Public Class Eliminar

        public DataTable ArticulosCostosGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosCostoGrid";
                cmd.Parameters.AddWithValue("@prmArt", intArticulosID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataAdapter sqlAD = new SqlDataAdapter(cmd);
                sqlAD.Fill(dt);
                cnn.Close();
                return dt;

            }
            catch (Exception) { return dt; }
        } //Compras

        public string GetConsecutivo()
        {
            string result = string.Empty;
            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ArticulosGetConsecutivo";
                cmd.Parameters.AddWithValue("@prmConsecutivo", strConsecutivo);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cnn;

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    result = dr["result"].ToString();
                }
                else
                    result = "no read";
                
                dr.Close();
                cnn.Close();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}