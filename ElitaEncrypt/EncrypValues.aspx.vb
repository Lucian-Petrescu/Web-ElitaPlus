Imports RMEncryption

Public Class EncrypValues
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    '*********************************************************************************************************************
    '*** This value is the defaul value that RMEncryption uses. This should be use to set RMEncryption back to default IV
    '*********************************************************************************************************************
    Private Function DefaultIV() As Byte()
        Return New Byte() {10, 61, 235, 120, 122, 120, 80, 248, 13, 182, 196, 212, 176, 46, 23, 85}
    End Function

    Private Sub btEncrypValues_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btEncrypValues.Click
        Dim b_ivb() As Byte
        '  Dim IV_for_IV() As Byte
        Dim Encrypted_IV As String
        Dim sEncryptedText As String = ""

        If txtIV.Text.Length > 0 Then
            RMEncryptor.ivb = DefaultIV() 'IV_for_IV
            Encrypted_IV = RMEncryptor.Encrypt(txtIV.Text) & "1234567890123456"

            '**Get the first 16 characters from the Encrypted IV and convert to bites.
            b_ivb = Encoding.ASCII.GetBytes(Encrypted_IV.Substring(1, 16))
            RMEncryptor.ivb = b_ivb
        Else
            b_ivb = DefaultIV()
            RMEncryptor.ivb = b_ivb
        End If

        sEncryptedText = "<settingsSection>" & vbCrLf _
                         & "<add key=""ELITA_INITVECTOR"" value=""" & txtIV.Text & """>" & vbCrLf

        '<add key="ELITA_INITVECTOR"     value="testIV"></add>
        '<add key="DATABASE_USERID"      value="jBnbSmdF0sA0wcJGHzPGRw=="></add> <!-- elp_app_user-->
        '<add key="DATABASE_PASSWORD"    value="vBlkc2NYEfyDjjRrfxHfCQ=="></add> <!-- elp1ple-->
        '<add key="DATABASE_USERID_EU"   value="Wu+bhL1+X4uWzwPI4BcKQw=="></add> <!-- elp_eu_user-->
        '<add key="DATABASE_PASSWORD_EU" value="vBlkc2NYEfyDjjRrfxHfCQ=="></add> <!-- elp_app_user--> 

        If txtUserId.Text <> "" Then
            sEncryptedText = sEncryptedText & "<add key=""DATABASE_USERID"" value=""" & RMEncryptor.Encrypt(txtUserId.Text) & """>" & vbCrLf
        End If

        If txtPassword.Text <> "" Then
            sEncryptedText = sEncryptedText & "<add key=""DATABASE_PASSWORD"" value=""" & RMEncryptor.Encrypt(txtPassword.Text) & """>" & vbCrLf
        End If

        If txtUserIdEU.Text <> "" Then
            sEncryptedText = sEncryptedText & "<add key=""DATABASE_USERID_EU"" value=""" & RMEncryptor.Encrypt(txtUserIdEU.Text) & """>" & vbCrLf
        End If

        If txtPasswordEU.Text <> "" Then
            sEncryptedText = sEncryptedText & "<add key=""DATABASE_PASSWORD_EU"" value=""" & RMEncryptor.Encrypt(txtPasswordEU.Text) & """>" & vbCrLf
        End If

        txtEncryptedValue.Text = sEncryptedText & "</settingsSection>" & vbCrLf


        'If tx.Text <> "" And Text.Text = "" Then
        '    Try
        '        RMEncryptor.ivb = b_ivb
        '        Decrypt.Text = RMEncryptor.Decrypt(Encrypted_text.Text)

        '    Catch ex As Exception
        '        Decrypt.Text = "Error - Incorrect IV and Encrypted Text conbination."
        '    End Try
        'Else
        '    RMEncryptor.ivb = b_ivb
        '    Encrypted_text.Text = RMEncryptor.Encrypt(Text.Text)

        '    Decrypt.Text = RMEncryptor.Decrypt(Encrypted_text.Text)
        'End If

    End Sub
End Class