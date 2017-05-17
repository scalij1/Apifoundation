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
        TextView txtresp0;
        TextView txtresp1;
        TextView txtresp2;
        TextView txtresp3;
        TextView txtresp4;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            txtcpf = FindViewById<TextView>(Resource.Id.txtcpf);
            txtsobrenome = FindViewById<TextView>(Resource.Id.txtresposta);
            txtnome = FindViewById<TextView>(Resource.Id.txtNome);
            txtresp0 = FindViewById<TextView>(Resource.Id.txtresp0);
            txtresp1 = FindViewById<TextView>(Resource.Id.txtresp1);
            txtresp2 = FindViewById<TextView>(Resource.Id.txtresp2);
            txtresp3 = FindViewById<TextView>(Resource.Id.txtresp3);
            txtresp4 = FindViewById<TextView>(Resource.Id.txtresp4);
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
                //// API Consumer CPF

                //consumer = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers");
                //cpf = new RestRequest("/" + edtcpf.Text, Method.GET);
                //cpf.AddHeader("Content-Type", "application/json; charset=utf-8");
                //cpf.AddHeader("Authorization", "Bearer fed6b2f0-7aba-3339-9813-7fc9387e2581");
                //mensagemConsumer = consumer.Execute(cpf);
                //Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(mensagemConsumer.Content);
                //txtnome.Text = "Nome: " + pessoa.firstName + "Sobrenome: " + pessoa.lastName;


                // API Consumer service-orders
                orderId = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers/");
                requestorderId = new RestRequest("/" + edtcpf.Text + "/service-orders", Method.GET);
                requestorderId.AddHeader("Content-Type", "application/json; charset=utf-8");
                requestorderId.AddHeader("Authorization", "Bearer fed6b2f0-7aba-3339-9813-7fc9387e2581");
                answerorder = orderId.Execute(requestorderId);
                var requestToken = JsonConvert.DeserializeObject<RootObject>(answerorder.Content);
                var end = "";

                for (var i = 0; i < requestToken.orders.Count; i++)
                {
                        end = requestToken.orders[i].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[i].order.orderStatusCode + " "
                       + "Status Description: " + " - " + requestToken.orders[i].order.orderStatusDescription;
                        var respostas0 = requestToken.orders[0].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[0].order.orderStatusCode + " "
                       + "Status Description: " + " - " + requestToken.orders[0].order.orderStatusDescription;
                        var respostas1 = requestToken.orders[1].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[1].order.orderStatusCode + " "
                       + "Status Description: " + " - " + requestToken.orders[1].order.orderStatusDescription; ;
                        var respostas2 = requestToken.orders[2].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[2].order.orderStatusCode + " "
                       + "Status Description: " + " - " + requestToken.orders[2].order.orderStatusDescription; ;
                        var respostas3 = requestToken.orders[3].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[3].order.orderStatusCode + " "
                       + "Status Description: " + " - " + requestToken.orders[3].order.orderStatusDescription; ;
                        var respostas4 = requestToken.orders[4].order.orderId + " " + "StatusCode" + " - " + requestToken.orders[4].order.orderStatusCode + " "
                       + "Status Description: " + " - " + requestToken.orders[4].order.orderStatusDescription; ;


                        txtresp0.Text = JsonConvert.ToString(respostas0);
                        txtresp1.Text = JsonConvert.ToString(respostas1);
                        txtresp2.Text = JsonConvert.ToString(respostas2);
                        txtresp3.Text = JsonConvert.ToString(respostas3);
                        txtresp4.Text = JsonConvert.ToString(respostas4);
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

