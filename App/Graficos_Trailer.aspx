<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Graficos_Trailer.aspx.cs" Inherits="App_Graficos_Trailer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
<div class="col-xs-12 separador"></div>
<h2>Gráfico Estados Trailer</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
<%--<div><canvas id="prueba" width="200px" height="200px" aria-label="Hello ARIA World" role="img"></canvas></div>--%>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Literal id="ltl_tablas" runat="server" />
        <div class="col-xs-3">
            <h4>En Ruta: <asp:Label ID="lbl_ruta" runat="server" /></h4>
        </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_html" runat="server" />
            <asp:HiddenField ID="hf_script" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script src="../js2/chart.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function cargaGrafico(site_id,data,labels,color){
        var ctx = $("#pieChart_"+site_id); // document.getElementById("pieChart_"+site_id).getContext('2d');
        var myPieChart = new Chart(ctx,{
            type: 'doughnut',
            data:{
                labels:labels,
                datasets:[{
                    backgroundColor:color,
                    data:data  
                }],                       
            },                                          
            options: {
                plugins:{
                    datalabels:{
                        color:'white',
                        display:false
                    }
                },
                legend: { 
                    display: false
                },
                responsive: false                    
            }
        });
    }
    function graficoprueba(){
        var ctx = $("#prueba"); // document.getElementById("pieChart_"+site_id).getContext('2d');
        con = document.getElementById("prueba").getContext('2d');
        var myPieChart = new Chart(con,{
            type: 'doughnut',
            data:{
                labels:["1","2"],
                datasets:[{
                    backgroundColor:["#FFFFF","#FFFFF"],
                    data:["1","2"]
                }],                       
            },                                          
            options: {
                plugins:{
                    datalabels:{
                        color:'white',
                        display:false
                    }
                },
                title:{
                    display:true,
                    text: 'Estado Trailers'
                },
                legend: { 
                    display: true
                },
                responsive: false                    
            }
        });
    }
</script>
</asp:Content>