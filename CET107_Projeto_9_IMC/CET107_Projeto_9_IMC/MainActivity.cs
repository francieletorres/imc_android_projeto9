using Android.Graphics;
using Android.Views;

namespace CET107_Projeto_9_IMC
{
    [Activity(Label = "@string/app_name2",
        Theme = "@style/AppTheme",
        Icon = "@drawable/icon_bmi01",
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

            Button? btCalcularC = FindViewById<Button>(Resource.Id.btCalcular)!;
            Button? btLimparC = FindViewById<Button>(Resource.Id.btLimpar)!;


            //DEFINIR A FONTE DA APLICACAO E ATRIBUIR AOS CONTROLOSUI
            Typeface minhaFonte = Resources.GetFont(Resource.Font.MontserratRegular);

            //tvIMCC.Typeface = minhaFonte;
            //etPesoC.Typeface = minhaFonte;
            //etAlturaC.Typeface = minhaFonte;
            //btCalcularC.Typeface = minhaFonte;

            //vamos utilizar a função que foi criada abaixo
            FontHelper.AplicaFonte(Window.DecorView.RootView, minhaFonte);


            btCalcularC!.Click += delegate
            {
                /*
                if (etPesoC == null || etAlturaC == null || tvIMCC == null)
                {
                    Toast.MakeText(this, "UI not inicializado.", ToastLength.Long).Show();
                    return;
                }

                var pesoCtrl = etPesoC;
                var alturaCtrl = etAlturaC;
                var imcCtrl = tvIMCC;
                */

                if (!string.IsNullOrEmpty(etPesoC!.Text) && !string.IsNullOrEmpty(etAlturaC!.Text))
                {
                    double peso = 0, altura = 0, imc = 0;

                    bool pesoValido = double.TryParse(etPesoC!.Text,
                        System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out peso);

                    bool alturaValida = double.TryParse(etAlturaC!.Text,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out altura);

                    if (pesoValido && alturaValida)
                    {
                        if (altura != 0)
                        {
                            if (peso > 0 && altura > 0)
                            {
                                //imc = peso / (altura * altura);
                                imc = peso / Math.Pow(altura, 2);
                                string resultado = ProcessaResultado(imc);
                                MostraMensagem(resultado);
                                tvIMCC!.Text = imc.ToString("F2",
                                    System.Globalization.CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                string mensagem = "Peso e altura devem ser valores positivos.";
                                if (peso < 0 && altura < 0)
                                {
                                    mensagem = "Peso e altura devem ser valores positivos.";
                                }
                                else if (peso < 0)
                                {
                                    mensagem = "Peso deve ser um valor positivo.";
                                }
                                else if (altura < 0)
                                {
                                    mensagem = "Altura deve ser um valor positivo.";
                                }
#pragma warning disable CS8602
                                Toast.MakeText(this, mensagem, ToastLength.Long).Show();
#pragma warning disable CS8602
                                tvIMCC!.Text = "Valores inválidos!";
                            }
                        }
                        else
                        {
#pragma warning disable CS8602
                            Toast.MakeText(this, "Altura não pode ser zero.", ToastLength.Long).Show();
#pragma warning disable CS8602
                            tvIMCC!.Text = "Erro: Altura zero.";
                        }
                    }
                    else
                    {
                        string mensagem = "Valores numéricos inválidos para peso e/ou altura.";
                        if (!pesoValido && !alturaValida)
                        {
                            mensagem = "Valores numéricos inválidos para peso e/ou altura.";
                        }
                        else if (!pesoValido)
                        {
                            mensagem = "Valor numérico inválido para peso.";
                        }
                        else if (!alturaValida)
                        {
                            mensagem = "Valor numérico inválido para altura.";
                        }
#pragma warning disable CS8602
                        Toast.MakeText(this, mensagem, ToastLength.Long).Show();
#pragma warning disable CS8602
                        tvIMCC!.Text = "Valores inválidos!";
                    }
                }
                else
                {
                    string mensagem = "Por favor preencha todos os campos.";

                    if (string.IsNullOrEmpty(etPesoC.Text) && string.IsNullOrEmpty(etAlturaC.Text))
                    {
                        mensagem = "Por favor preencha os campos de peso e altura.";
                    }
                    else if (string.IsNullOrEmpty(etPesoC.Text))
                    {
                        mensagem = "Por favor, preencha o campo de peso.";
                    }
                    else if (string.IsNullOrEmpty(etAlturaC.Text))
                    {
                        mensagem = "Por favor, preencha o campo de altura.";
                    }
#pragma warning disable CS8602
                    Toast.MakeText(this, mensagem, ToastLength.Long).Show();
#pragma warning disable CS8602
                    tvIMCC!.Text = "Sem valores!";

                }
            };

            btLimparC!.Click += delegate
            {

                if (etPesoC == null || etAlturaC == null || tvIMCC == null)
                {
#pragma warning disable CS8602
                    Toast.MakeText(this, "UI not inicializado", ToastLength.Long).Show();
#pragma warning disable CS8602
                }
                else
                {
                    etPesoC!.Text = string.Empty;
                    etAlturaC!.Text = string.Empty;
                    tvIMCC!.Text = string.Empty;
                    etPesoC!.RequestFocus();
                }
            };

        }

        //Método processa resultado

        private string ProcessaResultado(double imc)
        {
            string mensagem = string.Empty;

            if (imc < 16.9)
            {
                mensagem = "Desnutrido.";
            }
            else if (imc < 18.5)
            {
                mensagem = "Abaixo do peso.";
            }
            else if (imc >= 18.5 && imc < 25)
            {
                mensagem = "Peso normal";
            }
            else if (imc >= 25 && imc < 30)
            {
                mensagem = "Acima do peso";
            }
            else if (imc >= 30 && imc < 35)
            {
                mensagem = "Obesidade grau I.";
            }
            else if (imc >= 35 && imc < 40)
            {
                mensagem = "Obesidade grau II.";
            }
            else
            {
                mensagem = "Obesidade grau III.";
            }

            return mensagem;
        }

        private void MostraMensagem(string mensagem)
        {
            //exibir alert

            //Declaracao do builder da caixa de dialogo para exibir mensagem
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

            //Configuracao do título, mensagem e botão de ação da caixa de diálogo
            builder.SetTitle("Resultado do IMC");
            builder.SetMessage(mensagem);

            //Configuracao do botao de ação OK 
            builder.SetPositiveButton("OK", (sender, args) => {

                //Inserir código adicional aqui, se necessário para tratar o clique no botão "OK"

            });

            Android.App.AlertDialog? dialog = builder.Create();
            dialog!.Show();
        }
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
