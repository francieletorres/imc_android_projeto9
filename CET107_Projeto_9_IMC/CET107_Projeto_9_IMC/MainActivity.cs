using Android.Graphics;

namespace CET107_Projeto_9_IMC
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //ENTRADAS
        EditText etPesoC, etAlturaC;

        //SAÍDA
        TextView tvIMCC;




        protected override void OnCreate(Bundle? savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            tvIMCC = FindViewById<TextView>(Resource.Id.tvIMC);
            etPesoC = FindViewById<EditText>(Resource.Id.etPeso);
            etAlturaC = FindViewById<EditText>(Resource.Id.etAltura);

            Button btCalcularC = FindViewById<Button>(Resource.Id.btCalcular);


            //DEFINIR A FONTE DA APLICACAO E ATRIBUIR AOS CONTROLOSUI
            Typeface minhaFonte = Resources.GetFont(Resource.Font.VielottaRegular);

            tvIMCC.Typeface = minhaFonte;
            etPesoC.Typeface = minhaFonte;
            etAlturaC.Typeface = minhaFonte;
            btCalcularC.Typeface = minhaFonte;
        }
    }
}