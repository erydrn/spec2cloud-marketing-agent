namespace PLS.Shared.Enums;

/// <summary>
/// Lead status in the qualification pipeline.
/// </summary>
public enum LeadStatus
{
    /// <summary>New lead, not yet qualified</summary>
    New,
    
    /// <summary>In qualification process</summary>
    InQualification,
    
    /// <summary>Quote sent to prospect</summary>
    QuoteSent,
    
    /// <summary>Follow-up required</summary>
    FollowUpRequired,
    
    /// <summary>Quote agreed</summary>
    QuoteAgreed,
    
    /// <summary>Consent obtained to create case</summary>
    ConsentObtained,
    
    /// <summary>Converted to case</summary>
    Converted,
    
    /// <summary>Lost - not converted</summary>
    Lost,
    
    /// <summary>Disqualified</summary>
    Disqualified
}

/// <summary>
/// Source of the lead.
/// </summary>
public enum LeadSource
{
    /// <summary>Digital advertising campaigns</summary>
    DigitalAds,
    
    /// <summary>Search engine optimization</summary>
    SEO,
    
    /// <summary>Marketing channels</summary>
    Channels,
    
    /// <summary>Business development activities</summary>
    BusinessDevelopment,
    
    /// <summary>Organic traffic</summary>
    Organic,
    
    /// <summary>Strategic partnerships</summary>
    Strategy,
    
    /// <summary>Referral from existing client</summary>
    Referral,
    
    /// <summary>Direct inquiry</summary>
    Direct
}

/// <summary>
/// Type of case.
/// </summary>
public enum CaseType
{
    /// <summary>Purchase transaction</summary>
    Purchase,
    
    /// <summary>Sale transaction</summary>
    Sale,
    
    /// <summary>Remortgage transaction</summary>
    Remortgage,
    
    /// <summary>Transfer of equity</summary>
    TransferOfEquity,
    
    /// <summary>New build purchase</summary>
    NewBuild
}

/// <summary>
/// Status of a case in the workflow.
/// </summary>
public enum CaseStatus
{
    /// <summary>Case created, awaiting assignment</summary>
    Created,
    
    /// <summary>Client onboarding in progress</summary>
    ClientOnboarding,
    
    /// <summary>AML and ID checks in progress</summary>
    AMLChecks,
    
    /// <summary>Contract pack review in progress</summary>
    ContractReview,
    
    /// <summary>Searches and enquiries in progress</summary>
    SearchesAndEnquiries,
    
    /// <summary>Awaiting exchange of contracts</summary>
    AwaitingExchange,
    
    /// <summary>Contracts exchanged</summary>
    Exchanged,
    
    /// <summary>Awaiting completion</summary>
    AwaitingCompletion,
    
    /// <summary>Completed</summary>
    Completed,
    
    /// <summary>Post-completion work in progress</summary>
    PostCompletion,
    
    /// <summary>Registration with Land Registry</summary>
    Registration,
    
    /// <summary>Case archived</summary>
    Archived,
    
    /// <summary>Case cancelled</summary>
    Cancelled
}

/// <summary>
/// Type of document in the system.
/// </summary>
public enum DocumentType
{
    /// <summary>Contract of sale</summary>
    Contract,
    
    /// <summary>Title deeds</summary>
    TitleDeeds,
    
    /// <summary>Search results</summary>
    SearchResults,
    
    /// <summary>Mortgage offer</summary>
    MortgageOffer,
    
    /// <summary>ID document</summary>
    IdentificationDocument,
    
    /// <summary>Proof of funds</summary>
    ProofOfFunds,
    
    /// <summary>Client questionnaire</summary>
    Questionnaire,
    
    /// <summary>Completion statement</summary>
    CompletionStatement,
    
    /// <summary>Land Registry forms</summary>
    LandRegistryForm,
    
    /// <summary>SDLT return</summary>
    SDLTReturn,
    
    /// <summary>Transfer deed</summary>
    TransferDeed,
    
    /// <summary>General correspondence</summary>
    Correspondence,
    
    /// <summary>Other document type</summary>
    Other
}

/// <summary>
/// Status of agent task execution.
/// </summary>
public enum AgentTaskStatus
{
    /// <summary>Task created, not yet started</summary>
    Pending,
    
    /// <summary>Task in progress</summary>
    InProgress,
    
    /// <summary>Task completed successfully</summary>
    Completed,
    
    /// <summary>Task failed with error</summary>
    Failed,
    
    /// <summary>Task cancelled</summary>
    Cancelled,
    
    /// <summary>Task waiting for human input</summary>
    AwaitingHumanInput,
    
    /// <summary>Task timed out</summary>
    TimedOut
}
