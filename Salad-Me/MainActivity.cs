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
        ImageView imageView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            Button btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            btnCamera.Click += BtnCameraClik;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)(data.Extras.Get("data"));
            imageView.SetImageBitmap(bitmap);
        }
        private void BtnCameraClik(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }
}

