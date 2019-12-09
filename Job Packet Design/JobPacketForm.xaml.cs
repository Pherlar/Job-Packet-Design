using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Job_Packet_Design
{
        /// <summary>
    /// Interaction logic for JobPacketForm.xaml
    /// </summary>
    public partial class JobPacketForm : Window
    {
        public static Dictionary<string,string> jobInfoDictionary = new Dictionary<string, string>();
        public static Dictionary<string, string> clientInfoDictionary = new Dictionary<string, string>();
        public JobPacketForm()
        {

            InitializeComponent();
            this.notesScrollViewer.ScrollToEnd();
            this.image_CMSlogo.Source = ToBitmapImage(Resource.CMS_Logo1_Transparency);

            if (connectToDataBase("18870") == true)
            {
                FillOutTextBlocks();
            }
            else
            {
                MessageBox.Show("SQL Database connection failed");
            }   
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void JobPacketWindow_Drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void reassign_button_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Opacity = 100;
        }
        private BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }


        private void FillOutTextBlocks()
        {

            jobNumberLargeTextBlock.Text = jobInfoDictionary["jobNumberText"];

            jobDateTextBlock_output.Text = jobInfoDictionary["jobDateText"];
            jobDescriptionTextBlock_output.Text = jobInfoDictionary["jobDescriptionText"];
            jobNumberTextBlock_output.Text = jobInfoDictionary["jobNumberText"];
            packetRefTextBlock_output.Text = jobInfoDictionary["packetRefText"];
            streetNumberTextBlock_output.Text = jobInfoDictionary["streetNumberText"];
            streetNameTextBlock_output.Text = jobInfoDictionary["streetNameText"];
            suburbTextBlock_output.Text = jobInfoDictionary["suburbText"];
            lotTextBlock_output.Text = jobInfoDictionary["lotText"];
            dPTextBlock_output.Text = jobInfoDictionary["dPText"];
            reTextBlock_output.Text = jobInfoDictionary["reText"];
            jobStatusTextBlock_output.Text = jobInfoDictionary["jobStatusText"];
            billingTypeTextBlock_output.Text = jobInfoDictionary["billingTypeText"];
            ParishTextBlock_output.Text = jobInfoDictionary["ParishText"];
            countyTextBlock_output.Text = jobInfoDictionary["countyText"];
            localGovtTextBlock_output.Text = jobInfoDictionary["localGovtText"];
            quotedTextBlock_output.Text = jobInfoDictionary["quotedText"];
            
            mgaTruncationTextBlock_output.Text = jobInfoDictionary["mgaTruncationText"];
            fld2TextBlock_output.Text = jobInfoDictionary["fld2Text"];
            architectEngineerTextBlock_output.Text = jobInfoDictionary["architectEngineerText"];
            builderTextBlock_output.Text = jobInfoDictionary["builderText"];
            siteAccessViaTextBlock_output.Text = jobInfoDictionary["siteAccessViaText"];
            contactNoTextBlock_output.Text = jobInfoDictionary["contactNoText"];
            jobCaptainTextBlock_output.Text = jobInfoDictionary["jobCaptainText"];
            postcodeTextBlock_output.Text = jobInfoDictionary["postcodeText"];
            qCloudRefTextBlock_output.Text = jobInfoDictionary["qCloudRefText"];
            startDateTextBlock_output.Text = jobInfoDictionary["startDateText"];     
            jobNameTextBlock_output.Text = jobInfoDictionary["jobNameText"];
            subCaptainTextBlock_output.Text = jobInfoDictionary["subCaptainText"];
            jobTypeTextBlock_output.Text = jobInfoDictionary["jobTypeText"];
            ubdTextBlock_output.Text = jobInfoDictionary["ubdText"];
            specialDetailsTextBlock_output.Text = jobInfoDictionary["specialDetailsText"]; 
            orderInvoiceTextBlock_output.Text = jobInfoDictionary["orderInvoiceText"];
            refNoTextBlock_output.Text = jobInfoDictionary["refNoText"];
            expectedTextBlock_output.Text = jobInfoDictionary["expectedText"];
            endDateTextBlock_output.Text = jobInfoDictionary["endDateText"];  //Index 35
            searchOrderedTextBlock_output.Text = jobInfoDictionary["searchOrderedText"];
            clientTextBlock_output.Text = jobInfoDictionary["clientText"];
            clientFullNameTextBlock_output.Text = jobInfoDictionary["clientFullNameText"];

            //glAccountTextBlock_output.Text = jobInfoDictionary["glAccountText"]; //This appears to be always null in latitude


            //-------Client Info (needs to come from a different table)
            addressLine1TextBlock_output.Text = clientInfoDictionary["addressLine1Text"];
            addressLine2TextBlock_output.Text = clientInfoDictionary["addressLine2Text"];
            clientphoneTextBlock_output.Text = clientInfoDictionary["clientphoneText"];   
            faxTextBlock_output.Text = clientInfoDictionary["faxText"];
            clientmobileTextBlock_output.Text = clientInfoDictionary["clientmobileText"];
            clientPostcodeTextBlock_Output.Text = clientInfoDictionary["clientPostcodeText"];
            personMobileTextBlock_output.Text = clientInfoDictionary["personMobileText"];
            personphoneTextBlock_output.Text = clientInfoDictionary["personphoneText"];
            instrPersonTextBlock_output.Text = clientInfoDictionary["contactSalutation"] + " " + clientInfoDictionary["contactGivenName"] + " " + clientInfoDictionary["contactSurname"];

        }






        private bool connectToDataBase(string jobNumber)
        {
            try
            {
                //clear dictionary
                clientInfoDictionary.Clear();
                jobInfoDictionary.Clear();
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = @"CMS-APP01\SQLEXPRESS";
                builder.UserID = "Felix";
                builder.Password = "LW14546fz";
                builder.InitialCatalog = "master";
                // Connect to SQL
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT [Job Date],[Job Description],[File Number],[File Number],[Street Number],[Street Name],[Locality],[Lot],[DP],[RE],[Work Status],[Billing Type],[Parish],[County],[LocalGovt],[curQuotedAmount],[InstructingPersonId],[txtJobUserField1],[txtJobUserField2],[txtJobUserField3],[txtJobUserField4],[txtJobUserField5],[txtJobUserField6],[strCaptainCode],[JobPostCode],[txtJobUserField10],[dteJobStart],[dteJobExpectedEnd],[txtJobName],[strSubCaptain],[JobType],[txtJobUserField8],[Special Details],[txtJobUserField9],[txtClientRefNo],[dteJobEndDate],[txtJobUserField7],[Client],[Instructing Person] FROM [LatidataSQL].[dbo].[tblJobs] WHERE [Job Number] = '" + jobNumber + "'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                jobInfoDictionary.Add("jobDateText", SafeGetString(reader, 0));
                                jobInfoDictionary.Add("jobDescriptionText", SafeGetString(reader, 1));
                                jobInfoDictionary.Add("jobNumberText", SafeGetString(reader, 2));
                                jobInfoDictionary.Add("packetRefText", SafeGetString(reader, 2));
                                jobInfoDictionary.Add("streetNumberText", SafeGetString(reader, 4));
                                jobInfoDictionary.Add("streetNameText", SafeGetString(reader, 5));
                                jobInfoDictionary.Add("suburbText", SafeGetString(reader, 6));
                                jobInfoDictionary.Add("lotText", SafeGetString(reader, 7));
                                jobInfoDictionary.Add("dPText", SafeGetString(reader, 8));
                                jobInfoDictionary.Add("reText", SafeGetString(reader, 9));
                                jobInfoDictionary.Add("jobStatusText", SafeGetString(reader, 10));
                                jobInfoDictionary.Add("billingTypeText", SafeGetString(reader, 11));
                                jobInfoDictionary.Add("ParishText", SafeGetString(reader, 12));
                                jobInfoDictionary.Add("countyText", SafeGetString(reader, 13));
                                jobInfoDictionary.Add("localGovtText", SafeGetString(reader, 14));
                                jobInfoDictionary.Add("quotedText", SafeGetString(reader, 15));
                                jobInfoDictionary.Add("instrPersonText", SafeGetString(reader, 16));
                                jobInfoDictionary.Add("mgaTruncationText", SafeGetString(reader, 17));
                                jobInfoDictionary.Add("fld2Text", SafeGetString(reader, 18));
                                jobInfoDictionary.Add("architectEngineerText", SafeGetString(reader, 19));
                                jobInfoDictionary.Add("builderText", SafeGetString(reader, 20));
                                jobInfoDictionary.Add("siteAccessViaText", SafeGetString(reader, 21));
                                jobInfoDictionary.Add("contactNoText", SafeGetString(reader, 22));
                                jobInfoDictionary.Add("jobCaptainText", SafeGetString(reader, 23));
                                jobInfoDictionary.Add("postcodeText", SafeGetString(reader, 24));
                                jobInfoDictionary.Add("qCloudRefText", SafeGetString(reader, 25));
                                jobInfoDictionary.Add("startDateText", SafeGetString(reader, 26));
                                jobInfoDictionary.Add("expectedText", SafeGetString(reader, 27));
                                jobInfoDictionary.Add("jobNameText", SafeGetString(reader, 28));
                                jobInfoDictionary.Add("subCaptainText", SafeGetString(reader, 29));
                                jobInfoDictionary.Add("jobTypeText", SafeGetString(reader, 30));
                                jobInfoDictionary.Add("ubdText",SafeGetString(reader, 31));
                                jobInfoDictionary.Add("specialDetailsText", SafeGetString(reader, 32));
                                jobInfoDictionary.Add("orderInvoiceText", SafeGetString(reader, 33));
                                jobInfoDictionary.Add("refNoText", SafeGetString(reader, 34));
                                jobInfoDictionary.Add("endDateText", SafeGetString(reader, 35));
                                jobInfoDictionary.Add("searchOrderedText", SafeGetString(reader, 36));
                                jobInfoDictionary.Add("clientText", SafeGetString(reader, 37));
                                jobInfoDictionary.Add("clientFullNameText", SafeGetString(reader, 38));

                            }
                        }
                    }

                    String clientCode = jobInfoDictionary["clientText"];
                    string sql2 = "SELECT [Postal Address],[Postal Suburb],[Phone],[Fax],[Mobile],[Postal Postcode] FROM [LatidataSQL].[dbo].[tblClients] WHERE [Client Code] = '" + clientCode + "'";
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientInfoDictionary.Add("addressLine1Text", SafeGetString(reader, 0));
                                clientInfoDictionary.Add("addressLine2Text", SafeGetString(reader, 1));
                                clientInfoDictionary.Add("clientphoneText", SafeGetString(reader, 2));
                                clientInfoDictionary.Add("faxText", SafeGetString(reader, 3));
                                clientInfoDictionary.Add("clientmobileText", SafeGetString(reader, 4));
                                clientInfoDictionary.Add("clientPostcodeText", SafeGetString(reader, 5));
                            }
                        }
                    }

                    String clientId = jobInfoDictionary["instrPersonText"];
                    string sql3 = "SELECT [ContactMobile],[ContactPhone],[ContactSalutation],[ContactGivenName],[ContactSurname] FROM [LatidataSQL].[dbo].[tblClientsContacts] WHERE [ContactID] = '" + clientId + "'";
                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientInfoDictionary.Add("personMobileText", SafeGetString(reader, 0));
                                clientInfoDictionary.Add("personphoneText", SafeGetString(reader, 1));
                                clientInfoDictionary.Add("contactSalutation", SafeGetString(reader, 2));
                                clientInfoDictionary.Add("contactGivenName", SafeGetString(reader, 3));
                                clientInfoDictionary.Add("contactSurname", SafeGetString(reader, 4));
                            }
                        }
                    }

                    connection.Close();

                    return true;
                }
            }
            catch (Exception ex)
            
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (reader.IsDBNull(colIndex) == false)
            {
                if (reader.GetDataTypeName(colIndex) == "nvarchar")
                {
                    return reader.GetString(colIndex);
                }
                else if (reader.GetDataTypeName(colIndex) == "float")           //for lat and long
                {
                    return reader.GetDouble(colIndex).ToString();
                }
                else if (reader.GetDataTypeName(colIndex) == "datetime")
                {
                    DateTime dt = reader.GetDateTime(colIndex);
                    return dt.ToString("dd MMM yyyy"); // returns day only, no time
                }
                else if (reader.GetDataTypeName(colIndex) == "money")
                {
                    return reader.GetDecimal(colIndex).ToString();
                }
                else if (reader.GetDataTypeName(colIndex) == "int")
                {
                    return reader.GetInt32(colIndex).ToString();
                }
                else if (reader.GetDataTypeName(colIndex) == "ntext")
                {
                    return reader.GetString(colIndex);
                }
            }
            return string.Empty;                //identical to ""
        }

    }
}
