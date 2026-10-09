using System;
using CORNDatabaseLayer.Classes;
using CORNDataAccessLayer.Classes;
using CORNCommon.Classes;
using System.Data;

namespace CORNBusinessLayer.Classes
{
    public class SyncServerController
    {
        public object GetLastUpdateDateTime(int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetUpdateDateTimeForSync ObjSelect = new uspGetUpdateDateTimeForSync();
                ObjSelect.Connection = mConnection;
                ObjSelect.TYPE = p_TYPE;

                object obj = ObjSelect.ExecuteScalar();
                return obj;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #region Distributor

        public string InsertDistributorClient(int p_DISTRIBUTOR_ID, int p_Company, int p_Dist_Class_Id, bool p_IsDeleted, DateTime p_Time_Stamp, DateTime p_LastUpdate_Date, int p_Division_Id, int p_User_Id, int p_Region_Id, int p_Zone_Id, int p_SubZone_Id, string p_Contact_Person, string p_Contact_Number, string p_Gst_Number, string p_Password, string p_Address1, string p_Address2, string p_Distributor_Code, string p_Distributor_Name, string p_Ip_Address, bool p_Is_Registered, int p_CREDIT_LEVEL)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTORClient mDistributorInfo = new spInsertDISTRIBUTORClient();
                mDistributorInfo.Connection = mConnection;
                mDistributorInfo.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDistributorInfo.COMPANY_ID = p_Company;
                mDistributorInfo.USER_ID = p_User_Id;
                mDistributorInfo.DIST_CLASS_ID = p_Dist_Class_Id;
                mDistributorInfo.TIME_STAMP = p_Time_Stamp;
                mDistributorInfo.LASTUPDATE_DATE = p_LastUpdate_Date;
                mDistributorInfo.DIVISION_ID = p_Division_Id;                
                mDistributorInfo.REGION_ID = p_Region_Id;
                mDistributorInfo.ZONE_ID = p_Zone_Id;
                mDistributorInfo.SUBZONE_ID = p_SubZone_Id;
                mDistributorInfo.CREDIT_LEVEL = p_CREDIT_LEVEL;
                mDistributorInfo.CONTACT_PERSON = p_Contact_Person;
                mDistributorInfo.CONTACT_NUMBER = p_Contact_Number;
                mDistributorInfo.GST_NUMBER = p_Gst_Number;
                mDistributorInfo.PASSWORD = p_Password;
                mDistributorInfo.ADDRESS1 = p_Address1;
                mDistributorInfo.ADDRESS2 = p_Address2;
                mDistributorInfo.DISTRIBUTOR_CODE = p_Distributor_Code;
                mDistributorInfo.DISTRIBUTOR_NAME = p_Distributor_Name;
                mDistributorInfo.IP_ADDRESS = p_Ip_Address;
                mDistributorInfo.ISDELETED = p_IsDeleted;
                mDistributorInfo.IS_REGISTERED = p_Is_Registered;
                mDistributorInfo.ExecuteQuery();

                return mDistributorInfo.DISTRIBUTOR_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public bool InsertUserAssignmentClient(int UserId, int DistributorTypeid, int DistributorId, int Companyid, DateTime p_TIME_STAMP)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTOR_ASSIGNMENTClient mUserAssing = new spInsertDISTRIBUTOR_ASSIGNMENTClient();
                mUserAssing.Connection = mConnection;
                mUserAssing.USER_ID = UserId;
                mUserAssing.DISTRIBUTOR_TYPE = DistributorTypeid;
                mUserAssing.DISTRIBUTOR_ID = DistributorId;
                mUserAssing.COMPANY_ID = Companyid;
                mUserAssing.TIME_STAMP = p_TIME_STAMP;
                bool flag = mUserAssing.ExecuteQuery();
                return flag;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion
        
        #region Role

        public bool InsertRoleDetailClient(int p_ROLE_ID, int p_MODULE_ID, DateTime p_TIME_STAMP)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertROLE_DETAILClient ObjInsert = new spInsertROLE_DETAILClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.ROLE_ID = p_ROLE_ID;
                ObjInsert.MODULE_ID = p_MODULE_ID;
                ObjInsert.TIME_STAMP = p_TIME_STAMP;
                bool Bvalue = ObjInsert.ExecuteQuery();
                return Bvalue;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertRoleMasterClient(int p_ROLE_ID, string p_ROLE_NAME, DateTime p_TIME_STAMP, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertROLE_MASTERClient ObjInsert = new spInsertROLE_MASTERClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.ROLE_ID = p_ROLE_ID;
                ObjInsert.ROLE_NAME = p_ROLE_NAME;
                ObjInsert.TIME_STAMP = p_TIME_STAMP;
                ObjInsert.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                bool Bvalue = ObjInsert.ExecuteQuery();
                return Bvalue;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region SKUS

        public bool Insert_SKU_GroupDetailClient(int p_SKU_GROUP_DETAIL_ID, int p_SKU_GROUP_ID, int p_SKU_ID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKU_GROUP_DETAILClient ObjInsert = new spInsertSKU_GROUP_DETAILClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.SKU_GROUP_DETAIL_ID = p_SKU_GROUP_DETAIL_ID;
                ObjInsert.SKU_GROUP_ID = p_SKU_GROUP_ID;
                ObjInsert.SKU_ID = p_SKU_ID;
                ObjInsert.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                bool Bvalue = ObjInsert.ExecuteQuery();
                return Bvalue;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool Insert_SKUGroupClient(int p_SKU_GROUP_ID, string p_GROUP_NAME, DateTime p_TIMESTAMP, DateTime p_LASTUPDATE_DATE, int p_UserId, int p_PrincipalId, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertSKU_GROUPClient ObjInsert = new spInsertSKU_GROUPClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.SKU_GROUP_ID = p_SKU_GROUP_ID;
                ObjInsert.GROUP_NAME = p_GROUP_NAME;
                ObjInsert.TIME_STAMP = p_TIMESTAMP;
                ObjInsert.COMPANY_ID = Companyid;
                ObjInsert.USER_ID = p_UserId;
                ObjInsert.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                ObjInsert.PRINCIPAL_ID = p_PrincipalId;
                ObjInsert.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public string InsertSKUSClient(int p_SKU_ID, bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, string p_Units_In_Case, string p_Sku_Code, string p_Sku_Name,
            string p_Ip_Address, string p_packSize, int p_UserId, int Companyid, string p_BarCode, string p_color, int p_skuTagId, string p_skuCountry, string p_skuSeason, DateTime p_TIME_STAMP, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUSClient mSkus = new spInsertSKUSClient();

                mSkus.Connection = mConnection;
                mSkus.SKU_ID = p_SKU_ID;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = Companyid;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                mSkus.COLOR = p_Units_In_Case;
                mSkus.BAR_CODE = p_BarCode;
                mSkus.SKU_TAG_ID = p_skuTagId;
                mSkus.SKU_SEASON = p_skuSeason;
                mSkus.SKU_COUNTRY = p_skuCountry;
                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = p_TIME_STAMP;
                mSkus.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;

                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public string InsertSKU_PRICESClient(int p_Distributor_Id, int p_Sku_Id, decimal p_Distributor_Discount, decimal p_Tax_Price, decimal p_Distributor_Price, decimal p_Trade_Price, decimal p_Retail_Price, DateTime p_date_effected, decimal p_SED_Tax, DateTime p_TIME_STAMP, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspProcessSKU_PricesClient mSkuPrices = new UspProcessSKU_PricesClient();
                mSkuPrices.Connection = mConnection;
                mSkuPrices.DISTRIBUTOR_ID = p_Distributor_Id;
                mSkuPrices.DISTRIBUTOR_PRICE = p_Distributor_Price;
                mSkuPrices.SKU_ID = p_Sku_Id;
                mSkuPrices.RETAIL_PRICE = p_Retail_Price;
                mSkuPrices.TAX_PRICE = p_Tax_Price;
                mSkuPrices.TRADE_PRICE = p_Trade_Price;
                mSkuPrices.DISTRIBUTOR_DISCOUNT = p_Distributor_Discount;
                mSkuPrices.TIME_STAMP = p_TIME_STAMP;
                mSkuPrices.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSkuPrices.DATE_EFFECTED = p_date_effected;
                mSkuPrices.SED_TAX = p_SED_Tax;
                mSkuPrices.ExecuteQuery();
                return "Record Inserted";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public string InsertSKUHierarchyClient(int p_SKU_HIE_ID, int p_Sku_Type_Id, int p_Parent_Sku_Hie_Id, string p_Sku_Hie_Code, string p_Sku_Hie_Name, string p_Ip_Address, bool p_Is_Active, int Companyid, bool p_IS_MANUALDISCOUNT, DateTime p_TIME_STAMP, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKU_HIERARCHYClient mSkuHierarchy = new spInsertSKU_HIERARCHYClient();
                mSkuHierarchy.Connection = mConnection;
                mSkuHierarchy.SKU_HIE_ID = p_SKU_HIE_ID;
                mSkuHierarchy.SKU_HIE_TYPE_ID = p_Sku_Type_Id;
                mSkuHierarchy.PARENT_SKU_HIE_ID = p_Parent_Sku_Hie_Id;
                mSkuHierarchy.SKU_HIE_CODE = p_Sku_Hie_Code;
                mSkuHierarchy.SKU_HIE_NAME = p_Sku_Hie_Name;
                mSkuHierarchy.TIME_STAMP = p_TIME_STAMP;
                mSkuHierarchy.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSkuHierarchy.IP_ADDRESS = p_Ip_Address;
                mSkuHierarchy.IS_ACTIVE = p_Is_Active;
                mSkuHierarchy.COMPANY_ID = Companyid;
                mSkuHierarchy.IS_MANUALDISCOUNT = p_IS_MANUALDISCOUNT;
                mSkuHierarchy.ExecuteQuery();
                return "Record Inserted";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Module

        public bool Insert_ModuleClient(int p_MODULE_ID, string p_MODULE_DESCRIPTION, int p_MODULE_PARENT_ID, int p_MODULE_TYPE_ID, DateTime p_TIME_STAMP, bool p_IS_ACTIVE, string p_MODULE_KEY)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertMODULEClient ObjInsert = new spInsertMODULEClient();
                ObjInsert.Connection = mConnection;
                ObjInsert.MODULE_ID = p_MODULE_ID;
                ObjInsert.MODULE_DESCRIPTION = p_MODULE_DESCRIPTION;
                ObjInsert.MODULE_PARENT_ID = p_MODULE_PARENT_ID;
                ObjInsert.MODULE_TYPE_ID = p_MODULE_TYPE_ID;
                ObjInsert.TIME_STAMP = p_TIME_STAMP;
                ObjInsert.IS_ACTIVE = p_IS_ACTIVE;
                ObjInsert.MODULE_KEY = p_MODULE_KEY;
                ObjInsert.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion        

        #region Employee

        public string InsertDistributor_UserClient(int p_USER_ID, int p_CompanyId, string p_NIC, bool p_Is_Active, DateTime p_Time_Stamp, DateTime p_Lastupdate_Date, int p_User_Type_Id, int p_Distributor_Id,
            int p_Role_Id, string p_Email_Address, string p_Address1, string p_Address2, string p_Login_Id, string p_Password, string p_Mobile, string p_User_Code, string p_User_Name, string p_Phone)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDISTRIBUTOR_USERClient mDistributorUser = new spInsertDISTRIBUTOR_USERClient();
                mDistributorUser.Connection = mConnection;
                mDistributorUser.USER_ID = p_USER_ID;
                mDistributorUser.COMPANY_ID = p_CompanyId;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.NIC_NO = p_NIC;
                mDistributorUser.ADDRESS1 = p_Address1;
                mDistributorUser.ADDRESS2 = p_Address2;
                mDistributorUser.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributorUser.EMAIL = p_Email_Address;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.LOGIN_ID = p_Login_Id;
                mDistributorUser.MOBILE = p_Mobile;
                mDistributorUser.PASSWORD = p_Password;
                mDistributorUser.PHONE = p_Phone;
                mDistributorUser.ROLE_ID = p_Role_Id;
                mDistributorUser.TIME_STAMP = p_Time_Stamp;
                mDistributorUser.DATE_JOIN = p_Time_Stamp;
                mDistributorUser.LASTUPDATE_DATE = p_Lastupdate_Date;
                mDistributorUser.USER_CODE = p_User_Code;
                mDistributorUser.USER_NAME = p_User_Name;
                mDistributorUser.USER_TYPE_ID = p_User_Type_Id;
                mDistributorUser.IS_ACTIVE = p_Is_Active;
                mDistributorUser.ExecuteQuery();
                return mDistributorUser.USER_ID.ToString();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Principal Assignment

        public void InsertAssignBrandClient(int p_companyId, int p_UserId, bool p_Is_ManualDiscount, DateTime p_TIME_STAMP)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertBRAND_ASSIGNMENTClient mInsertBrand = new spInsertBRAND_ASSIGNMENTClient();
                mInsertBrand.Connection = mConnection;
                mInsertBrand.TIME_STAMP = p_TIME_STAMP;
                mInsertBrand.PRINCIPAL_ID = p_companyId;
                mInsertBrand.Is_ManualDiscount = p_Is_ManualDiscount;
                mInsertBrand.USER_ID = p_UserId;
                mInsertBrand.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region User

        public string InserUserClient(int User_Id, int p_CompanyId, int p_DistributorId, string p_LoginId, string p_Password, int p_RoleId, bool p_IsDiscountAllowed, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsert_USERClient mSlashUser = new spInsert_USERClient();
                mSlashUser.Connection = mConnection;
                mSlashUser.USER_ID = User_Id;
                mSlashUser.DISTRIBUTOR_ID = p_DistributorId;
                mSlashUser.COMPANY_ID = p_CompanyId;
                mSlashUser.LOGIN_ID = p_LoginId;
                mSlashUser.PASSWORD = p_Password;
                mSlashUser.ROLE_ID = p_RoleId;
                mSlashUser.IsDiscountAllowed = p_IsDiscountAllowed;
                mSlashUser.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mSlashUser.ExecuteQuery();
                return mSlashUser.USER_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Purchase

        #region Master

        public bool InsertPurchaseMasterClient(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, 
            int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, decimal pDiscount, DateTime p_TIME_STAMP, DateTime p_LAST_UPDATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPURCHASE_MASTERClient mPurchaseMaster = new spInsertPURCHASE_MASTERClient();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = p_TIME_STAMP;
                mPurchaseMaster.LAST_UPDATE = p_LAST_UPDATE;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.DISCOUNT = pDiscount;
                mPurchaseMaster.ExecuteQuery();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);                
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Detail

        public bool InsertPurchaseDetailClient(long p_PURCHASE_DETAIL_ID, long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, int p_SKU_ID, string p_BATCH_NO, int p_QUANTITY, int p_FREE_SKU,
            decimal p_PRICE, decimal p_AMOUNT, DateTime p_TIME_STAMP, int p_TYPE_ID, decimal p_TRADE_PRICE, DateTime p_LAST_UPDATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPURCHASE_DETAILClient mPurchaseDetail = new spInsertPURCHASE_DETAILClient();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_DETAIL_ID = p_PURCHASE_DETAIL_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.SKU_ID = p_SKU_ID;
                mPurchaseDetail.BATCH_NO = p_BATCH_NO;
                mPurchaseDetail.QUANTITY = p_QUANTITY;
                mPurchaseDetail.FREE_SKU = p_FREE_SKU;
                mPurchaseDetail.PRICE = p_PRICE;
                mPurchaseDetail.AMOUNT = p_AMOUNT;
                mPurchaseDetail.TIME_STAMP = p_TIME_STAMP;
                mPurchaseDetail.TYPE_ID = p_TYPE_ID;
                mPurchaseDetail.TRADEPRICE = p_TRADE_PRICE;
                mPurchaseDetail.LAST_UPDATE = p_LAST_UPDATE;
                mPurchaseDetail.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #endregion

        #region Promotion

        public bool InsertSchemeClient(int p_SCHEME_ID, int p_Distributor_Id, string p_Scheme_Code, string p_Scheme_Desc, DateTime p_Scheme_Date, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertSCHEMEClient mScheme = new spInsertSCHEMEClient();
                mScheme.Connection = mConnection;
                mScheme.SCHEME_ID = p_SCHEME_ID;
                mScheme.DISTRIBUTOR_ID = p_Distributor_Id;
                mScheme.SCHEME_CODE = p_Scheme_Code;
                mScheme.SCHEME_DESC = p_Scheme_Desc;
                mScheme.SCHEME_DATE = p_Scheme_Date;
                mScheme.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mScheme.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertPromotionClient(long p_PROMOTION_ID, int p_Scheme_Id, int p_Distributor_Id, string p_Pro_Code, string p_Pro_Description, DateTime p_Promo_Date, bool p_Claimable, DateTime p_Start_Date, DateTime p_End_Date, bool p_Is_Active, int p_Promotion_Type, int p_Promotion_Selection, bool p_Is_Scheme, bool p_Promotion_For, int p_UserId, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertPROMOTIONClient mPromotion = new spInsertPROMOTIONClient();
                mPromotion.Connection = mConnection;
                mPromotion.PROMOTION_ID = p_PROMOTION_ID;
                mPromotion.SCHEME_ID = p_Scheme_Id;
                mPromotion.DISTRIBUTOR_ID = p_Distributor_Id;
                mPromotion.PROMOTION_CODE = p_Pro_Code;
                mPromotion.PROMOTION_DESCRIPTION = p_Pro_Description;
                mPromotion.PROMO_DATE = p_Promo_Date;
                mPromotion.CLAIMABLE = p_Claimable;
                mPromotion.START_DATE = p_Start_Date;
                mPromotion.END_DATE = p_End_Date;
                mPromotion.IS_ACTIVE = p_Is_Active;
                mPromotion.PROMOTION_TYPE = p_Promotion_Type;
                mPromotion.PROMOTION_SELECTION = p_Promotion_Selection;
                mPromotion.IS_SCHEME = p_Is_Scheme;
                mPromotion.PROMOTION_FOR = p_Promotion_For;
                mPromotion.USER_ID = p_UserId;
                mPromotion.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mPromotion.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertPromotionCustTypeClient(long p_PROMOTION_CUSTOMER_TYPE_ID, long p_PROMOTION_ID, int p_DISTRIBUTOR_ID, int p_SCHEME_ID, int p_CUSTOMER_TYPE_ID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertPROMOTION_CUSTOMER_TYPEClient mPromotion = new spInsertPROMOTION_CUSTOMER_TYPEClient();
                mPromotion.Connection = mConnection;
                mPromotion.PROMOTION_CUSTOMER_TYPE_ID = p_PROMOTION_CUSTOMER_TYPE_ID;
                mPromotion.PROMOTION_ID = p_PROMOTION_ID;
                mPromotion.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPromotion.SCHEME_ID = p_SCHEME_ID;
                mPromotion.CUSTOMER_TYPE_ID = p_CUSTOMER_TYPE_ID;
                mPromotion.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mPromotion.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertPromotionVolClass(long p_PROMOTION_VOLUMECLASS_ID, long p_Promotion_ID, int p_Scheme_ID, int p_Dist_ID, int p_CUSTOMER_VOLUMECLASS_ID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertPROMOTION_CUSTOMER_VOLUMECLASSClient mPromotion = new spInsertPROMOTION_CUSTOMER_VOLUMECLASSClient();
                mPromotion.Connection = mConnection;
                mPromotion.PROMOTION_VOLUMECLASS_ID = p_PROMOTION_VOLUMECLASS_ID;
                mPromotion.SCHEME_ID = p_Scheme_ID;
                mPromotion.PROMOTION_ID = p_Promotion_ID;
                mPromotion.DISTRIBUTOR_ID = p_Dist_ID;
                mPromotion.CUSTOMER_VOLUMECLASS_ID = p_CUSTOMER_VOLUMECLASS_ID;
                mPromotion.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mPromotion.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertPromotionDistClient(long p_PROMOTION_FOR_ID,long p_Promotion_ID, int p_Scheme_ID, int p_Dist_ID, int p_Assigned_Dist_ID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPROMOTION_FORClient mPromotion = new spInsertPROMOTION_FORClient();
                mPromotion.Connection = mConnection;
                mPromotion.PROMOTION_FOR_ID = p_PROMOTION_FOR_ID;
                mPromotion.PROMOTION_ID = p_Promotion_ID;
                mPromotion.DISTRIBUTOR_ID = p_Dist_ID;
                mPromotion.SCHEME_ID = p_Scheme_ID;
                mPromotion.ASSIGNED_DISTRIBUTOR_ID = p_Assigned_Dist_ID;
                mPromotion.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mPromotion.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertPromotionOfferClient(long p_PROMOTION_OFFER_ID, long p_Basket_ID, long p_Promotion_ID, int p_Scheme_ID, int p_Dist_ID, long p_BasketDetail_ID, int p_Quantity, decimal p_Offer_Value, float p_Discount, bool p_Is_And, int p_SKU_ID, int p_UOM_ID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertPROMOTION_OFFERClient mPromotion = new spInsertPROMOTION_OFFERClient();
                mPromotion.Connection = mConnection;
                mPromotion.PROMOTION_OFFER_ID = p_PROMOTION_OFFER_ID;
                mPromotion.IS_AND = p_Is_And;
                mPromotion.DISCOUNT = p_Discount;
                mPromotion.QUANTITY = p_Quantity;
                mPromotion.SKU_ID = p_SKU_ID;
                mPromotion.UOM_ID = p_UOM_ID;
                mPromotion.SCHEME_ID = p_Scheme_ID;
                mPromotion.DISTRIBUTOR_ID = p_Dist_ID;
                mPromotion.BASKET_DETAIL_ID = p_BasketDetail_ID;
                mPromotion.BASKET_ID = p_Basket_ID;
                mPromotion.PROMOTION_ID = p_Promotion_ID;
                mPromotion.OFFER_VALUE = p_Offer_Value;
                mPromotion.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mPromotion.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertBasketClient(long p_BASKET_ID,long p_Promotion_ID, int p_Scheme_ID, int p_Dist_ID, bool p_Is_Basket, bool p_Is_And, bool p_Is_Multiple, int p_Basket_On, int p_Basket_Selection, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBASKET_MASTERClient mBasket = new spInsertBASKET_MASTERClient();
                mBasket.Connection = mConnection;
                mBasket.BASKET_ID = p_BASKET_ID;
                mBasket.PROMOTION_ID = p_Promotion_ID;
                mBasket.SCHEME_ID = p_Scheme_ID;
                mBasket.DISTRIBUTOR_ID = p_Dist_ID;
                mBasket.IS_BASKET = p_Is_Basket;
                mBasket.IS_AND = p_Is_And;
                mBasket.IS_MULTIPLE = p_Is_Multiple;
                mBasket.BASKET_ON = p_Basket_On;
                mBasket.BASKET_SELECTION = p_Basket_Selection;
                mBasket.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mBasket.ExecuteQuery();
                return true;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool InsertBasketDetailClient(long p_BASKET_DETAIL_ID,long p_Basket_ID, long p_Promotion_ID, int p_Scheme_ID, int p_Dist_ID, decimal p_Min_Val, decimal p_Max_Val, int p_Multiple_Of, int p_SKU_ID, int p_SKUBrand_ID, int p_SKUDiv_ID, int p_SKUCatg_ID, int p_SKUVariant_ID, int p_SKUGroup_ID, int p_UOM_ID, int p_CompanyID, DateTime p_LASTUPDATE_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertBASKET_DETAILClient mBasket = new spInsertBASKET_DETAILClient();
                mBasket.Connection = mConnection;
                mBasket.BASKET_DETAIL_ID = p_BASKET_DETAIL_ID;
                mBasket.DISTRIBUTOR_ID = p_Dist_ID;
                mBasket.BASKET_ID = p_Basket_ID;
                mBasket.PROMOTION_ID = p_Promotion_ID;
                mBasket.CATEGORY_ID = p_SKUCatg_ID;
                mBasket.SKU_GROUP_ID = p_SKUGroup_ID;
                mBasket.UOM_ID = p_UOM_ID;
                mBasket.DIVISION_ID = p_SKUDiv_ID;
                mBasket.VARIANT_ID = p_SKUVariant_ID;
                mBasket.COMPANY_ID = p_CompanyID;
                mBasket.BRAND_ID = p_SKUBrand_ID;
                mBasket.SCHEME_ID = p_Scheme_ID;
                mBasket.MULTIPLE_OF = p_Multiple_Of;
                mBasket.SKU_ID = p_SKU_ID;
                mBasket.MIN_VAL = p_Min_Val;
                mBasket.MAX_VAL = p_Max_Val;
                mBasket.LASTUPDATE_DATE = p_LASTUPDATE_DATE;
                mBasket.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion
    }
}