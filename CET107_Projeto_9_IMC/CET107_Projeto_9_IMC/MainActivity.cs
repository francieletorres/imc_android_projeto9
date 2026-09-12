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
        EditText? etPesoC = null, etAlturaC = null;

        //SAÍDA
        TextView? tvIMCC =  null;
        ImageView? ivIMCC = null;



        protected override void OnCreate(Bundle? savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            //pode usar dessa forma ou usando o screnOrientation lá de cima
            //RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait; 

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            etPesoC = FindViewById<EditText>(Resource.Id.etPeso);
            etAlturaC = FindViewById<EditText>(Resource.Id.etAltura);

            Button? btCalcularC = FindViewById<Button>(Resource.Id.btCalcular)!;
            Button? btLimparC = FindViewById<Button>(Resource.Id.btLimpar)!;
            ImageButton? ibSairC = FindViewById<ImageButton>(Resource.Id.ibSair)!;

            tvIMCC = FindViewById<TextView>(Resource.Id.tvIMC);
            ivIMCC = FindViewById<ImageView>(Resource.Id.ivIMC);

            //DEFINIR A FONTE DA APLICACAO E ATRIBUIR AOS CONTROLOSUI
#pragma warning disable CA1416 // Converting null literal or possible null value to non-nullable type.
            Typeface? minhaFonte = Resources?.GetFont(Resource.Font.MontserratRegular);
#pragma warning restore CA1416 // Converting null literal or possible null value to non-nullable type.

            //tvIMCC.Typeface = minhaFonte;
            //etPesoC.Typeface = minhaFonte;
            //etAlturaC.Typeface = minhaFonte;
            //btCalcularC.Typeface = minhaFonte;

            //vamos utilizar a função que foi criada abaixo
            if (minhaFonte != null)
            FontHelper.AplicaFonte(Window!.DecorView.RootView!, minhaFonte);


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

                                 ProcessaResultadoIMC(imc);
                              
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

            //evento click do botão limpar
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
                    MostraImagem("img_imc0");
                    etPesoC!.RequestFocus();
                }
            };


            if(ibSairC != null)
            {
                ibSairC!.Click += delegate
                {
                    VerificaSaida();
                };

            }
      
        }

        private void VerificaSaida()
        {
           if(this == null)  return;
           var builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Confirmação da Saída!");
            builder.SetMessage("Tem certeza de que deseja sair da aplicaçao?");
            builder.SetPositiveButton("Sim", (sender, args) =>
            {
                FinishAffinity(); //encerra a  atividade atual e as relacionadas fechando toda a aplicação
            });
            builder.SetNegativeButton("Não", (sender, args) =>
            {
                //Ação para o botao cancel, (opcional)
                //Neste não é necessário fazer nada porque a caixa será fechada automaticamente.
            });
              builder.Show();
        }


    //Método processa resultado IMC

    private void ProcessaResultadoIMC(double imc)
        {
            string mensagem = string.Empty;
            string imagem = string.Empty;

            if (imc < 16.9)
            {
                mensagem = "Peso muito baixo.";
                imagem = "img_imc1";
            }
            else if (imc < 18.5)
            {
                mensagem = "Abaixo do peso.";
                imagem = "img_imc1";
            }
            else if (imc >= 18.5 && imc < 25)
            {
                mensagem = "Peso normal";
                imagem = "img_imc2";
            }
            else if (imc >= 25 && imc < 30)
            {
                mensagem = "Acima do peso";
                imagem = "img_imc3";
            }
            else if (imc >= 30 && imc < 35)
            {
                mensagem = "Obesidade grau I.";
                imagem = "img_imc4";
            }
            else if (imc >= 35 && imc < 40)
            {
                mensagem = "Obesidade grau II.";
                imagem = "img_imc5";
            }
            else
            {
                mensagem = "Obesidade grau III.";
                imagem = "img_imc5";
            }

            MostraMensagem(mensagem);
            MostraImagem(imagem);

        }

        //responsavel por exibir a imagem correspondente ao 
        //calculo do IMC
        private void MostraImagem(string imagem)
        {
            if (Resources == null || ivIMCC == null) return;
            int resourceId = Resources.GetIdentifier(imagem, "drawable", PackageName);
            ivIMCC!.SetImageResource(resourceId);
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
        public static void AplicaFonte(View? view, Typeface tf) //método
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
