using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeradorDeProcessos.Models
{
	[MetadataType(typeof(UnidadesMetadado))]
	public partial class Unidades
	{
	}

	public class UnidadesMetadado
	{
		public int IDUnidade { get; set; }
		[Required(ErrorMessage = "Obrigatório informar número da unidade")]
		[StringLength(20, ErrorMessage = "Número da unidade deve possuir no máximo 20 caracteres")]
		[DisplayName("Número")]
		public string Numero { get; set; }
		[DisplayName("Empreendimento")]
		public int IDEmpreendimento { get; set; }
		[DisplayName("Status")]
		public string UnidadeStatus { get; set; }
		[DisplayName("Tipo")]
		public string Tipo { get; set; }
		[DisplayName("Observação")]
		[StringLength(100, ErrorMessage = "Observação deve possuir no máximo 100 caracteres")]
		public string UnidadeObservacao { get; set; }
	}
}