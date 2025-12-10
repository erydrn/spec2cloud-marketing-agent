using PLS.Shared.Enums;

namespace PLS.Shared.Models;

/// <summary>
/// Represents a potential client lead in the system.
/// </summary>
public class Lead
{
    /// <summary>Unique identifier for the lead</summary>
    public Guid Id { get; set; }
    
    /// <summary>Lead source</summary>
    public LeadSource Source { get; set; }
    
    /// <summary>Current status of the lead</summary>
    public LeadStatus Status { get; set; }
    
    /// <summary>Contact first name</summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>Contact last name</summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>Contact email address</summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>Contact phone number</summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>Property address of interest</summary>
    public string? PropertyAddress { get; set; }
    
    /// <summary>Type of transaction</summary>
    public CaseType? TransactionType { get; set; }
    
    /// <summary>Estimated property value</summary>
    public decimal? EstimatedValue { get; set; }
    
    /// <summary>Lead notes and details</summary>
    public string? Notes { get; set; }
    
    /// <summary>Quoted fee amount</summary>
    public decimal? QuotedFee { get; set; }
    
    /// <summary>Date quote was sent</summary>
    public DateTime? QuoteSentDate { get; set; }
    
    /// <summary>Date quote was agreed</summary>
    public DateTime? QuoteAgreedDate { get; set; }
    
    /// <summary>Date consent was obtained</summary>
    public DateTime? ConsentDate { get; set; }
    
    /// <summary>Assigned case ID if converted</summary>
    public Guid? CaseId { get; set; }
    
    /// <summary>Date lead was created</summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>Date lead was last modified</summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>User who created the lead</summary>
    public string? CreatedBy { get; set; }
    
    /// <summary>Navigation property to converted case</summary>
    public Case? Case { get; set; }
}
