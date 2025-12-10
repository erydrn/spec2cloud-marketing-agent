using PLS.Shared.Enums;

namespace PLS.Shared.Models;

/// <summary>
/// Represents an AI agent task in the system.
/// </summary>
public class AgentTask
{
    /// <summary>Unique identifier for the task</summary>
    public Guid Id { get; set; }
    
    /// <summary>Name of the agent executing the task</summary>
    public string AgentName { get; set; } = string.Empty;
    
    /// <summary>Task type or action</summary>
    public string TaskType { get; set; } = string.Empty;
    
    /// <summary>Current status of the task</summary>
    public AgentTaskStatus Status { get; set; }
    
    /// <summary>Associated case ID</summary>
    public Guid? CaseId { get; set; }
    
    /// <summary>Associated lead ID</summary>
    public Guid? LeadId { get; set; }
    
    /// <summary>Task input data (JSON)</summary>
    public string? InputData { get; set; }
    
    /// <summary>Task output data (JSON)</summary>
    public string? OutputData { get; set; }
    
    /// <summary>Error message if task failed</summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>Task execution start time</summary>
    public DateTime? StartTime { get; set; }
    
    /// <summary>Task execution end time</summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>Task execution duration in milliseconds</summary>
    public long? DurationMs { get; set; }
    
    /// <summary>Number of retry attempts</summary>
    public int RetryCount { get; set; }
    
    /// <summary>Parent task ID if this is a sub-task</summary>
    public Guid? ParentTaskId { get; set; }
    
    /// <summary>Priority level (1-10, 10 being highest)</summary>
    public int Priority { get; set; }
    
    /// <summary>Date task was created</summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>Date task was last modified</summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>Navigation property to case</summary>
    public Case? Case { get; set; }
}
