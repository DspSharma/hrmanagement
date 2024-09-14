using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Helper
{
    
    public class Email
    {
        #region Constructors-Destructors
        public Email()
        {
            //set defaults 
            myEmail = new System.Net.Mail.MailMessage();
            _MailBodyManualSupply = false;
        }
        //public void Dispose()
        //{
        //    base.Finalize();
        //    myEmail.Dispose();
        //}
        #endregion

        #region  Class Data
        private string _Host;
        private int _Port;
        private string _UserName;
        private string _Password;

        private string _From;
        private string _FromName;
        private string _To;
        private string _ToList;
        private string _Subject;
        private string _CC;
        private string _CCList;
        private string _BCC;
        private string _TemplateDoc;
        private string[] _ArrValues;
        private string _BCCList;
        private bool _MailBodyManualSupply;
        private string _MailBody;
        private string _Attachment;
        private System.Net.Mail.MailMessage myEmail;

        #endregion

        #region Propertie

        public string Host
        {
            set { _Host = value; }
        }
        public int Port
        {
            set { _Port = value; }
        }
        public string UserName
        {
            set { _UserName = value; }
        }
        public string Password
        {
            set { _Password = value; }
        }


        public string From
        {
            set { _From = value; }
        }
        public string FromName
        {
            set { _FromName = value; }
        }
        public string To
        {
            set { _To = value; }
        }
        public string Subject
        {
            set { _Subject = value; }
        }
        public string CC
        {
            set { _CC = value; }
        }
        public string BCC
        {

            set { _BCC = value; }
        }
        public bool MailBodyManualSupply
        {

            set { _MailBodyManualSupply = value; }
        }
        public string MailBody
        {
            set { _MailBody = value; }
        }
        public string EmailTemplateFileName
        {
            //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
            set { _TemplateDoc = value; }
        }
        public string[] ValueArray
        {
            //ARRAY OF VALUES TO REPLACE VARS IN TEMPLATE 
            set { _ArrValues = value; }
        }

        public string AttachFile
        {
            set { _Attachment = value; }
        }

        #endregion

        #region SEND EMAIL
        public async void Send()
        {
            try
            {
                await SendEmailAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SendEmailAsync()
        {
            myEmail.IsBodyHtml = true;

            //set mandatory properties 
            if (_FromName == "")
                _FromName = _From;
            myEmail.From = new MailAddress(_From, _FromName);
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (_CC != "")
            {
                _CCList = _CC.Replace(";", ",");
                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }
            }

            //---Set recipients in BCC List 
            if (_BCC != "")
            {
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                myEmail.Body = GetHtml(_TemplateDoc);
                //& GetHtml("EML_Footer.htm") 
            }

            // set attachment 
            if (_Attachment != null && _Attachment != "")
            {
                Attachment objAttach = new Attachment(_Attachment);
                myEmail.Attachments.Add(objAttach);
            }

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Host = "smtp.sendgrid.net";  // Specify main and backup SMTP servers
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("vickytailor", "Tiku2011#");
            client.EnableSsl = false;



            try
            {
                client.SendMailAsync(myEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            await Task.Delay(0);

        }
        public void SendEmail()
        {
            myEmail.IsBodyHtml = true;

            //set mandatory properties 
            if (_FromName == "")
                _FromName = _From;
            myEmail.From = new MailAddress(_From, _FromName);
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (_CC != "")
            {
                _CCList = _CC.Replace(";", ",");
                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }
            }

            //---Set recipients in BCC List 
            if (_BCC != "")
            {
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                myEmail.Body = GetHtml(_TemplateDoc);
                //& GetHtml("EML_Footer.htm") 
            }

            // set attachment 
            if (_Attachment != null && _Attachment != "")
            {
                Attachment objAttach = new Attachment(_Attachment);
                myEmail.Attachments.Add(objAttach);
            }

            ////Send mail 
            //SmtpClient client = new SmtpClient();
            //client.Send(myEmail);

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();


            //client.Host = System.Web.Configuration.WebConfigurationManager.AppSettings["smtphost"].ToString();
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential(System.Web.Configuration.WebConfigurationManager.AppSettings["smtpuser"].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings["smtppass"].ToString());
            //client.EnableSsl = false;
            //client.Port = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["smtpport"]);


            //--------------------------------------



            //client.Host = "smtp.office365.com";  // Specify main and backup SMTP servers
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("account@bloodvolunteers.store", "Blood2017#");
            //client.EnableSsl = false;


            client.Host = _Host;  // Specify main and backup SMTP servers
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
            client.EnableSsl = true;

            if (_Port != 0)
                client.Port = _Port;

            try
            {
                client.SendMailAsync(myEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        #endregion

        #region GetHtml
        public string GetHtml(string argTemplateDocument)
        {
            int i;
            StreamReader filePtr;
            string fileData = "";
            filePtr = File.OpenText(argTemplateDocument);
            //filePtr = File.OpenText(ConfigurationSettings.AppSettings["EMLPath"] + argTemplateDocument);
            fileData = filePtr.ReadToEnd();

            if ((_ArrValues == null))
            {
                filePtr.Close();
                filePtr = null;
                return fileData;
            }
            else
            {
                for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
                {
                    fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
                }
                filePtr.Close();
                filePtr = null;
                return fileData;
            }
        }


        public string getHtmlFromUrl(string urlAddress)
        {
            string data = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return data;
        }
        #endregion
    }
}


