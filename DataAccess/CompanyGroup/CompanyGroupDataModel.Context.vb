﻿'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports Assurant.ElitaPlus.DataEntities

Partial Public Class CompanyGroupContext
    Inherits BaseDbContext

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property CompanyGroups() As DbSet(Of CompanyGroup)
    Public Property RiskTypes1() As DbSet(Of RiskType)
    Public Property Manufacturers() As DbSet(Of Manufacturer)
    Public Property PaymentTypes() As DbSet(Of PaymentType)
    Public Property CoverageLosses() As DbSet(Of CoverageLoss)
    Public Property ClaimStatusByGroups() As DbSet(Of ClaimStatusByGroup)
    Public Property DefaultClaimStatus() As DbSet(Of DefaultClaimStatus)

End Class
