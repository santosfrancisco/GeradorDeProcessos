using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeradorDeProcessos.Models
{
	[MetadataType(typeof(AnalisesMetadado))]
	public partial class Analises
	{
	}

	public class AnalisesMetadado
	{
		public int IDAnalise { get; set; }
		[DisplayName("Habite-se")]
		public System.DateTime DataEntrega { get; set; }
		[DisplayName("Valor do financiamento")]
		public decimal ValorFinanciamento { get; set; }
		[DisplayName("Valor total da venda")]
		[Required(ErrorMessage = "Obrigatório informar o valor total da venda")]
		public decimal ValorTotal { get; set; }
		[DisplayName("Valor do saldo devedor")]
		public decimal SaldoDevedor { get; set; }
		[DisplayName("Observação")]
		[StringLength(300, ErrorMessage = "Observação deve conter no máximo 300 caracteres")]
		public string Observacao { get; set; }
		[DisplayName("Tipo de análise")]
		public string TipoAnalise { get; set; }
		[DisplayName("Cliente")]
		public int IDCliente { get; set; }
		[DisplayName("Unidade")]
		public int IDUnidade { get; set; }
		[DisplayName("Usuário")]
		public int IDUsuario { get; set; }
	}
}