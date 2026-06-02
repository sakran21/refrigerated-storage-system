using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class UpdateCustomerRequest
{
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string IdType { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string IdNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string EmergencyContactName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string EmergencyContactPhone { get; set; } = string.Empty;
}