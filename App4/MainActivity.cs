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
using Android.Graphics;
using Android.Content.Res;
using App4.Dialogs;

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
        TextView txtnome;
        TextView txtresp0;
        TextView txtresp1;
        TextView txtresp2;
        TextView txtresp3;
        TextView txtresp4;
        public List<string> lista;
        ListView mlistview;
       // ImageView imageView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            txtcpf = FindViewById<TextView>(Resource.Id.txtcpf);
            mlistview = FindViewById<ListView>(Resource.Id.listView2);
            txtnome = FindViewById<TextView>(Resource.Id.txtNome);
            edtcpf = FindViewById<EditText>(Resource.Id.edtcpf);
            btnConsumer = FindViewById<Button>(Resource.Id.btnConsumer);
            //imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            btnConsumer.Click += BtnConsumer_Click;
            lista = new List<string>();
        }

        private void BtnConsumer_Click(object sender, EventArgs e)
        {
            try
            {

                // API Consumer CPF

                imageView.SetImageResource(Resource.Drawable.giphy);

                consumer = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers");
                cpf = new RestRequest("/" + edtcpf.Text, Method.GET);
                cpf.AddHeader("Content-Type", "application/json; charset=utf-8");
                cpf.AddHeader("Authorization", "Bearer fed6b2f0-7aba-3339-9813-7fc9387e2581");
                mensagemConsumer = consumer.Execute(cpf);
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(mensagemConsumer.Content);
                txtnome.Text = "Nome: " + pessoa.firstName + " " + pessoa.lastName;


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
                    lista.Add(end);
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lista);
                    mlistview.Adapter = adapter;
                }
            }

            //        if (requestToken.orders[0].order.orderStatusCode == "CANC")
            //        {
            //            mlistview.Adapter = adapter;

            //        }
            //        else if (requestToken.orders[0].order.orderStatusCode == "ABRT")
            //        {
            //            txtresp0.Text = JsonConvert.ToString(respostas0);
            //            txtresp0.SetTextColor(Color.ParseColor("green"));
            //        }

            //        else if (requestToken.orders[0].order.orderStatusCode == "AGEN")
            //        {
            //            txtresp0.Text = JsonConvert.ToString(respostas0);
            //            txtresp0.SetTextColor(Color.ParseColor("blue"));
            //        }

            //        if (requestToken.orders[1].order.orderStatusCode == "CANC")
            //        {
            //            txtresp1.Text = JsonConvert.ToString(respostas1);
            //            txtresp1.SetTextColor(Color.ParseColor("red"));
            //        }
            //        else if (requestToken.orders[1].order.orderStatusCode == "ABRT")
            //        {
            //            txtresp1.Text = JsonConvert.ToString(respostas1);
            //            txtresp1.SetTextColor(Color.ParseColor("green"));
            //        }

            //        else if (requestToken.orders[1].order.orderStatusCode == "AGEN")
            //        {
            //            txtresp1.Text = JsonConvert.ToString(respostas1);
            //            txtresp1.SetTextColor(Color.ParseColor("blue"));
            //        }

            //        if (requestToken.orders[2].order.orderStatusCode == "CANC")
            //        {
            //            txtresp2.Text = JsonConvert.ToString(respostas2);
            //            txtresp2.SetTextColor(Color.ParseColor("red"));
            //        }
            //        else if (requestToken.orders[2].order.orderStatusCode == "ABRT")
            //        {
            //            txtresp2.Text = JsonConvert.ToString(respostas2);
            //            txtresp2.SetTextColor(Color.ParseColor("green"));
            //        }

            //        else if (requestToken.orders[2].order.orderStatusCode == "AGEN")
            //        {
            //            txtresp2.Text = JsonConvert.ToString(respostas2);
            //            txtresp2.SetTextColor(Color.ParseColor("blue"));
            //        }
            //        if (requestToken.orders[3].order.orderStatusCode == "CANC")
            //        {
            //            txtresp3.Text = JsonConvert.ToString(respostas3);
            //            txtresp3.SetTextColor(Color.ParseColor("red"));
            //        }
            //        else if (requestToken.orders[3].order.orderStatusCode == "ABRT")
            //        {
            //            txtresp3.Text = JsonConvert.ToString(respostas3);
            //            txtresp3.SetTextColor(Color.ParseColor("green"));
            //        }

            //        else if (requestToken.orders[3].order.orderStatusCode == "AGEN")
            //        {
            //            txtresp3.Text = JsonConvert.ToString(respostas3);
            //            txtresp3.SetTextColor(Color.ParseColor("blue"));
            //        }
            //        if (requestToken.orders[4].order.orderStatusCode == "CANC")
            //        {
            //            txtresp4.Text = JsonConvert.ToString(respostas4);
            //            txtresp4.SetTextColor(Color.ParseColor("red"));
            //        }
            //        else if (requestToken.orders[4].order.orderStatusCode == "ABRT")
            //        {
            //            txtresp4.Text = JsonConvert.ToString(respostas4);
            //            txtresp4.SetTextColor(Color.ParseColor("green"));
            //        }

            //        else if (requestToken.orders[4].order.orderStatusCode == "AGEN")
            //        {
            //            txtresp4.Text = JsonConvert.ToString(respostas4);
            //            txtresp4.SetTextColor(Color.ParseColor("blue"));
            //        } 
            //    }
            //}

            catch (Exception)
            {

                throw;
            }
        }
    }
}

