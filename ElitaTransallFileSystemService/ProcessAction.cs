using Assurant.Common.Ftp;
using Assurant.ElitaPlus.BusinessObjectsNew;
using Assurant.ElitaPlus.Common;
using Assurant.ElitaPlus.DALObjects;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ElitaTransallFileSystemService
{
    class ProcessAction
    {

        private String _notificationEmail = "";

        public ProcessAction(String notificationEmail)
        {
            _notificationEmail = notificationEmail;
        }

        public void ProcessRejects(string fileName, DataView dv)
        {

            System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient _ser = new System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer);
            string[] _recips = dv[0][TransAllMappingDAL.COL_NAME_LOGFILE_EMAILS].ToString().Split(',');
            FileInfo _fil = new FileInfo(fileName);

            try
            {
                _mail.To.Add(_notificationEmail);
                _mail.Subject = String.Format(TransallMessages.TransallReject, EnvironmentContext.Current.EnvironmentShortName.ToUpper(), AppConfig.ConnType);
                _mail.From = new MailAddress(_notificationEmail);
                _mail.Attachments.Add(new Attachment(_fil.FullName));

                _ser.Send(_mail);
            }
            catch (Exception ex)
            {
                _fil.MoveTo(_fil.FullName + string.Format(".err_{0}", System.DateTime.Now.ToString("yyyyMMddhhmmss")));
                throw ex;
            }
        }

        public void ProcessLogs(string fileName, DataView dv)
        {
            string mailBody = "";

            try
            {
                System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient _ser = new System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer);
                string[] _recips = dv[0][Assurant.ElitaPlus.DALObjects.TransAllMappingDAL.COL_NAME_LOGFILE_EMAILS].ToString().Split(',');

                try
                {
                    if (System.IO.File.Exists(fileName))
                    {
                        mailBody = System.IO.File.ReadAllText(fileName);
                    }
                    else
                    {
                        mailBody = TransallMessages.ReportfileError;
                    }
                }
                catch
                {
                    mailBody = TransallMessages.ReportfileError;
                }

                foreach (string _recip in _recips)
                {
                    string to = "";

                    if (!_recip.Contains("@"))
                    {
                        to = _recip.Trim() + "@assurant.com";
                    }
                    else
                    {
                        to = _recip.Trim();
                    }

                    if (to.Length > 0) { _mail.To.Add(to); }
                }

                if (_mail.To.Count > 0)
                {
                    string filpart;

                    if (fileName.Contains("\\"))
                    {
                        char[] specialChars = new char[] { '\\' };
                        filpart = fileName.TrimEnd(specialChars);
                        filpart = filpart.Substring(filpart.LastIndexOf('\\') + 1);
                    }
                    else
                    {
                        filpart = fileName;
                    }

                    _mail.Subject = String.Format(TransallMessages.TransallFilesLoaded, EnvironmentContext.Current.EnvironmentShortName.ToUpper(), filpart, AppConfig.ConnType);
                    _mail.From = new MailAddress(_notificationEmail);
                    _mail.Body = mailBody;

                    _ser.Send(_mail);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessErrors(FileInfo _fil, string errorMessage, StringBuilder strTrace)
        {
            try
            {
                System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient _ser = new System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer);

                _mail.To.Add(_notificationEmail);

                _mail.Subject = String.Format(TransallMessages.TransallFileError, EnvironmentContext.Current.EnvironmentShortName.ToUpper(), _fil.Name, AppConfig.ConnType);
                _mail.From = new MailAddress(_notificationEmail);
                _mail.Body = errorMessage;

                _ser.Send(_mail);
            }
            catch
            {
                strTrace.AppendLine(TransallMessages.EmailError);
            }
        }

        public void ProcessErrors(string fileName, string errorMessage)
        {
            try
            {
                System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient _ser = new System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer);

                _mail.To.Add(_notificationEmail);

                _mail.Subject = String.Format(TransallMessages.TransallFileError, EnvironmentContext.Current.EnvironmentShortName.ToUpper(), fileName, AppConfig.ConnType);
                _mail.From = new MailAddress(_notificationEmail);
                _mail.Body = errorMessage;

                _ser.Send(_mail);
            }
            catch
            {
                //_eventLog.WriteEntry("Error Sending Email notifying problem file", EventLogEntryType.Error);
            }
        }

        public void ProcessErrors(string errorMessage)
        {
            try
            {
                System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient _ser = new System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer);

                _mail.To.Add(_notificationEmail);

                _mail.Subject = $"[{EnvironmentContext.Current.EnvironmentShortName.ToUpper()} {AppConfig.ConnType}] - TRANSALL FILES ERRORS";
                _mail.From = new MailAddress(_notificationEmail);
                _mail.Body = errorMessage;

                _ser.Send(_mail);
            }
            catch
            {
                //_eventLog.WriteEntry("Error Sending Email notifying problem file", EventLogEntryType.Error);
            }
        }

        public Boolean reTryFTP(string fileName, DataView dv)
        {
          
            FileInfo fil = new FileInfo(fileName);
            String uName, pass, altUriPrefix, altUriString, altUser, altPass;

            altUriPrefix = string.Empty;
            altUriString = string.Empty;
            altUser = string.Empty;
            altPass = string.Empty;

            try
            {
                //Send file to Elita through sFtp
                uName = AppConfig.UnixServer.UserId;
                pass = AppConfig.UnixServer.Password;

                //sFtp to Elita
                sFtp objUnixFTP = new sFtp(AppConfig.UnixServer.HostName, AppConfig.UnixServer.FtpDirectory, uName, pass, 22);
                objUnixFTP.UploadFile(fileName);

                //Create & upload trigger file to Elita through sFtp
                string trcFileName = fileName;
                trcFileName = trcFileName.Replace(".txt", ".TRC");

                System.IO.File.WriteAllBytes(trcFileName, System.Text.Encoding.UTF8.GetBytes(LookupListNew.GetCodeFromId(
                                  LookupListNew.DropdownLookupList(LookupListNew.LK_TRANSALL_LAYOUT_CODES,
                                  ElitaPlusIdentity.Current.ActiveUser.LanguageId, false),
                                  GuidControl.ByteArrayToGuid(dv[0][TransAllMappingDAL.COL_NAME_LAYOUT_CODE])).ToLower()));

                objUnixFTP.UploadFile(trcFileName);

                //Send file to Alternate location through SendFile
                if ((dv[0][TransAllMappingDAL.COL_NAME_FTP_SITE_ID] != DBNull.Value) && ((byte[])dv[0][TransAllMappingDAL.COL_NAME_FTP_SITE_ID] != null))
                {
                    FtpSite _ftpSite = new FtpSite(GuidControl.ByteArrayToGuid((byte[])dv[0][TransAllMappingDAL.COL_NAME_FTP_SITE_ID]));
                    altUser = _ftpSite.UserName;
                    altPass = _ftpSite.Password;

                    altUriPrefix = String.Format("ftp://{0}/{1}", _ftpSite.Host, _ftpSite.Directory.Trim(new char[] { Char.Parse("/") }));
                    altUriPrefix = altUriPrefix.Replace("..", "%2e%2e");
                    altUriString = String.Format("{0}/{1}", altUriPrefix, fil.Name);

                    //SendFile to Elita
                    sendFile(altUser, altPass, altUriString, fileName);

                    //Create and send trigger file to Elita through SendFile
                    byte[] fileData = System.Text.Encoding.UTF8.GetBytes(LookupListNew.GetCodeFromId(
                                      LookupListNew.DropdownLookupList(LookupListNew.LK_TRANSALL_LAYOUT_CODES,
                                      ElitaPlusIdentity.Current.ActiveUser.LanguageId, false),
                                      GuidControl.ByteArrayToGuid(dv[0][TransAllMappingDAL.COL_NAME_LAYOUT_CODE])).ToLower());

                    altUriString = String.Format("{0}/{1}", altUriPrefix, fil.Name.Replace(".txt", ".TRC"));
                    sendFile(altUser, altPass, altUriString, fileData);
                }
            }
            catch (Exception ex)
            {
                string err = string.Format(TransallMessages.FTPAccessError, fileName, ex.Message, ex.StackTrace);
                ProcessErrors(fileName, err);
                throw ex;
            }
            finally
            {
                if (fil != null) { fil = null; }
            }

            return true;
        }

        private Boolean sendFile(String uid, String pass, String uri, String filName)
        {
            Uri _uri = default(Uri);
            FtpWebRequest wftp;
            FileInfo fil = new FileInfo(filName);

            try
            {
                _uri = new Uri(uri, UriKind.Absolute);
                wftp = (FtpWebRequest)WebRequest.Create(_uri);
                wftp.Credentials = new NetworkCredential(uid, pass);
                wftp.Method = WebRequestMethods.Ftp.UploadFile;


                // Instead of reading from the File type use FileStream
                FileStream logFileStream = new FileStream(fil.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                long filLength = fil.Length;
                BinaryReader br = new BinaryReader(logFileStream);

                byte[] bFile = br.ReadBytes((int)filLength);

                System.IO.Stream str = wftp.GetRequestStream();

                str.Write(bFile, 0, bFile.Length);
                str.Close();
                str.Dispose();
                logFileStream.Close();
                wftp = null;

            }
            catch (Exception e)
            {
                string err = string.Format(TransallMessages.FTPAccessErrorTrace, uri, filName, e.Message, e.StackTrace);
                ProcessErrors(filName, err);
                throw e;
            }
            finally
            {
                if (fil != null) { fil = null; }
            }

            return true;
        }

        private Boolean sendFile(String uid, String pass, String uri, byte[] fileData)
        {
            Uri _uri = default(Uri);
            FtpWebRequest wftp;

            try
            {
                _uri = new Uri(uri, UriKind.Absolute);
                wftp = (FtpWebRequest)WebRequest.Create(_uri);
                wftp.Credentials = new NetworkCredential(uid, pass);
                wftp.Method = WebRequestMethods.Ftp.UploadFile;
                
                System.IO.Stream str = wftp.GetRequestStream();

                str.Write(fileData, 0, fileData.Length);
                str.Close();
                str.Dispose();
                wftp = null;

            }
            catch (Exception e)
            {
                string err = string.Format(TransallMessages.FTPProcessError, uri, e.Message, e.StackTrace);
                ProcessErrors(err);
                throw e;
            }

            return true;
        }

        public void ProcessOutput(string fileName, DataView dv)
        {
            bool ret = false;
            ret = reTryFTP(fileName, dv);
        }

    }

   
}
