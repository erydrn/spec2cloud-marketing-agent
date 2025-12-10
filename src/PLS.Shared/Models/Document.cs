using PLS.Shared.Enums;

namespace PLS.Shared.Models;

/// <summary>
/// Represents a document in the system.
/// </summary>
public class Document
{
    /// <summary>Unique identifier for the document</summary>
    public Guid Id { get; set; }
    
    /// <summary>Document type</summary>
    public DocumentType DocumentType { get; set; }
    
    /// <summary>Document filename</summary>
    public string FileName { get; set; } = string.Empty;
    
    /// <summary>Document file size in bytes</summary>
    public long FileSize { get; set; }
    
    /// <summary>MIME type of the document</summary>
    public string MimeType { get; set; } = string.Empty;
    
    /// <summary>Blob storage URI or path</summary>
    public string StorageUrl { get; set; } = string.Empty;
    
    /// <summary>Associated case ID</summary>
    public Guid? CaseId { get; set; }
    
    /// <summary>Associated client ID</summary>
    public Guid? ClientId { get; set; }
    
    /// <summary>Document version number</summary>
    public int Version { get; set; }
    
    /// <summary>Description or notes about the document</summary>
    public string? Description { get; set; }
    
    /// <summary>Tags for categorization</summary>
    public string? Tags { get; set; }
    
    /// <summary>Whether document has been reviewed</summary>
    public bool IsReviewed { get; set; }
    
    /// <summary>Date document was reviewed</summary>
    public DateTime? ReviewedDate { get; set; }
    
    /// <summary>User who reviewed the document</summary>
    public string? ReviewedBy { get; set; }
    
    /// <summary>Date document was uploaded</summary>
    public DateTime UploadedDate { get; set; }
    
    /// <summary>User who uploaded the document</summary>
    public string? UploadedBy { get; set; }
    
    /// <summary>Navigation property to case</summary>
    public Case? Case { get; set; }
}
