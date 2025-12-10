using PLS.Shared.Enums;

namespace PLS.Shared.Models;

/// <summary>
/// Represents a conveyancing case in the system.
/// </summary>
public class Case
{
    /// <summary>Unique identifier for the case</summary>
    public Guid Id { get; set; }
    
    /// <summary>Human-readable case reference</summary>
    public string CaseReference { get; set; } = string.Empty;
    
    /// <summary>Type of transaction</summary>
    public CaseType CaseType { get; set; }
    
    /// <summary>Current status of the case</summary>
    public CaseStatus Status { get; set; }
    
    /// <summary>Lead that was converted to this case</summary>
    public Guid? LeadId { get; set; }
    
    /// <summary>Primary client for this case</summary>
    public Guid ClientId { get; set; }
    
    /// <summary>Property address</summary>
    public string PropertyAddress { get; set; } = string.Empty;
    
    /// <summary>Property purchase/sale price</summary>
    public decimal PropertyPrice { get; set; }
    
    /// <summary>Agreed legal fee</summary>
    public decimal AgreedFee { get; set; }
    
    /// <summary>Target completion date</summary>
    public DateTime? TargetCompletionDate { get; set; }
    
    /// <summary>Actual completion date</summary>
    public DateTime? ActualCompletionDate { get; set; }
    
    /// <summary>Date contracts were exchanged</summary>
    public DateTime? ExchangeDate { get; set; }
    
    /// <summary>Mortgage lender name</summary>
    public string? MortgageLender { get; set; }
    
    /// <summary>Mortgage amount</summary>
    public decimal? MortgageAmount { get; set; }
    
    /// <summary>Other party solicitor name</summary>
    public string? OtherPartySolicitor { get; set; }
    
    /// <summary>Other party solicitor reference</summary>
    public string? OtherPartySolicitorRef { get; set; }
    
    /// <summary>Estate agent name</summary>
    public string? EstateAgent { get; set; }
    
    /// <summary>Assigned legal team member</summary>
    public string? AssignedTo { get; set; }
    
    /// <summary>Case notes</summary>
    public string? Notes { get; set; }
    
    /// <summary>Date case was created</summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>Date case was last modified</summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>User who created the case</summary>
    public string? CreatedBy { get; set; }
    
    /// <summary>Navigation property to lead</summary>
    public Lead? Lead { get; set; }
    
    /// <summary>Navigation property to client</summary>
    public Client? Client { get; set; }
    
    /// <summary>Navigation property to documents</summary>
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    
    /// <summary>Navigation property to agent tasks</summary>
    public ICollection<AgentTask> AgentTasks { get; set; } = new List<AgentTask>();
}
