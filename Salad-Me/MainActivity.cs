using Android.App;
using Android.Content;
using Android.Provider;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Graphics;
using System;

namespace Salad_Me
{
    [Activity(Label = "Salad_Me", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText editText;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            Button btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
            editText = FindViewById<EditText>(Resource.Id.editText);

            btnCamera.Click += BtnCameraClik;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)(data.Extras.Get("data"));
            Image img = new Image(bitmap);
            editText.Text = "Contient : " + img.getWhite().ToString() + "% de blanc !";
        }

        private void BtnCameraClik(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }
}
