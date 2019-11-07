using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoletoNet;
using System.Web.Mvc;
using Boleto = BoletoNet.Boleto;

namespace Boleto.Net.MVC.Models
{
    public enum Bancos
    {
        BancodoBrasil = 1,
        Banrisul = 41,
        Basa = 3,
        Bradesco = 237,
        BRB = 70,
        Caixa = 104,
        HSBC = 399,
        Itau = 341,
        Real = 356,
        Safra = 422,
        Santander = 33,
        Sicoob = 756,
        Sicred = 748,
        Sudameris = 347,
        Unibanco = 409,
        Semear = 743
    }

    /// <Author>
    /// Sandro Ribeiro - CODTEC SISTEMAS
    /// </Author>
    public class Exemplos
    {
        public Exemplos(int Banco)
        {
            boletoBancario = new BoletoBancario();
            boletoBancario.CodigoBanco = (short)Banco;
        }
        public BoletoBancario boletoBancario { get; set; }

        public BoletoBancario BancodoBrasil()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            #region Exemplo Carteira 16, com nosso número de 11 posições
            /*
         * Nesse exemplo utilizamos a carteira 16 e o nosso número no máximo de 11 posições.
         * Não é necessário informar o numero do convênio e nem o tipo da modalidade.
         * O nosso número tem que ter no máximo 11 posições.
         */

            Cedente c = new Cedente("01.924.069/0001-74", "Empresa de Atacado", "2890", "8", "110893", "X");
            c.Convenio = 2550661;
            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 55.00m, "17-019", "25506610000000012", c);

            #endregion Exemplo Carteira 16, com nosso número de 11 posições

            #region Exemplo Carteira 16, convênio de 6 posições e tipo modalidade 21
            /*
         * Nesse exemplo utilizamos a carteira 16 e o número do convênio de 6 posições.
         * É obrigatório informar o tipo da modalidade 21.
         * O nosso número tem que ter no máximo 10 posições.
         */

