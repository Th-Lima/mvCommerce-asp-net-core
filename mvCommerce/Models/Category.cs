using mvCommerce.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models
{
    public class Category
    {
        [Display(Name = "Código")]
        public int Id{ get; set; }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        //TODO - Criate validation - Category name unique on database
        public string Name { get; set; }
      
        /*
         * www.mvCommerce.com.br/category/1
         * EX -> www.mvCommerce.com.br/category/toys -> friendly url with slug
         */
         //TODO - CRIAR VALIDAÇÃO DE SLUG ÚNICO OU GERAÇÃO AUTOMÁTICO DO SLUG
        [Display(Name = "Slug")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public string Slug { get; set; }

        /*
         * Self relantionship
         * Ex: 
         * - 1- Computing - father: null
         * -- 2- Mouse - father: 1
         * --- 3- Mouse Gamer - father: 2
         * ---- 4- Wireless Mouse - father: 2 
         */
         [Display(Name = "Categoria Pai")]
        public int? CategoryFatherId { get; set; }

        /*
         * ORM - EntityFrameworkCore
         */
         [ForeignKey("CategoryFatherId")]
         public virtual Category CategoryFather { get; set; }
    }
}
