
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ClientePathBit.Models;

public class Cliente
{
    

    [Required(ErrorMessage = "{0} is Required")]
    public string Nome { get; set; } = null!;


    [Required(ErrorMessage = "{0} is Required")]
    [DataType(DataType.Date)]
    public DateTime Nascimento { get; set; }


    [Required(ErrorMessage = "{0} is Required")]
    public string CPF { get; set; } = null!;

    [Required(ErrorMessage = "{0} is Required")]
    public string Telefone { get; set; } = null!;

    [Required(ErrorMessage = "{0} is Required")]
    public DadosFinanceiros Financeiro { get; set; } = null!;

    [Required(ErrorMessage = "{0} is Required")]
    public DadosEndereco Endereco { get; set; } = null!;

    [Required(ErrorMessage = "{0} is Required")]
    public DadosSeguranca Seguranca { get; set; } = null!;

}
public class DadosFinanceiros
{
    [Required(ErrorMessage = "{0} is Required")]
    [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
    public Double Renda { get; set; } = 0.0;


    [Required(ErrorMessage = "{0} is Required")]
    [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
    public Double Patrimonio { get; set; } = 0.0;
}

public class DadosEndereco
{

    [Required(ErrorMessage = "{0} is Required")]
    public string Rua { get; set; } = null!;


    [Required(ErrorMessage = "{0} is Required")]
    [StringLength(8, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    public string Numero { get; set; } = null!;


    [Required(ErrorMessage = "{0} is Required")]
    public string Bairro { get; set; } = null!;


    [Required(ErrorMessage = "{0} is Required")]
    public string Cidade { get; set; } = null!;


    [Required(ErrorMessage = "{0} is Required")]
    public string Estado { get; set; } = null!;


    [Required(ErrorMessage = "{0} is Required")]
    [StringLength(8, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
    public string CEP { get; set; } = null!;
}

public class DadosSeguranca
{
    [Required(ErrorMessage = "{0} is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "{0} is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 8)]
    [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\]*+\\/|!\"£$%^&*()#[@~'?><,.=_-]).{6,}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = null!;

    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 8)]
    [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\]*+\\/|!\"£$%^&*()#[@~'?><,.=_-]).{6,}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
    [DataType(DataType.Password)]
    [Compare("Senha")]
    public string ConfirmacaoSenha { get; set; } = null!;
}