            //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //c.Convenio = 123456;

            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01, "16", "09876543210", c);
            //b.TipoModalidade = "21";
            #endregion Exemplo Carteira 16, convênio de 6 posições e tipo modalidade 21

            #region Exemplo Carteira 18, com nosso número de 11 posições
            /*
         * Nesse exemplo utilizamos a carteira 18 e o nosso número no máximo de 11 posições.
         * Não é necessário informar o numero do convênio e nem o tipo da modalidade.
         * O nosso número tem que ter no máximo 11 posições.
         */

            //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01, "18", "09876543210", c);

            #endregion Exemplo Carteira 18, com nosso número de 11 posições

            #region Exemplo Carteira 18, convênio de 6 posições e tipo modalidade 21
            /*
         * Nesse exemplo utilizamos a carteira 18 e o número do convênio de 6 posições.
         * É obrigatório informar o tipo da modalidade 21.
         * O nosso número tem que ter no máximo 10 posições.
         */

            //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //c.Convenio = 123456;
            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01, "18", "09876543210", c);
            //b.TipoModalidade = "21";
            #endregion Exemplo Carteira 18, convênio de 6 posições e tipo modalidade 21


            b.NumeroDocumento = "00007940";

            b.Sacado = new Sacado("02.980.839/0001-69", "Aleff sistemas");
            b.Sacado.Endereco.End = "Rua josephina mandotti";
            b.Sacado.Endereco.Bairro = "Maia";
            b.Sacado.Endereco.Cidade = "Guarulhos";
            b.Sacado.Endereco.CEP = "07115080";
            b.Sacado.Endereco.UF = "SP";

            //Adiciona as instruções ao boleto
            #region Instruções
            ////Protestar
            //Instrucao_BancoBrasil item = new Instrucao_BancoBrasil(9, 5);
            //b.Instrucoes.Add(item);
            ////ImportanciaporDiaDesconto
            //item = new Instrucao_BancoBrasil(30, 0);
            //b.Instrucoes.Add(item);
            ////ProtestarAposNDiasCorridos
            //item = new Instrucao_BancoBrasil(81, 15);
            //b.Instrucoes.Add(item);
            #endregion Instruções

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            boletoBancario.RemoveSimboloMoedaValorDocumento = true;
            boletoBancario.AjustaTamanhoFonte(12, 10, 14, 14);

            return boletoBancario;
        }

        public BoletoBancario Banrisul()
        {
            DateTime vencimento = Convert.ToDateTime("15/02/2019");

            Cedente c = new Cedente("00.100.992/0001-29", "TRANSPORTES KOBRASOL EIRELI", "0515", "", "005864038", "3"); //60058640

            c.Codigo = "0000005864038";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 5.60m, "1", "00000134", c);

            b.Sacado = new Sacado("11.550.874/0001-39", "TBT COM DE PAPAEIS EIREL");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "DF";

            //Adiciona as instruções ao boleto
            #region Instruções
            //Protestar
            //Instrucao_Banrisul item = new Instrucao_Banrisul(9, 10, 0);
            //b.Instrucoes.Add(item);
            #endregion Instruções

            b.NumeroDocumento = "00000033";


            boletoBancario.Boleto = b;
            boletoBancario.Boleto.TipoModalidade = "40";
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Basa()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "12345678", "9");

            c.Codigo = "12548";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 45.50m, "CNR", "125478", c);

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";


            b.NumeroDocumento = "12345678901";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            boletoBancario.Cedente.Endereco = new Endereco()
            {
                End = "Endereço do Cedente",
                Bairro = "Bairro",
                Cidade = "Cidade",
                CEP = "70000000",
                UF = "DF"

            };

            //boletoBancario.MostrarEnderecoCedente = true;

            //boletoBancario.AjustaTamanhoFonte(12, tamanhoFonteInstrucaoImpressao: 14);
            boletoBancario.RemoveSimboloMoedaValorDocumento = false;

            return boletoBancario;
        }

        public BoletoBancario Bradesco()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Instrucao_Bradesco item = new Instrucao_Bradesco(9, 5);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "123456", "7");
            c.Codigo = "13000";


            //Carteiras 
            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1.00m, "09", "01000000001", c);
            b.ValorMulta = 0.10m;
            b.ValorCobrado = 1.10m;
            b.NumeroDocumento = "01000000001";
            b.DataVencimento = new DateTime(2015, 09, 12);

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            item.Descricao += " após " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item); //"Não Receber após o vencimento");

            Instrucao i = new Instrucao(237);
            i.Descricao = "Nova Instrução";
            b.Instrucoes.Add(i);

            /* 
             * A data de vencimento não é usada
             * Usado para mostrar no lugar da data de vencimento o termo "Contra Apresentação";
             * Usado na carteira 06
             */
            boletoBancario.MostrarContraApresentacaoNaDataVencimento = true;

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario BRB()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "208", "", "010357", "6");
            c.Codigo = "13000";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01m, "COB", "119964", c);
            b.NumeroDocumento = "119964";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Caixa()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("01.924.069/0001-74", "BRUCAI TRANSPORTES E ARMAZEM GERAIS LTDA", "3792", "1031", "6");

            c.Codigo = "943870";


            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 508.4300M, "1", "00000000000000004", c);

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //Adiciona as instruções ao boleto
            //#region Instruções
            //Instrucao_Caixa item;
            ////ImportanciaporDiaDesconto
            //item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.Multa, Convert.ToDecimal(0.40));
            //b.Instrucoes.Add(item);
            //item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.JurosdeMora, Convert.ToDecimal(0.01));
            //b.Instrucoes.Add(item);
            //item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.NaoReceberAposNDias, 90);
            //b.Instrucoes.Add(item);
            //#endregion Instruções

            EspecieDocumento_Caixa espDocCaixa = new EspecieDocumento_Caixa();
            b.EspecieDocumento = new EspecieDocumento_Caixa(espDocCaixa.getCodigoEspecieByEnum(EnumEspecieDocumento_Caixa.DuplicataMercantil));
            b.NumeroDocumento = "00001";
            b.DataProcessamento = DateTime.Now;
            b.DataDocumento = DateTime.Now;

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario HSBC()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Minha empresa", "0000", "", "00000", "00");
            // Código fornecido pela agencia, NÃO é o numero da conta
            c.Codigo = "0000000"; // 7 posicoes

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 2, "CNR", "1330001490684", c); //cod documento
            b.NumeroDocumento = "9999999999999"; // nr documento

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Itau()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
            Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
            Cedente c = new Cedente("03.999.930/0001-99", "DIGGO TRANSPORTES e logistica", "3197", "6000", "1");
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "60001";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 8.0m, "109", "00045509", c, new EspecieDocumento(341, "1"));
            b.NumeroDocumento = "00045509";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "2541245"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            b.Instrucoes.Add(item2);

            //juros / descontos

            if (b.ValorDesconto == 0)
            {
                Instrucao_Itau item3 = new Instrucao_Itau(999, 1);
                item3.Descricao += ("1,00 por dia de antecipação.");
                b.Instrucoes.Add(item3);
            }

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Real()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Coloque a Razão Social da sua empresa aqui", "1234", "12345");
            c.Codigo = "12345";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.1m, "57", "123456", c, new EspecieDocumento(356, "9"));
            b.NumeroDocumento = "1234567";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Real");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");
            boletoBancario.Boleto = b;

            EspeciesDocumento ed = EspecieDocumento_Real.CarregaTodas();
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Safra()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "5413000");

            c.Codigo = "13000";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1642, "198", "02592082835", c);
            b.NumeroDocumento = "1008073";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            Instrucao_Safra instrucao = new Instrucao_Safra();
            instrucao.Descricao = "Instrução 1";

            b.Instrucoes.Add(instrucao);


            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Santander()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("26.430.486/0001-91", "AMR Cargas e emcomendas eireli", "1319", "13000900");
            c.Codigo = "98482822";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 4.50m, "101", "000000001955", c);

            //NOSSO NÚMERO
            //############################################################################################################################
            //Número adotado e controlado pelo Cliente, para identificar o título de cobrança.
            //Informação utilizada pelos Bancos para referenciar a identificação do documento objeto de cobrança.
            //Poderá conter número da duplicata, no caso de cobrança de duplicatas, número de apólice, no caso de cobrança de seguros, etc.
            //Esse campo é devolvido no arquivo retorno.
            b.NumeroDocumento = "0000008";

            b.Sacado = new Sacado("02.980.839/0001-69", "Aleff sistemas");
            b.Sacado.Endereco.End = "Rua josephina mandotti";
            b.Sacado.Endereco.Bairro = "Maia";
            b.Sacado.Endereco.Cidade = "Guarulhos";
            b.Sacado.Endereco.CEP = "07115080";
            b.Sacado.Endereco.UF = "SP";

            //Espécie Documento - [R] Recibo
            b.EspecieDocumento = new EspecieDocumento_Santander("2");

            boletoBancario.Boleto = b;
            boletoBancario.MostrarCodigoCarteira = true;

            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Sicoob()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);


            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "4444", "", "", "");

            c.Codigo = "123456";
            c.DigitoCedente = 7;
            c.Carteira = "1";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 10, "1", "897654321", c);
            b.NumeroDocumento = "119964";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Sicred()
        {

            DateTime vencimento = DateTime.Now.AddDays(1);

            //Instrucao_Sicredi item1 = new Instrucao_Sicredi(9, 5);
            //Instrucao_Sicredi item2 = new Instrucao_Sicredi();

            Cedente c = new Cedente("01.924.069/0001-74", "BRUCAI TRANSPORTES E ARMAZEM GERAIS LTDA", "0710", "07914","3");
            c.Codigo = "07914";
            c.ContaBancaria = new ContaBancaria
            {
                Agencia = "0710",
                Conta = "07914",
                DigitoAgencia = "",
                DigitoConta = "3",
                OperacaConta = "68"
            };


            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.1m, "1", "000011", c);
            b.NumeroDocumento = "000011";



            //b.Sacado = new Sacado("356.733.598-70", "Eric Filardi");
            //b.Sacado.Endereco.End = "Endereço do seu Cliente";
            //b.Sacado.Endereco.Bairro = "Bairro";
            //b.Sacado.Endereco.Cidade = "Cidade";
            //b.Sacado.Endereco.CEP = "00000000";
            //b.Sacado.Endereco.UF = "UF";


            //// Exemplo de como adicionar mais informações ao sacado
            //b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "2541245"));

            //item2.Descricao += " " + item1.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            //b.Instrucoes.Add(item1);

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Sudameris()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0501", "6703255");

            c.Codigo = "13000";

            //Nosso número com 7 dígitos
            string nn = "0003020";
            //Nosso número com 13 dígitos
            //nn = "0000000003025";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1642, "198", nn, c);// EnumEspecieDocumento_Sudameris.DuplicataMercantil);
            b.NumeroDocumento = "1008073";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Sudameris");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Unibanco()
        {
            // ----------------------------------------------------------------------------------------
            // Exemplo 1

            //DateTime vencimento = new DateTime(2001, 12, 31);

            //Cedente c = new Cedente("00.000.000/0000-00", "Next Consultoria", "1234", "5", "123456", "7");
            //c.Codigo = 123456;

            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1000.00, "20", "1803029901", c);
            //b.NumeroDocumento = b.NossoNumero;

            // ----------------------------------------------------------------------------------------
            // Exemplo 2

            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Next Consultoria Ltda.", "0123", "100618", "9");
            c.Codigo = "203167";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 2952.95m, "20", "1803029901", c);
            b.NumeroDocumento = b.NossoNumero;

            // ----------------------------------------------------------------------------------------

            //b.Instrucoes.Add("Não receber após o vencimento");
            //b.Instrucoes.Add("Após o vencimento pague somente no Unibanco");
            //b.Instrucoes.Add("Taxa bancária - R$ 2,95");
            //b.Instrucoes.Add("Emitido pelo componente Boleto.NET");

            // ----------------------------------------------------------------------------------------

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Semear()
        {
            var boleto = new BoletoNet.Boleto();

            boleto.Cedente = new Cedente()
            {
                Codigo = "743",
                MostrarCNPJnoBoleto = true,
                Nome = "BANCO SEMEAR",
                CPFCNPJ = "65825481000110",
                Carteira = "2",
                DigCedente = "9",
                ContaBancaria = new ContaBancaria()
                {
                    Agencia = "001",
                    DigitoAgencia = "0",
                    Conta = "65456",
                    DigitoConta = "5"
                },
                Endereco = new Endereco()
            };

            boleto.LocalPagamento = "Este boleto poderá ser pago em toda a Rede Bancária até 5 dias após o vencimento. ";
            boleto.Instrucoes.Add(new Instrucao_Bradesco()
            {
                Descricao = "Não receber após o vencimento",
                QuantidadeDias = 3
            });

            boleto.ValorBoleto = 251.51M;
            boleto.ValorCobrado = 251.51M;
            boleto.NossoNumero = "35148373401";
            boleto.NumeroDocumento = "051483734";
            boleto.DataVencimento = new DateTime(2017, 12, 4);
            boleto.DataProcessamento = DateTime.Now;
            boleto.DataDocumento = DateTime.Now;
            boleto.Carteira = "03";

            boleto.Sacado = new Sacado()
            {
                CPFCNPJ = "05461883893",
                Nome = "Joãozinho Testador",
                Endereco = new Endereco()
                {
                    Complemento = "Bla bla",
                    Numero = "80",
                    Bairro = "",
                    CEP = "32310535",
                    Cidade = "Contagem",
                    UF = "Minas Gerais",
                }
            };

            boleto.CodigoBarra.CodigoBanco = "743";
            boleto.CodigoBarra.Moeda = 9;
            boleto.CodigoBarra.CampoLivre = "0001023514837340110996818";
            boleto.CodigoBarra.ValorDocumento = "0000025151";
            boleto.CodigoBarra.FatorVencimento = 7363;
            
            var linhaDigitavel = boleto.CodigoBarra.LinhaDigitavelFormatada;
            boleto.CodigoBarra.Codigo = boleto.CodigoBarra.LinhaDigitavelFormatada.Trim().Replace(".",string.Empty).Replace(" ", string.Empty);

            var boletoBancario = new BoletoBancario
            {
                CodigoBanco = 743,
                Boleto = boleto,
                MostrarEnderecoCedente = true,
                MostrarContraApresentacaoNaDataVencimento = false,
                GerarArquivoRemessa = true
            };

            return boletoBancario;
        }
    }
}