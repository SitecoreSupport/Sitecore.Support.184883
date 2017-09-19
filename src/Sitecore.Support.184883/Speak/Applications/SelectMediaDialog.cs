namespace Sitecore.Support.Speak.Applications
{
    using Sitecore;
    using Sitecore.Data.Items;
    using Sitecore.Web;
    using System.Web;

    public class SelectMediaDialog : Sitecore.Speak.Applications.SelectMediaDialog
    {
        public override void Initialize()
        {
            string str4;
            string str5;
            string str6;
            string str7;
            RedirectOnItembucketsDisabled(ClientHost.Items.GetItem("{16227E67-F9CB-4FB7-9928-7FF6A529708E}"));
            string queryString = WebUtil.GetQueryString("ro");
            string folder = WebUtil.GetQueryString("fo");
            bool showFullPath = GetShowFullPath(folder);
            string str3 = WebUtil.GetQueryString("hasUploaded");
            if (!string.IsNullOrEmpty(str3) && (str3 == "1"))
            {
                this.DataSource.Parameters["SearchConfigItemId"] = "{1E723604-BFE0-47F6-B7C5-3E2FA6DD70BD}";
                this.Menu.Parameters["DefaultSelectedItemId"] = "{BE8CD31C-2A01-4ED6-9C83-E84C2275E429}";
            }
            // get an Item in the context language
            Item mediaItemFromQueryString = GetMediaItemFromQueryString(queryString) ?? ClientHost.Items.GetItem("/sitecore/media library");
            //mediaItemFromQueryString = this.GetRootItem(mediaItemFromQueryString);
            Item item2 = GetMediaItemFromQueryString(folder);
            if (mediaItemFromQueryString != null)
            {
                this.DataSource.Parameters["RootItemId"] = mediaItemFromQueryString.ID.ToString();
                // add the context language to parameters
                this.DataSource.Parameters["Language"] = mediaItemFromQueryString.Language.Name;
            }
            if (mediaItemFromQueryString == null)
            {
                mediaItemFromQueryString = ClientHost.Items.GetItem("/sitecore/media library");
            }
            this.MediaResultsListControl.Parameters["ContentLanguage"] = mediaItemFromQueryString.Language.ToString();
            this.MediaResultsListControl.Parameters["DefaultSelectedItemId"] = (item2 == null) ? mediaItemFromQueryString.ID.ToString() : item2.ID.ToString();
            this.MediaFolderValueText.Parameters["Text"] = GetDisplayPath(mediaItemFromQueryString.Paths.Path, null, showFullPath);
            this.TreeViewToggleButton.Parameters["Click"] = string.Format(this.TreeViewToggleButton.Parameters["Click"], HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(folder), showFullPath);
            FillCommandParts(this.UploadButton.Parameters["Click"], out str4, out str5, out str6, out str7);
            string str8 = SetUrlContentDatabase(str4, WebUtil.GetQueryString("sc_content"));
            string[] textArray1 = new string[] { str5, str7, str8, str7, str6 };
            string format = string.Concat(textArray1);
            this.UploadButton.Parameters["Click"] = string.Format(format, HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(folder), showFullPath);
        }
    }
}