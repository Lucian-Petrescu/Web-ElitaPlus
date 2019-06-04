﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace Antivirus.UpdateProduct
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest", ConfigurationName:="Antivirus.UpdateProduct.UpdateProduct")>  _
    Public Interface UpdateProduct
        
        'CODEGEN: Generating message contract since the operation UpdateProduct is neither RPC nor document wrapped.
        <System.ServiceModel.OperationContractAttribute(Action:="UpdateProduct", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function UpdateProduct(ByVal request As Antivirus.UpdateProduct.UpdateProductRequest1) As Antivirus.UpdateProduct.UpdateProductResponse1
    End Interface
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequest
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private userAuthorizationField As UpdateProductRequestUserAuthorization
        
        Private subscriberInfoField As UpdateProductRequestSubscriberInfo
        
        Private productInfoField As UpdateProductRequestProductInfo
        
        Private deviceInfoField As UpdateProductRequestDeviceInfo
        
        Private carrierField As UpdateProductRequestCarrier
        
        Private transactionField As UpdateProductRequestTransaction
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property UserAuthorization() As UpdateProductRequestUserAuthorization
            Get
                Return Me.userAuthorizationField
            End Get
            Set
                Me.userAuthorizationField = value
                Me.RaisePropertyChanged("UserAuthorization")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property SubscriberInfo() As UpdateProductRequestSubscriberInfo
            Get
                Return Me.subscriberInfoField
            End Get
            Set
                Me.subscriberInfoField = value
                Me.RaisePropertyChanged("SubscriberInfo")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property ProductInfo() As UpdateProductRequestProductInfo
            Get
                Return Me.productInfoField
            End Get
            Set
                Me.productInfoField = value
                Me.RaisePropertyChanged("ProductInfo")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property DeviceInfo() As UpdateProductRequestDeviceInfo
            Get
                Return Me.deviceInfoField
            End Get
            Set
                Me.deviceInfoField = value
                Me.RaisePropertyChanged("DeviceInfo")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=4)>  _
        Public Property Carrier() As UpdateProductRequestCarrier
            Get
                Return Me.carrierField
            End Get
            Set
                Me.carrierField = value
                Me.RaisePropertyChanged("Carrier")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=5)>  _
        Public Property Transaction() As UpdateProductRequestTransaction
            Get
                Return Me.transactionField
            End Get
            Set
                Me.transactionField = value
                Me.RaisePropertyChanged("Transaction")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequestUserAuthorization
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private userIdField As String
        
        Private passwordField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property UserId() As String
            Get
                Return Me.userIdField
            End Get
            Set
                Me.userIdField = value
                Me.RaisePropertyChanged("UserId")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property Password() As String
            Get
                Return Me.passwordField
            End Get
            Set
                Me.passwordField = value
                Me.RaisePropertyChanged("Password")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequestSubscriberInfo
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private subscriberIdField As String
        
        Private firstNameField As String
        
        Private lastNameField As String
        
        Private streetField As String
        
        Private postalCodeField As String
        
        Private regionField As String
        
        Private requestedPasswordField As String
        
        Private preferredLanguageField As String
        
        Private emailAddressField As String
        
        Private oldPhoneNumberField As String
        
        Private phoneNumberField As String
        
        Private countryCodeField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property SubscriberId() As String
            Get
                Return Me.subscriberIdField
            End Get
            Set
                Me.subscriberIdField = value
                Me.RaisePropertyChanged("SubscriberId")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property FirstName() As String
            Get
                Return Me.firstNameField
            End Get
            Set
                Me.firstNameField = value
                Me.RaisePropertyChanged("FirstName")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property LastName() As String
            Get
                Return Me.lastNameField
            End Get
            Set
                Me.lastNameField = value
                Me.RaisePropertyChanged("LastName")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property Street() As String
            Get
                Return Me.streetField
            End Get
            Set
                Me.streetField = value
                Me.RaisePropertyChanged("Street")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=4)>  _
        Public Property PostalCode() As String
            Get
                Return Me.postalCodeField
            End Get
            Set
                Me.postalCodeField = value
                Me.RaisePropertyChanged("PostalCode")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=5)>  _
        Public Property Region() As String
            Get
                Return Me.regionField
            End Get
            Set
                Me.regionField = value
                Me.RaisePropertyChanged("Region")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=6)>  _
        Public Property RequestedPassword() As String
            Get
                Return Me.requestedPasswordField
            End Get
            Set
                Me.requestedPasswordField = value
                Me.RaisePropertyChanged("RequestedPassword")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=7)>  _
        Public Property PreferredLanguage() As String
            Get
                Return Me.preferredLanguageField
            End Get
            Set
                Me.preferredLanguageField = value
                Me.RaisePropertyChanged("PreferredLanguage")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=8)>  _
        Public Property EmailAddress() As String
            Get
                Return Me.emailAddressField
            End Get
            Set
                Me.emailAddressField = value
                Me.RaisePropertyChanged("EmailAddress")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=9)>  _
        Public Property OldPhoneNumber() As String
            Get
                Return Me.oldPhoneNumberField
            End Get
            Set
                Me.oldPhoneNumberField = value
                Me.RaisePropertyChanged("OldPhoneNumber")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=10)>  _
        Public Property PhoneNumber() As String
            Get
                Return Me.phoneNumberField
            End Get
            Set
                Me.phoneNumberField = value
                Me.RaisePropertyChanged("PhoneNumber")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=11)>  _
        Public Property CountryCode() As String
            Get
                Return Me.countryCodeField
            End Get
            Set
                Me.countryCodeField = value
                Me.RaisePropertyChanged("CountryCode")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequestProductInfo
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private vendorNameField As UpdateProductRequestProductInfoVendorName
        
        Private vendorNameFieldSpecified As Boolean
        
        Private productIdField As String
        
        Private productTypeField As UpdateProductRequestProductInfoProductType
        
        Private startDatetimeField As Date
        
        Private startDatetimeFieldSpecified As Boolean
        
        Private endDatetimeField As Date
        
        Private endDatetimeFieldSpecified As Boolean
        
        Private activationCodeField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property VendorName() As UpdateProductRequestProductInfoVendorName
            Get
                Return Me.vendorNameField
            End Get
            Set
                Me.vendorNameField = value
                Me.RaisePropertyChanged("VendorName")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()>  _
        Public Property VendorNameSpecified() As Boolean
            Get
                Return Me.vendorNameFieldSpecified
            End Get
            Set
                Me.vendorNameFieldSpecified = value
                Me.RaisePropertyChanged("VendorNameSpecified")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property ProductId() As String
            Get
                Return Me.productIdField
            End Get
            Set
                Me.productIdField = value
                Me.RaisePropertyChanged("ProductId")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property ProductType() As UpdateProductRequestProductInfoProductType
            Get
                Return Me.productTypeField
            End Get
            Set
                Me.productTypeField = value
                Me.RaisePropertyChanged("ProductType")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property StartDatetime() As Date
            Get
                Return Me.startDatetimeField
            End Get
            Set
                Me.startDatetimeField = value
                Me.RaisePropertyChanged("StartDatetime")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()>  _
        Public Property StartDatetimeSpecified() As Boolean
            Get
                Return Me.startDatetimeFieldSpecified
            End Get
            Set
                Me.startDatetimeFieldSpecified = value
                Me.RaisePropertyChanged("StartDatetimeSpecified")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=4)>  _
        Public Property EndDatetime() As Date
            Get
                Return Me.endDatetimeField
            End Get
            Set
                Me.endDatetimeField = value
                Me.RaisePropertyChanged("EndDatetime")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlIgnoreAttribute()>  _
        Public Property EndDatetimeSpecified() As Boolean
            Get
                Return Me.endDatetimeFieldSpecified
            End Get
            Set
                Me.endDatetimeFieldSpecified = value
                Me.RaisePropertyChanged("EndDatetimeSpecified")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=5)>  _
        Public Property ActivationCode() As String
            Get
                Return Me.activationCodeField
            End Get
            Set
                Me.activationCodeField = value
                Me.RaisePropertyChanged("ActivationCode")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Public Enum UpdateProductRequestProductInfoVendorName
        
        '''<remarks/>
        Kaspersky
        
        '''<remarks/>
        AVG
        
        '''<remarks/>
        McAfee
    End Enum
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Public Enum UpdateProductRequestProductInfoProductType
        
        '''<remarks/>
        Antivirus
    End Enum
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequestDeviceInfo
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private mEIdField As String
        
        Private deviceTypeField As String
        
        Private makeField As String
        
        Private modelField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property MEId() As String
            Get
                Return Me.mEIdField
            End Get
            Set
                Me.mEIdField = value
                Me.RaisePropertyChanged("MEId")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property DeviceType() As String
            Get
                Return Me.deviceTypeField
            End Get
            Set
                Me.deviceTypeField = value
                Me.RaisePropertyChanged("DeviceType")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property Make() As String
            Get
                Return Me.makeField
            End Get
            Set
                Me.makeField = value
                Me.RaisePropertyChanged("Make")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property Model() As String
            Get
                Return Me.modelField
            End Get
            Set
                Me.modelField = value
                Me.RaisePropertyChanged("Model")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequestCarrier
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private carrierNameField As String
        
        Private carrierIdField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property CarrierName() As String
            Get
                Return Me.carrierNameField
            End Get
            Set
                Me.carrierNameField = value
                Me.RaisePropertyChanged("CarrierName")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property CarrierId() As String
            Get
                Return Me.carrierIdField
            End Get
            Set
                Me.carrierIdField = value
                Me.RaisePropertyChanged("CarrierId")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductRequest")>  _
    Partial Public Class UpdateProductRequestTransaction
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private transactionIdField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property TransactionId() As String
            Get
                Return Me.transactionIdField
            End Get
            Set
                Me.transactionIdField = value
                Me.RaisePropertyChanged("TransactionId")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
        "as.UpdateProductResponse")>  _
    Partial Public Class UpdateProductResponse
        Inherits Object
        Implements System.ComponentModel.INotifyPropertyChanged
        
        Private activationCodeField As String
        
        Private vendorNameField As String
        
        Private productTypeField As String
        
        Private productIdField As String
        
        Private mEIdField As String
        
        Private transactionIdField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=0)>  _
        Public Property ActivationCode() As String
            Get
                Return Me.activationCodeField
            End Get
            Set
                Me.activationCodeField = value
                Me.RaisePropertyChanged("ActivationCode")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=1)>  _
        Public Property VendorName() As String
            Get
                Return Me.vendorNameField
            End Get
            Set
                Me.vendorNameField = value
                Me.RaisePropertyChanged("VendorName")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=2)>  _
        Public Property ProductType() As String
            Get
                Return Me.productTypeField
            End Get
            Set
                Me.productTypeField = value
                Me.RaisePropertyChanged("ProductType")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=3)>  _
        Public Property ProductId() As String
            Get
                Return Me.productIdField
            End Get
            Set
                Me.productIdField = value
                Me.RaisePropertyChanged("ProductId")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=4)>  _
        Public Property MEId() As String
            Get
                Return Me.mEIdField
            End Get
            Set
                Me.mEIdField = value
                Me.RaisePropertyChanged("MEId")
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, Order:=5)>  _
        Public Property TransactionId() As String
            Get
                Return Me.transactionIdField
            End Get
            Set
                Me.transactionIdField = value
                Me.RaisePropertyChanged("TransactionId")
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class UpdateProductRequest1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
            "as.UpdateProductRequest", Order:=0)>  _
        Public UpdateProductRequest As Antivirus.UpdateProduct.UpdateProductRequest
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal UpdateProductRequest As Antivirus.UpdateProduct.UpdateProductRequest)
            MyBase.New
            Me.UpdateProductRequest = UpdateProductRequest
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class UpdateProductResponse1
        
        <System.ServiceModel.MessageBodyMemberAttribute([Namespace]:="http://Assurant.Integration.Wireless.VendorIntegration.UpdateProduct.Common.Schem"& _ 
            "as.UpdateProductResponse", Order:=0)>  _
        Public UpdateProductResponse As Antivirus.UpdateProduct.UpdateProductResponse
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal UpdateProductResponse As Antivirus.UpdateProduct.UpdateProductResponse)
            MyBase.New
            Me.UpdateProductResponse = UpdateProductResponse
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface UpdateProductChannel
        Inherits Antivirus.UpdateProduct.UpdateProduct, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class UpdateProductClient
        Inherits System.ServiceModel.ClientBase(Of Antivirus.UpdateProduct.UpdateProduct)
        Implements Antivirus.UpdateProduct.UpdateProduct
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function Antivirus_UpdateProduct_UpdateProduct_UpdateProduct(ByVal request As Antivirus.UpdateProduct.UpdateProductRequest1) As Antivirus.UpdateProduct.UpdateProductResponse1 Implements Antivirus.UpdateProduct.UpdateProduct.UpdateProduct
            Return MyBase.Channel.UpdateProduct(request)
        End Function
        
        Public Function UpdateProduct(ByVal UpdateProductRequest As Antivirus.UpdateProduct.UpdateProductRequest) As Antivirus.UpdateProduct.UpdateProductResponse
            Dim inValue As Antivirus.UpdateProduct.UpdateProductRequest1 = New Antivirus.UpdateProduct.UpdateProductRequest1()
            inValue.UpdateProductRequest = UpdateProductRequest
            Dim retVal As Antivirus.UpdateProduct.UpdateProductResponse1 = CType(Me,Antivirus.UpdateProduct.UpdateProduct).UpdateProduct(inValue)
            Return retVal.UpdateProductResponse
        End Function
    End Class
End Namespace
