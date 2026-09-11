using Android.Graphics;
using Android.Views;

namespace CET107_Projeto_9_IMC
{
    [Activity(Label = "@string/app_name", 
        Theme = "@style/AppTheme",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        MainLauncher = true)]
    public class MainActivity : Activity
    {
        //ENTRADAS
        EditText etPesoC, etAlturaC;

        //SAÍDA
        TextView tvIMCC;




        protected override void OnCreate(Bundle? savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            //pode usar dessa forma ou usando o screnOrientation lá de cima
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait; 

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            tvIMCC = FindViewById<TextView>(Resource.Id.tvIMC);
            etPesoC = FindViewById<EditText>(Resource.Id.etPeso);
            etAlturaC = FindViewById<EditText>(Resource.Id.etAltura);

            Button btCalcularC = FindViewById<Button>(Resource.Id.btCalcular);


            //DEFINIR A FONTE DA APLICACAO E ATRIBUIR AOS CONTROLOSUI
            Typeface minhaFonte = Resources.GetFont(Resource.Font.VielottaRegular);

            //tvIMCC.Typeface = minhaFonte;
            //etPesoC.Typeface = minhaFonte;
            //etAlturaC.Typeface = minhaFonte;
            //btCalcularC.Typeface = minhaFonte;

            //vamos utilizar a função que foi criada abaixo
            FontHelper.AplicaFonte(Window.DecorView.RootView, minhaFonte);



        }
    }

    //FontHelper
    public static class FontHelper
    {
        public static void AplicaFonte(View view, Typeface tf) //método
        {
            if(view is ViewGroup group)
            {
                for(int contaControloUI = 0;
                        contaControloUI < group.ChildCount;
                        contaControloUI++)
                {
                    AplicaFonte(group.GetChildAt(contaControloUI), tf); //recursivo
                }
            }
            else if(view is TextView textView) 
            {
               textView.Typeface = tf;
            }
            else if(view is EditText editText)
            {
                editText.Typeface = tf;
            }
            else if(view is Button button)
            {
                button.Typeface = tf;
            }
        }
    }
}