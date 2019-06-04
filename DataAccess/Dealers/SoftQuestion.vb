'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

#if DataEntities
Partial Public Class SoftQuestion
         Inherits BaseEntity
         Implements IDealerEntity, IRecordCreateModifyInfo 
    Public Property SoftQuestionId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return SoftQuestionId
		End Get
	End Property
    Public Property SoftQuestionGroupId As System.Guid
    Public Property ParentId As Nullable(Of System.Guid)
    Public Property ChildOrder As Decimal
    Public Property Description As String
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property Code As String
    Public Property QUESTION_TYPE_ID As Nullable(Of System.Guid)
    Public Property IMPACTS_CLAIM_ID As Nullable(Of System.Guid)
    Public Property ANSWER_TYPE_ID As Nullable(Of System.Guid)
    Public Property CUSTOMER_MESSAGE As String
    Public Property ENTITY_ATTRIBUTE_ID As Nullable(Of System.Guid)
    Public Property SEARCH_TAGS As String
    Public Property Effective As Nullable(Of Date)
    Public Property Expiration As Nullable(Of Date)

    Public Overridable Property IssueQuestions As ICollection(Of IssueQuestion) = New HashSet(Of IssueQuestion)
    Public Overridable Property SoftQuestion1 As ICollection(Of SoftQuestion) = New HashSet(Of SoftQuestion)
    Public Overridable Property SoftQuestion2 As SoftQuestion

End Class
#End If
