namespace PLS.Shared.Models;

/// <summary>
/// Represents a company entity in the system.
/// </summary>
public class Company
{
    /// <summary>Unique identifier for the company</summary>
    public Guid Id { get; set; }
    
    /// <summary>Company registered name</summary>
    public string CompanyName { get; set; } = string.Empty;
    
    /// <summary>Companies House registration number</summary>
    public string? CompanyNumber { get; set; }
    
    /// <summary>Registered office address</summary>
    public string? RegisteredAddress { get; set; }
    
    /// <summary>VAT registration number</summary>
    public string? VatNumber { get; set; }
    
    /// <summary>Company type</summary>
    public string? CompanyType { get; set; }
    
    /// <summary>Date of incorporation</summary>
    public DateTime? IncorporationDate { get; set; }
    
    /// <summary>Company status (Active, Dissolved, etc.)</summary>
    public string? Status { get; set; }
    
    /// <summary>Date company record was created</summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>Date company record was last modified</summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>Navigation property to clients associated with this company</summary>
    public ICollection<Client> Clients { get; set; } = new List<Client>();
}
