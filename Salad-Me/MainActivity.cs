using Android.App;
using Android.Content;
using Android.Provider;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using System;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using System.IO;

namespace Salad_Me
{
    [Activity(Label = "Salad_Me", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView whiteText;
        TextView necText;
        Button netBtn;
        Button nfcBtn;
        Image img;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            Button btnCamera = FindViewById<Button>(Resource.Id.startSessionButton);
            whiteText = FindViewById<TextView>(Resource.Id.whiteText);
            necText = FindViewById<TextView>(Resource.Id.necText);
            netBtn = FindViewById<Button>(Resource.Id.netButton);
            nfcBtn = FindViewById<Button>(Resource.Id.nfcButton);
            btnCamera.Click += BtnCameraClik;
            netBtn.Click += (sender, e) => {
                string url = "https://salad-me.firebaseio.com/vegetablesData.json";
                FetchResults(url, img);
            };
        }
        private void BtnCameraClik(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)(data.Extras.Get("data"));
            img = new Image(bitmap);
            whiteText.Text = img.getWhite().ToString();
            necText.Text = img.getNecrose().ToString();
            netBtn.Enabled = true;
            nfcBtn.Enabled = true;
        }

        private void FetchResults(string url, Image img)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";
            string data =
                "{\"farmerId\": 1," +
                " \"type\": 1," +
                "\"position\": {\"lat\": \"48.862725\",\"lng\": \"2.287592000000018\"}," +
                "\"date\": 1211929182," +
                "\"white\": " + img.getWhite().ToString() + "," +
                "\"necrosity\": " + img.getNecrose().ToString() + "}";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
                try
                {
                    var httpResponse = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Console.Out.WriteLine("Response Body: \r\n {0}", result);
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Exception {0}", ex);
                }
            }
        }
    }
}
