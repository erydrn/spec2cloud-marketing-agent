namespace PLS.Shared.Models;

/// <summary>
/// Represents a client in the system.
/// </summary>
public class Client
{
    /// <summary>Unique identifier for the client</summary>
    public Guid Id { get; set; }
    
    /// <summary>Client title (Mr, Mrs, Ms, Dr, etc.)</summary>
    public string? Title { get; set; }
    
    /// <summary>First name</summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>Last name</summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>Email address</summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>Primary phone number</summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>Mobile phone number</summary>
    public string? MobileNumber { get; set; }
    
    /// <summary>Current residential address</summary>
    public string? Address { get; set; }
    
    /// <summary>Date of birth</summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>National Insurance number</summary>
    public string? NationalInsuranceNumber { get; set; }
    
    /// <summary>ID verification completed</summary>
    public bool IdVerificationCompleted { get; set; }
    
    /// <summary>Date ID verification was completed</summary>
    public DateTime? IdVerificationDate { get; set; }
    
    /// <summary>AML check completed</summary>
    public bool AmlCheckCompleted { get; set; }
    
    /// <summary>Date AML check was completed</summary>
    public DateTime? AmlCheckDate { get; set; }
    
    /// <summary>Associated company ID if applicable</summary>
    public Guid? CompanyId { get; set; }
    
    /// <summary>Date client record was created</summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>Date client record was last modified</summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>Navigation property to company</summary>
    public Company? Company { get; set; }
    
    /// <summary>Navigation property to cases</summary>
    public ICollection<Case> Cases { get; set; } = new List<Case>();
}
