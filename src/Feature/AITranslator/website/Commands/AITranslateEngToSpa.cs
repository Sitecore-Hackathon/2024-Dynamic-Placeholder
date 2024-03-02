using Sitecore;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Specialized;

namespace DynamicPlaceholder.Feature.AITranslator.Commands
{
    /// <summary>
    /// Command bound to the menu for AI Translation
    /// </summary>
    [Serializable]
    public class AITranslateEngToSpa : Command
    {
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
                var itemName = args.Parameters["name"];
                string msg = $"Generating a Spanish version of the item:  {itemName} ";
                SheerResponse.Alert(msg);
                args.WaitForPostBack();
            }
        }
    }
}