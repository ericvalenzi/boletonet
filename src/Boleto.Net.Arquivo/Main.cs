using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using BoletoNet;

namespace BoletoNet.Arquivo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void remessaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region Remessa
        public void GeraArquivoCNAB400(IBanco banco, Cedente cedente, Boletos boletos, string numeroConvenio = null)
        {
            try
            {
                saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);

                    //Valida a Remessa Correspondentes antes de Gerar a mesma...
                    string vMsgRetorno = string.Empty;
                    bool vValouOK = arquivo.ValidarArquivoRemessa(cedente.Convenio.ToString(), banco, cedente, boletos, 1, out vMsgRetorno);
                    if (!vValouOK)
                    {
                        MessageBox.Show(String.Concat("Foram localizados inconsistências na validação da remessa!", Environment.NewLine, vMsgRetorno),
                                        "Teste",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        arquivo.GerarArquivoRemessa(numeroConvenio != null ? numeroConvenio : "0", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                        MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GeraArquivoCNAB240(IBanco banco, Cedente cedente, Boletos boletos)
        {
            saveFileDialog.Filter = "Arquivos de Retorno (*.rem)|*.rem|Todos Arquivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB240);
                arquivo.GerarArquivoRemessa("1200303001417053", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);

                MessageBox.Show("Arquivo gerado com sucesso!", "Teste",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
        //
        public void GeraDadosItau(TipoArquivo tipoArquivo)
        {
            DateTime vencimento = new DateTime(2019, 9, 10);

            Instrucao_Itau item1 = new Instrucao_Itau(10);
            //Instrucao_Itau item1 = new Instrucao_Itau(34, 10);
            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "13000");
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "13000";

            Boleto b = new Boleto(vencimento, 1642, "198", "92082835", c);
            b.NumeroDocumento = "1008073";

            b.DataVencimento = Convert.ToDateTime("12-12-12");

            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "DF";

            //item2.Descricao += item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Cedente.ContaBancaria.DigitoAgencia = "1";
            b.Cedente.ContaBancaria.DigitoAgencia = "2";

            b.Banco = new Banco(341);

            Boletos boletos = new Boletos();
            boletos.Add(b);

            Boleto b2 = new Boleto(vencimento, 1642, "198", "92082835", c);
            b2.NumeroDocumento = "1008073";

            b2.DataVencimento = Convert.ToDateTime("12-12-12");

            b2.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b2.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b2.Sacado.Endereco.Bairro = "Testando";
            b2.Sacado.Endereco.Cidade = "Testelândia";
            b2.Sacado.Endereco.CEP = "70000000";
            b2.Sacado.Endereco.UF = "DF";

            //item2.Descricao += item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b2.Instrucoes.Add(item1);
            //b2.Instrucoes.Add(item2);
            b2.Cedente.ContaBancaria.DigitoAgencia = "1";
            b2.Cedente.ContaBancaria.DigitoAgencia = "2";

            b2.Banco = new Banco(341);

            boletos.Add(b2);

            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    GeraArquivoCNAB240(b2.Banco, c, boletos);
                    break;
                case TipoArquivo.CNAB400:
                    GeraArquivoCNAB400(b2.Banco, c, boletos);
                    break;
                default:
                    break;
            }

        }
        public void GeraDadosBanrisul()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.Agencia = "051";
            conta.DigitoAgencia = "2";
            conta.Conta = "13000";
            conta.DigitoConta = "3";
            //
            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "00.000.000/0000-00";
            c.Nome = "Empresa de Atacado";
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "513035600299";//No Banrisul, esse código está no manual como 12 caracteres, por eu(sidneiklein) isso tive que alterar o tipo de int para string;
            c.Convenio = 124522;
            //
            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(2469.69);
            b.Carteira = "1";
            b.VariacaoCarteira = "02";
            b.NossoNumero = string.Empty; //"92082835"; //** Para o "Remessa.TipoDocumento = "06", não poderá ter NossoNúmero Gerado!
            b.NumeroDocumento = "1008073";
            //
            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "RS";

            Instrucao_Banrisul item1 = new Instrucao_Banrisul(9, 5, 0);
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Banco = new Banco(041);

            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "06"; //06 - COBRANÇA ESCRITURAL
            #endregion

            //
            Boletos boletos = new Boletos();
            boletos.Add(b);

            GeraArquivoCNAB400(b.Banco, c, boletos);
        }
        public void GeraDadosSicredi()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.Agencia = "0710";
            conta.DigitoAgencia = "0";
            conta.Conta = "7914";
            conta.DigitoConta = "3";
            conta.OperacaConta = "0";
            //
            Cedente c = new Cedente("01.924.069/0001-74", "BRUCAI TRANSPORTES E ARMAZEM GERAIS LTDA", "0710","7914","3");
            c.ContaBancaria = conta;
            //c.CPFCNPJ = "01.924.069/0001-74";
            //c.Nome = "Empresa de Atacado";
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "79143";//No Banrisul, esse código está no manual como 12 caracteres, por eu(sidneiklein) isso tive que alterar o tipo de int para string;
            c.Convenio = 79143;
            //
            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataDocumento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(2469.69);
            b.Carteira = "1";
            //b.VariacaoCarteira = "02";            
            b.DigitoNossoNumero = "8";
            b.NossoNumero = "00200006"; //"92082835"; //** Para o "Remessa.TipoDocumento = "06", não poderá ter NossoNúmero Gerado!
            b.NumeroDocumento = "000010379";
            //
            b.Sacado = new Sacado("356.733.598-70", "Eric Filardi");
            b.Sacado.Endereco.End = "Rua vito lilla";
            b.Sacado.Endereco.Bairro = "Vila augusta";
            b.Sacado.Endereco.Cidade = "Guarulhos";
            b.Sacado.Endereco.CEP = "07025-040";
            b.Sacado.Endereco.UF = "SP";

            Instrucao_Sicredi item1 = new Instrucao_Sicredi(1, 0);
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Banco = new Banco(748);

            //
            EspecieDocumento especiedocumento = new EspecieDocumento(748, "A");//(341, 1);
            b.EspecieDocumento = especiedocumento;


            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "A"; //A = 'A' - SICREDI com Registro
            #endregion

            //
            Boletos boletos = new Boletos();
            boletos.Add(b);

            GeraArquivoCNAB400(b.Banco, c, boletos);
        }
        public void GeraDadosSantander()
        {
            Boletos boletos = new Boletos();

            DateTime vencimento = new DateTime(2003, 5, 15);

            Cedente c = new Cedente("01.924.069/0001-74", "BRUCAI TRANSPORTES E ARMAZEM GERAIS LTDA", "1319", "1300900","7");
            c.Codigo = "9482822";
            c.CodigoTransmissao = "";
            c.ContaBancaria.DigitoAgencia = "";
            c.DigCedente = null;
            c.DigitoCedente = -1;
            c.Carteira = null;
            //c.CPFCNPJ = "01924069000174";
            //c.ContaBancaria.DigitoConta = "7";
            string[] t1 = new string[8] { "00000970-9", "00000971-7", "00000972-5", "00000973-3", "00000974-1", "00000975-0", "00000976-8", "00000977-6" };
            string[] t2 = new string[8] { "000010256", "000010257", "000010258", "000010259", "000010260", "000010261", "000010262", "000010263" };

            for (int i = 0; i < 8; i++)
            {
                Boleto b = new Boleto
                {
                    NumeroDocumento = t2[i],
                    Carteira = "101",
                    NossoNumero = t1[i],
                    Cedente = c,
                    ValorBoleto = 113.6400m,
                    Sacado = new Sacado
                    {
                        CPFCNPJ = "18.268.146/0001-88",
                        Nome = "ARMAZENS VICTORIA LTDAEPP",
                        Endereco = new Endereco
                        {
                            End = "VIA DAS TORRES S/N",
                            Bairro = "DISTRITO INDUSTRIAL",
                            Cidade = "CANDEIAS",
                            CEP = "43813-100",
                            UF = "BA"
                        },
                    },
                    Banco = new Banco(033),
                    DataVencimento = DateTime.Now,
                    DataProcessamento = DateTime.Now,
                    DataDocumento = DateTime.Now,
                    PercMulta = 2.2728m,
                    JurosMora = 0.0011m,
                    TipoImpressao = "A",
                    Remessa = null,
                    ModalidadeCobranca = 0,
                    Moeda = 9,
                    ApenasRegistrar = false,
                    QuantidadeMoeda = 1,
                    LocalPagamento = "Até o vencimento, preferencialmente no ",
                    BancoCarteira = null,
                    Abatimento = 0,
                    Aceite = "N",
                    Avalista = null,
                    CodJurosMora = "",
                    Categoria = 0,
                    DigitoNossoNumero = "",
                    Especie = "R$",
                    IOF = 0,
                    JurosPermanente = false,
                    NumeroControle = null,
                    NumeroDiasBaixa = 0,
                    NumeroParcela =0,
                    OutrosAcrescimos =0 ,
                    OutrosDescontos = 0,
                    PercentualIOS = 0,
                    PercJurosMora = 0,
                    EspecieDocumento = new EspecieDocumento(33, "1")
                };
                b.EspecieDocumento = new EspecieDocumento_Santander("02");
                boletos.Add(b);
            }           

            GeraArquivoCNAB400(new Banco(33), c, boletos, "9482822");
        }
        public void GeraDadosCaixa()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.OperacaConta = "OPE";
            conta.Agencia = "3792";
            conta.DigitoAgencia = "0";
            conta.Conta = "1031";
            conta.DigitoConta = "6";
            //
            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "01.924.069/0001-74";
            c.Nome = "BRUCAI TRANSPORTES E ARMAZEM GERAIS LTDA";
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = String.Concat(conta.Agencia, conta.DigitoAgencia, conta.OperacaConta, conta.Conta, conta.DigitoConta); //Na Caixa, esse código está no manual como 16 caracteres AAAAOOOCCCCCCCCD;
            //
            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(2469.69);
            b.Carteira = "1";
            b.NossoNumero = "0000000000006-0";
            b.NumeroDocumento = "000010333";
            EspecieDocumento ED = new EspecieDocumento(104,"0");
            b.EspecieDocumento = ED;
            b.ValorMulta = Convert.ToDecimal(2.55);
            b.DataMulta = b.DataVencimento;

            //
            //b.Sacado = new Sacado("Fulano de Silva");
            //b.Sacado.CPFCNPJ = "000.000.000-00";
            //b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            //b.Sacado.Endereco.Bairro = "Testando";
            //b.Sacado.Endereco.Cidade = "Testelândia";
            //b.Sacado.Endereco.CEP = "70000000";
            //b.Sacado.Endereco.UF = "RS";

            Instrucao_Caixa item1 = new Instrucao_Caixa(9, 5);
            b.Instrucoes.Add(item1);
            //b.Instrucoes.Add(item2);
            b.Banco = new Banco(104);

            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "2"; // SIGCB - SEM REGISTRO
            //b.Remessa.CodigoOcorrencia = string.Empty;
            #endregion

            //
            Boletos boletos = new Boletos();
            boletos.Add(b);

            GeraArquivoCNAB240(b.Banco, c, boletos);
        }
        public void GeraDadosBancoDoNordeste()
        {
            ContaBancaria conta = new ContaBancaria();
            conta.Agencia = "21";
            conta.DigitoAgencia = "0";
            conta.Conta = "12717";
            conta.DigitoConta = "8";

            Cedente c = new Cedente();
            c.ContaBancaria = conta;
            c.CPFCNPJ = "00.000.000/0000-00";
            c.Nome = "Empresa de Atacado";

            Boleto b = new Boleto();
            b.Cedente = c;
            //
            b.DataProcessamento = DateTime.Now;
            b.DataVencimento = DateTime.Now.AddDays(15);
            b.ValorBoleto = Convert.ToDecimal(1);
            b.Carteira = "4";
            b.NossoNumero = "7777777";
            b.NumeroDocumento = "2525";
            //
            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "RS";

            b.Banco = new Banco(004);

            EspecieDocumento especiedocumento = new EspecieDocumento(004, "1");//Duplicata Mercantil
            b.EspecieDocumento = especiedocumento;

            #region Dados para Remessa:
            b.Remessa = new Remessa();
            b.Remessa.TipoDocumento = "A";
            #endregion


            Boletos boletos = new Boletos();
            boletos.Add(b);

            GeraArquivoCNAB400(b.Banco, c, boletos);
        }
        public void GeraDadosBB()
        {
            Boletos boletos = new Boletos();

            DateTime vencimento = new DateTime(2003, 5, 15);

            Cedente c = new Cedente("01.924.069/0001-74", "BRUCAI TRANSPORTES E ARMAZEM GERAIS LTDA", "2890", "1108933", "X");
            c.Codigo = "2550661";
            c.Convenio = 2550661;
            c.CodigoTransmissao = "";
            c.ContaBancaria.DigitoAgencia = "";
            c.Carteira = "17-019";
            //c.CPFCNPJ = "01924069000174";
            //c.ContaBancaria.DigitoConta = "7";
            string[] t1 = new string[2] { "25506610000000009", "25506610000000010" };
            string[] t2 = new string[2] { "000010266", "000010267" };

            for (int i = 0; i < 2; i++)
            {
                Boleto b = new Boleto
                {
                    NumeroDocumento = t2[i],
                    Carteira = "17-019",
                    NossoNumero = t1[i],
                    Cedente = c,
                    ValorBoleto = 515.62m,
                    Sacado = new Sacado
                    {
                        CPFCNPJ = "356.733.598-70",
                        Nome = "ERIC VALENZI FILARDI",
                        Endereco = new Endereco
                        {
                            End = "RUA VITO LILLA",
                            Bairro = "VILA AUGUSTA",
                            Cidade = "GUARULHOS",
                            CEP = "07025-040",
                            UF = "SP"
                        },
                    },
                    Banco = new Banco(1),
                    DataVencimento = DateTime.Now,
                    DataProcessamento = DateTime.Now,
                    DataDocumento = DateTime.Now,
                    PercMulta = 10.3124m,
                    JurosMora = 0.3437m,
                    Remessa = new Remessa()
                    {
                        //NumeroLote = i,
                        TipoDocumento = "06",
                        CodigoOcorrencia = "01"
                    }
                };
                b.EspecieDocumento = new EspecieDocumento_BancoBrasil("4");
                b.Especie = "12";
                boletos.Add(b);
            }
            //GeraArquivoCNAB400(new Banco(1), c, boletos, "2550661");
            GeraArquivoCNAB240(new Banco(1), c, boletos);
        }
        public void GeraDadosBradesco(TipoArquivo tipoArquivo)
        {
            Cedente objCEDENTE = new Cedente(
                   "12345678000155",
                   "TESTE",
                   "1111",
                   "11234",
                   "1"
                   );
            objCEDENTE.Codigo = "123456";
            objCEDENTE.Convenio = 9;

            Instrucao_Bradesco item1 = new Instrucao_Bradesco(6, 5);

            //Instancia de Boleto
            Boleto objBOLETO = new Boleto();
            //O nosso-numero deve ser de 11 posi��es
            objBOLETO.EspecieDocumento = new EspecieDocumento(237, "12");
            objBOLETO.DataVencimento = DateTime.Now.AddDays(10);
            objBOLETO.ValorBoleto = 90;
            objBOLETO.Carteira = "09";
            objBOLETO.NossoNumero = ("00000012345");
            objBOLETO.Cedente = objCEDENTE;
            //O num do documento deve ser de 10 posi��es
            objBOLETO.NumeroDocumento = "1234567890";
            objBOLETO.NumeroControle = "100";
            //A data do documento � a data de emiss�o do boleto
            objBOLETO.DataDocumento = DateTime.Now;
            //A data de processamento � a data em que foi processado o documento, portanto � da data de emissao do boleto
            objBOLETO.DataProcessamento = DateTime.Now;
            objBOLETO.Sacado = new Sacado("12345678000255", "TESTE SACADO");
            objBOLETO.Sacado.Endereco.End = "END SACADO";
            objBOLETO.Sacado.Endereco.Bairro = "BAIRRO SACADO";
            objBOLETO.Sacado.Endereco.Cidade = "CIDADE SACADO";
            objBOLETO.Sacado.Endereco.CEP = "CEP SACADO";
            objBOLETO.Sacado.Endereco.UF = "RR";

            objBOLETO.PercMulta = 10;
            objBOLETO.JurosMora = 5;
            objBOLETO.Instrucoes.Add(item1);

            objBOLETO.Banco = new Banco(237);

            // nao precisa desta parte no boleto do brasdesco.
            /*objBOLETO.Remessa = new Remessa()
            {
                Ambiente = Remessa.TipoAmbiemte.Producao,
                CodigoOcorrencia = "01",
            };*/

            Boletos objBOLETOS = new Boletos();
            objBOLETOS.Add(objBOLETO);
            objBOLETOS.Add(objBOLETO);

            var mem = new MemoryStream();
            var objREMESSA = new ArquivoRemessa(TipoArquivo.CNAB400);

            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    //GeraArquivoCNAB240(b2.Banco, c, boletos);
                    break;
                case TipoArquivo.CNAB400:
                    GeraArquivoCNAB400(objBOLETO.Banco, objCEDENTE, objBOLETOS, "09");
                    break;
                default:
                    break;
            }

        }
        #endregion Remessa

        #region Retorno
        private void LerRetorno(int codigo)
        {
            try
            {
                Banco bco = new Banco(codigo);

                openFileDialog.FileName = "";
                openFileDialog.Title = "Selecione um arquivo de retorno";
                openFileDialog.Filter = "Arquivos de Retorno (*.ret;*.crt)|*.ret;*.crt|Todos Arquivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    if (radioButtonCNAB400.Checked)
                    {
                        ArquivoRetornoCNAB400 cnab400 = null;
                        if (openFileDialog.CheckFileExists == true)
                        {
                            cnab400 = new ArquivoRetornoCNAB400();
                            cnab400.LinhaDeArquivoLida += new EventHandler<LinhaDeArquivoLidaArgs>(cnab400_LinhaDeArquivoLida);
                            cnab400.LerArquivoRetorno(bco, openFileDialog.OpenFile());
                        }

                        if (cnab400 == null)
                        {
                            MessageBox.Show("Arquivo não processado!");
                            return;
                        }

                        lstReturnFields.Items.Clear();

                        foreach (DetalheRetorno detalhe in cnab400.ListaDetalhe)
                        {
                            ListViewItem li = new ListViewItem(detalhe.NomeSacado.ToString().Trim());
                            li.Tag = detalhe;

                            li.SubItems.Add(detalhe.DataVencimento.ToString("dd/MM/yy"));
                            li.SubItems.Add(detalhe.DataCredito.ToString("dd/MM/yy"));

                            li.SubItems.Add(detalhe.ValorTitulo.ToString("###,###.00"));

                            li.SubItems.Add(detalhe.ValorPago.ToString("###,###.00"));
                            li.SubItems.Add(detalhe.CodigoOcorrencia.ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(detalhe.NossoNumeroComDV); // = detalhe.NossoNumero.ToString() + "-" + detalhe.DACNossoNumero.ToString());
                            li.SubItems.Add(detalhe.NumeroDocumento);
                            lstReturnFields.Items.Add(li);
                        }
                    }
                    else if (radioButtonCNAB240.Checked)
                    {
                        ArquivoRetornoCNAB240 cnab240 = null;
                        if (openFileDialog.CheckFileExists == true)
                        {
                            cnab240 = new ArquivoRetornoCNAB240();
                            cnab240.LinhaDeArquivoLida += new EventHandler<LinhaDeArquivoLidaArgs>(cnab240_LinhaDeArquivoLida);
                            cnab240.LerArquivoRetorno(bco, openFileDialog.OpenFile());
                        }

                        if (cnab240 == null)
                        {
                            MessageBox.Show("Arquivo não processado!");
                            return;
                        }


                        lstReturnFields.Items.Clear();

                        foreach (DetalheRetornoCNAB240 detalhe in cnab240.ListaDetalhes)
                        {
                            ListViewItem li = new ListViewItem(detalhe.SegmentoT.NomeSacado.Trim());
                            li.Tag = detalhe;

                            li.SubItems.Add(detalhe.SegmentoT.DataVencimento.ToString("dd/MM/yy"));
                            li.SubItems.Add(detalhe.SegmentoU.DataCredito.ToString("dd/MM/yy"));
                            li.SubItems.Add(detalhe.SegmentoT.ValorTitulo.ToString("###,###.00"));
                            li.SubItems.Add(detalhe.SegmentoU.ValorPagoPeloSacado.ToString("###,###.00"));
                            li.SubItems.Add(detalhe.SegmentoU.CodigoOcorrenciaSacado.ToString());
                            li.SubItems.Add("");
                            li.SubItems.Add(detalhe.SegmentoT.NossoNumero);
                            lstReturnFields.Items.Add(li);
                        }
                    }
                    MessageBox.Show("Arquivo aberto com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao abrir arquivo de retorno.");
            }
        }

        void cnab240_LinhaDeArquivoLida(object sender, LinhaDeArquivoLidaArgs e)
        {
            MessageBox.Show(e.Linha);
        }

        void cnab400_LinhaDeArquivoLida(object sender, LinhaDeArquivoLidaArgs e)
        {
            MessageBox.Show(e.Linha);
        }
        #endregion Retorno

        #region Exemplos de arquivos de retorno
        public void GeraArquivoCNAB400Itau(Stream arquivo)
        {
            try
            {
                StreamWriter gravaLinha = new StreamWriter(arquivo);

                #region Variáveis

                string _header;
                string _detalhe1;
                string _detalhe2;
                string _detalhe3;
                string _trailer;

                string n275 = new string(' ', 275);
                string n025 = new string(' ', 25);
                string n023 = new string(' ', 23);
                string n039 = new string('0', 39);
                string n026 = new string('0', 26);
                string n090 = new string(' ', 90);
                string n160 = new string(' ', 160);

                #endregion

                #region HEADER

                _header = "02RETORNO01COBRANCA       347700232610        ALLMATECH TECNOLOGIA DA INFORM341BANCO ITAU SA  ";
                _header += "08010800000BPI00000201207";
                _header += n275;
                _header += "000001";

                gravaLinha.WriteLine(_header);

                #endregion

                #region DETALHE

                _detalhe1 = "10201645738000250097700152310        " + n025 + "00000001            112000000000             ";
                _detalhe1 += "I06201207000000000100000000            261207000000002000034134770010000000000500" + n025 + " ";
                _detalhe1 += n039 + "0000000020000" + n026 + "   2112070000      0000000000000POLITEC LTDA                  " + n023 + "               ";
                _detalhe1 += "AA000002";

                gravaLinha.WriteLine(_detalhe1);

                _detalhe2 = "10201645738000250097700152310        " + n025 + "00000002            112000000000             ";
                _detalhe2 += "I06201207000000000100000000            261207000000002000034134770010000000000500" + n025 + " ";
                _detalhe2 += n039 + "0000000020000" + n026 + "   2112070000      0000000000000POLITEC LTDA                  " + n023 + "               ";
                _detalhe2 += "AA000003";

                gravaLinha.WriteLine(_detalhe2);

                _detalhe3 = "10201645738000250097700152310        " + n025 + "00000003            112000000000             ";
                _detalhe3 += "I06201207000000000100000000            261207000000002000034134770010000000000500" + n025 + " ";
                _detalhe3 += n039 + "0000000020000" + n026 + "   2112070000      0000000000000POLITEC LTDA                  " + n023 + "               ";
                _detalhe3 += "AA000004";

                gravaLinha.WriteLine(_detalhe3);

                #endregion

                #region TRAILER

                _trailer = "9201341          0000000300000000060000                  0000000000000000000000        ";
                _trailer += n090 + "0000000000000000000000        000010000000300000000060000" + n160 + "000005";
                ;

                gravaLinha.WriteLine(_trailer);

                #endregion

                gravaLinha.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar arquivo.", ex);
            }
        }
        #endregion Exemplos de arquivos de retorno

        private void impressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NBoleto form = new NBoleto();

            if (radioButtonItau.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonItau.Tag);
            else if (radioButtonUnibanco.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonUnibanco.Tag);
            else if (radioButtonSudameris.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonSudameris.Tag);
            else if (radioButtonSafra.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonSafra.Tag);
            else if (radioButtonReal.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonReal.Tag);
            else if (radioButtonHsbc.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonHsbc.Tag);
            else if (radioButtonBancoBrasil.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonBancoBrasil.Tag);
            else if (radioButtonBradesco.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonBradesco.Tag);
            else if (radioButtonCaixa.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonCaixa.Tag);
            else if (radioButtonBNB.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonBNB.Tag);
            else if (radioButtonSicredi.Checked)
                form.CodigoBanco = Convert.ToInt16(radioButtonSicredi.Tag);


            form.ShowDialog();
        }

        private void cNABToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (radioButtonCNAB400.Checked)
            {
                if (radioButtonItau.Checked)
                    GeraDadosItau(TipoArquivo.CNAB400);
                else if (radioButtonBanrisul.Checked)
                    GeraDadosBanrisul();
                else if (radioButtonCaixa.Checked)
                    GeraDadosCaixa();
                else if (radioButtonSicredi.Checked)
                    GeraDadosSicredi();
                else if (radioButtonBNB.Checked)
                    GeraDadosBancoDoNordeste();
                else if (radioButtonBradesco.Checked)
                    GeraDadosBradesco(TipoArquivo.CNAB400);
                else if (radioButtonSantander.Checked)
                    GeraDadosSantander();
                else if (radioButtonBancoBrasil.Checked)
                    GeraDadosBB();
            }
            else if (radioButtonCNAB240.Checked)
            {
                if (radioButtonItau.Checked)
                    GeraDadosItau(TipoArquivo.CNAB240);
                else if (radioButtonSantander.Checked)
                    GeraDadosSantander();
                else if (radioButtonBanrisul.Checked)
                    MessageBox.Show("Não Implementado!");
                else if (radioButtonCaixa.Checked)
                    GeraDadosCaixa();
                else if (radioButtonBradesco.Checked)
                    GeraDadosBradesco(TipoArquivo.CNAB240);
                else if (radioButtonBancoBrasil.Checked)
                    GeraDadosBB();
            }
        }

        private void lerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (radioButtonItau.Checked)
                LerRetorno(341);
            else if (radioButtonSudameris.Checked)
                LerRetorno(347);
            else if (radioButtonSantander.Checked)
                LerRetorno(33);
            else if (radioButtonReal.Checked)
                LerRetorno(356);
            else if (radioButtonCaixa.Checked)
                LerRetorno(104);
            else if (radioButtonBradesco.Checked)
                LerRetorno(237);
            else if (radioButtonSicredi.Checked)
                LerRetorno(748);
            else if (radioButtonBanrisul.Checked)
                LerRetorno(041);
            else if (radioButtonBNB.Checked)
                LerRetorno(4);
            else if (radioButtonBancoBrasil.Checked)
                LerRetorno(1);
        }

        private void gerarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Arquivos de Retorno (*.ret)|*.ret|Todos Arquivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (radioButtonCNAB400.Checked)
                {
                    if (radioButtonItau.Checked)
                        GeraArquivoCNAB400Itau(saveFileDialog.OpenFile());
                }
                else
                {
                    //if (radioButtonSantander.Checked)
                    //    GeraArquivoCNAB240Santander(saveFileDialog.OpenFile());

                }

                MessageBox.Show("Arquivo gerado com sucesso!", "Teste", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}