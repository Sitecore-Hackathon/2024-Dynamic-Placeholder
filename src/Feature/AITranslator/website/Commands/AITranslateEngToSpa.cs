using System;
using System.Collections.Specialized;
using Sitecore;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace DynamicPlaceholder.Feature.AITranslator.Commands
{
    [Serializable]
    public class AITranslateEngToSpa : Command
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
                    Context.ClientPage.Start("aiTranslateEngToSpa", args);
                }
            }
            else
            {
                //Confirmation message to proceed with the process
                var itemName = args.Parameters["name"];
                string msg = $"Generating a spanish version of the item {itemName} ";
                SheerResponse.Confirm(msg);
                args.WaitForPostBack();
            }
        }
    }
}