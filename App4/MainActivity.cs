using System;
using Android.App;
using Android.Widget;
using Android.OS;
using RestSharp;


namespace App4
{
    [Activity(Label = "App4", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText edtcpf;
        Button btnConsumer;
        TextView txtcpf;
        EditText edtmsg;
        TextView txtmsg;
        GridView grdview3;
        RestRequest request { get; set; }
        public RestClient client { get; set; }
        IRestResponse mensagem;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnConsumer = FindViewById<Button>(Resource.Id.btnConsumer);
            edtcpf = FindViewById<EditText>(Resource.Id.edtcpf);
            txtcpf = FindViewById<TextView>(Resource.Id.txtcpf);
            txtmsg = FindViewById<TextView>(Resource.Id.txtmsg);
            grdview3 = FindViewById<GridView>(Resource.Id.gridView3);
            btnConsumer.Click += BtnConsumer_Click;
        }

        private void BtnConsumer_Click(object sender, EventArgs e)
        {
            try
            {
                
                client = new RestClient("https://qa.api-latam.whirlpool.com/v1.0/consumers");
                request = new RestRequest("/" + edtcpf.Text   , Method.GET);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddHeader("Authorization", "Bearer 70197e6c-d81b-384c-bb32-d69e8c10b101");
                request.AddParameter("cpf",edtcpf.Text);
                mensagem = client.Execute(request);
                
                
            }
            catch (Exception)
            {
                
                throw;
            }
           
            
            

        }
        

    }
}

/*var request = new RestRequest("resource/{id}", Method.GET);
            request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            string url = "https://qa.api-latam.whirlpool.com/v1.0/consumer/54493746238" + edtcpf.Text;
            RestSharp.RestRequest call = new RestRequest(url);
            IRestResponse response = call.Equals(request);
            var content = response.Content;*/
