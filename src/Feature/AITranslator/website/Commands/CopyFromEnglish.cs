using System;
using System.Collections.Specialized;
using Sitecore;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace DynamicPlaceholder.Feature.AITranslator.Commands
{
    [Serializable]
    public class CopyFromEnglish : Command
    {
        /// <summary>
        /// Command in "Copy English to Spanish" option
        /// </summary>
        /// <param name="context"></param>
        public override void Execute(CommandContext context)
        {
            if (context.Items.Length != 1 || context.Items[0] == null) return;
            var item = context.Items[0];
            var parameters = new NameValueCollection();
            //Pass the database name
            parameters.Add("database", item.Database.Name);
            //Pass the item id
            parameters.Add("id", item.ID.ToString());
            //Pass the item name
            parameters.Add("name", item.Name);
            Context.ClientPage.Start(this, "Run", parameters);
        }

        protected void Run(ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                if (args.Result == "yes")
                {
                    Context.ClientPage.Start("uiCopyFromEnglish", args);
                }
            }
            else
            {
                //Confirmation message to proceed with the process
                var itemName = args.Parameters["name"];
                string msg = $"Are you sure you want to copy the latest English content to the latest Spanish version for the item {itemName}?";
                SheerResponse.Confirm(msg);
                args.WaitForPostBack();
            }
        }
    }
}