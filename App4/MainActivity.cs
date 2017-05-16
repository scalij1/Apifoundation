using System;
using Android.App;
using Android.Widget;
    using Android.OS;
    using RestSharp;
    using Newtonsoft.Json;
    using Android.Util;
    using App4.Resources;
    using Newtonsoft.Json.Linq;
    using Org.Json;
    using System.Net;
    using System.IO;
    using System.Collections.Generic;
    using System.Globalization;


    namespace App4
    {
    [Activity(Label = "App4", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText edtcpf;
        Button btnConsumer;
        TextView txtcpf;
        RestRequest cpf { get; set; }
        public RestClient consumer { get; set; }
        IRestResponse mensagemConsumer;
        TextView txtsobrenome;
        RestClient orderId { get; set; }
        RestRequest requestorderId { get; set; }
        IRestResponse answerorder { get; set; }
        //RadioButton buttonCancelado;
        //RadioButton buttonAberto;
        //RadioButton buttonAgendado;
        TextView txtnome;
        TextView txtorder;
        GridView grid;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            txtcpf = FindViewById<TextView>(Resource.Id.txtcpf);
            txtsobrenome = FindViewById<TextView>(Resource.Id.txtresposta);
            txtnome = FindViewById<TextView>(Resource.Id.txtNome);
            txtorder = FindViewById<TextView>(Resource.Id.txtorder);
            grid = FindViewById<GridView>(Resource.Id.gridView1);
            edtcpf = FindViewById<EditText>(Resource.Id.edtcpf);
            //buttonCancelado = FindViewById<RadioButton>(Resource.Id.rdbtnCancelada);
            //buttonAberto = FindViewById<RadioButton>(Resource.Id.rdbtnAberta);
            //buttonAgendado = FindViewById<RadioButton>(Resource.Id.rdbtnAgendada);
            btnConsumer = FindViewById<Button>(Resource.Id.btnConsumer);
            btnConsumer.Click += BtnConsumer_Click;

        }

        private void BtnConsumer_Click(object sender, EventArgs e)
        {
            try
            {
                // API Consumer CPF

                consumer = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers");
                cpf = new RestRequest("/" + edtcpf.Text, Method.GET);
                cpf.AddHeader("Content-Type", "application/json; charset=utf-8");
                cpf.AddHeader("Authorization", "Bearer 70197e6c-d81b-384c-bb32-d69e8c10b101");
                mensagemConsumer = consumer.Execute(cpf);
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(mensagemConsumer.Content);
                txtnome.Text = "Nome: " + pessoa.firstName + "Sobrenome: " + pessoa.lastName;


                // API Consumer service-orders
                orderId = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers/");
                requestorderId = new RestRequest("/" + edtcpf.Text + "/service-orders", Method.GET);
                requestorderId.AddHeader("Content-Type", "application/json; charset=utf-8");
                requestorderId.AddHeader("Authorization", "Bearer 70197e6c-d81b-384c-bb32-d69e8c10b101");
                answerorder = orderId.Execute(requestorderId);
                var requestToken = JsonConvert.DeserializeObject<RootObject>(answerorder.Content);
                var parse = JObject.Parse(answerorder.Content);
                var QtdeItens = parse.Count;
                var end = "";
                
                for (var i = 0; i < requestToken.orders.Count; i++)
                {
                    end = "OrderId: " + " - " + requestToken.orders[i].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[i].order.orderStatusCode + " "
                        + "Status Description: " + " - " + requestToken.orders[i].order.orderStatusDescription;
                    
                   
                }
                

                    //if (order.order.orderStatusCode == "CANC")
                    //{
                    //    buttonCancelado.Visibility = Android.Views.ViewStates.Visible;
                    //    buttonAgendado.Visibility = Android.Views.ViewStates.Invisible;
                    //    buttonAberto.Visibility = Android.Views.ViewStates.Invisible;
                    //    buttonCancelado.Clickable = false;
                //}
                //else if (order.order.orderStatusCode == "ABRT")
                //{
                //    buttonCancelado.Visibility = Android.Views.ViewStates.Invisible;
                //    buttonAgendado.Visibility = Android.Views.ViewStates.Invisible;
                //    buttonAberto.Visibility = Android.Views.ViewStates.Visible;
                //    buttonAberto.Clickable = false;
                //}
                //else if (order.order.orderStatusCode == "AGEN")
                //{
                //    buttonCancelado.Visibility = Android.Views.ViewStates.Invisible;
                //    buttonAgendado.Visibility = Android.Views.ViewStates.Visible;
                //    buttonAberto.Visibility = Android.Views.ViewStates.Invisible;
                //    buttonAgendado.Clickable = false;
                //}
            }

                catch (Exception)
                {

                throw;
                 }
        }
    }
}

