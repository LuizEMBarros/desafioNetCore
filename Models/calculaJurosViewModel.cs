using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ConsomeApis.Models
{
    public class calculaJurosViewModel
    {
        [Required(ErrorMessage = "*Campo obrigatório", AllowEmptyStrings = false)]
        [Range(0, 36, ErrorMessage = "Atenção de 1 a 36 meses!")]
        public int Meses { get; set; }


      
        [Required(ErrorMessage = "*Campo obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Currency, ErrorMessage = "*Valor inválido!")]
        [Column(TypeName = "decimal(18, 2)")]
        public double ValorInicial { get; set; }
        public double ValorTaxa{ get; set; }
        public string ValorFinal{ get; set; }

    
    }
}
