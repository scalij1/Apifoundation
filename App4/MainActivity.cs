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
using System;

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
        public int EndDate { get; private set; }
        public int StartDate { get; private set; }

        TextView txtnome;
        TextView txtorder;
        TextView txtmensagem;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnConsumer = FindViewById<Button>(Resource.Id.btnConsumer);
            edtcpf = FindViewById<EditText>(Resource.Id.edtcpf);
            txtcpf = FindViewById<TextView>(Resource.Id.txtcpf);
            txtsobrenome = FindViewById<TextView>(Resource.Id.txtresposta);
            txtnome = FindViewById<TextView>(Resource.Id.txtNome);
            txtorder = FindViewById<TextView>(Resource.Id.txtorder);
            txtmensagem = FindViewById<TextView>(Resource.Id.txtMensagem);
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
                txtnome.Text = "Nome: " +pessoa.firstName;
                txtsobrenome.Text = "Sobrenome: "+ pessoa.lastName;
                
                // API Consumer Appliances
                orderId = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers/");
                requestorderId = new RestRequest("/"+ edtcpf.Text+ "/service-orders", Method.GET);
                requestorderId.AddHeader("Content-Type", "application/json; charset=utf-8");
                requestorderId.AddHeader("Authorization", "Bearer 70197e6c-d81b-384c-bb32-d69e8c10b101");
                answerorder = orderId.Execute(requestorderId);
                var requestToken = JsonConvert.DeserializeObject<RootObject>(answerorder.Content);
                var parse = JObject.Parse(answerorder.Content);
                var QtdeItens = parse.Count;
                foreach (var order in requestToken.orders)
                {
                    //for (var i = 0; i < requestToken.orders.Count; i++)
                    //{
                        object vader = order.order.orderId;
                        string darth = Convert.ToString(vader);
                        txtorder.Text = darth + order.order.orderStatusDescription + order.order.orderStatusCode;      
                    //}
                }

               
                
                
                

            }
            catch (Exception)
            {

                throw;
            }




        }


    }
}

