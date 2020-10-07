Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization

Namespace SpecializedServices
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/DataContracts", Name:="CertClaimInfoRequestDC")>
    Public Class CertClaimInfoRequestDC

#Region "DataMember"
        Private _CompanyCode  As String
        Private _SerialNumber As String
        Private _PhoneNumber As String
        

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Company_Code", Order:=1)> _
        Public Property Company_Code() As String
            Get
                Return _CompanyCode
            End Get
            Set(value As String)
                'If String.IsNullOrEmpty(value) Then
                '    Throw New ArgumentNullException()
                'End If
                _CompanyCode = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Serial_Number", Order:=2)> _
        Public Property Serial_Number() As String
            Get
                Return _SerialNumber
            End Get
            Set(value As String)
                'If String.IsNullOrEmpty(value) Then
                '    Throw New ArgumentNullException()
                'End If
                _SerialNumber = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="Phone_Number", Order:=3)> _
        Public Property Phone_Number() As String
            Get
                Return _PhoneNumber
            End Get
            Set(value As String)
                'If String.IsNullOrEmpty(value) Then
                '    Throw New ArgumentNullException()
                'End If
                _PhoneNumber = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=false, IsRequired:=False, Name:="Tax_Id", Order:=4)> 
        Public Property TaxId As String

        <DataMember(EmitDefaultValue:=false, IsRequired:=False, Name:="Claim_Status", Order:=5)> 
        Public Property ClaimStaus As ClaimStatusType
#End Region

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/DataContracts")>
    Public  Enum ClaimStatusType
        <EnumMember()>
        All
        <EnumMember()>
        Active
        <EnumMember()>
        Pending
        <EnumMember()>
        Closed      
        <EnumMember()>
        Denied        
    End Enum
    
    public Module ClaimStatusTypeExtensions
        <Extension()>
        Function GetCodeString(claimstatus As ClaimStatusType) As string
            select case claimstatus
                Case ClaimStatusType.Active
                    return "A"
                Case ClaimStatusType.Closed
                    Return "C"
                Case ClaimStatusType.Pending
                    return "P"
                Case ClaimStatusType.Denied
                    return "D"
                case ClaimStatusType.All
                    return String.Empty
                Case Else
                    return "UKNOWN"
            End Select

        End Function
    End Module
                      
End Namespace
