using System;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Web.UI.Sheer;

namespace DynamicPlaceholder.Feature.AITranslator.Pipelines
{
    public class AITranslateEngToSpaPipeline
    {
        public void Execute(ClientPipelineArgs args)
        {
            try
            {
                //Get the item id as parameter
                string itemInput = args.Parameters["id"];
                Log.Debug($"Starting AI Translate From English to Spanish item {itemInput}  ");
                Assert.ArgumentNotNull(args, "args");
                //Get the database as parameter
                Database database = Factory.GetDatabase(args.Parameters["database"]);
                Assert.IsNotNull(database, args.Parameters["database"]);
                //If the database is different to master display a message
                if (database.Name != "master")
                {
                    SheerResponse.Alert("This process only runs in the master database", Array.Empty<string>());
                    args.AbortPipeline();
                    return;
                }
                //Get the item with the id
                Item item = database.GetItem(ID.Parse(itemInput));
                if (item == null)
                {
                    SheerResponse.Alert("Item not found.", Array.Empty<string>());
                    args.AbortPipeline();
                    return;
                }

                //Get the english version of the item
                Sitecore.Globalization.Language enLanguage = Sitecore.Data.Managers.LanguageManager.GetLanguage("en");
                Sitecore.Data.Items.Item englishVersion = database.GetItem(item.ID, enLanguage);
                //Get the spanish version of the item
                Sitecore.Globalization.Language esLanguage = Sitecore.Data.Managers.LanguageManager.GetLanguage("es-ES");
                Sitecore.Data.Items.Item spanishVersion = database.GetItem(item.ID, esLanguage);
                //If there is not spanish version or is the fallback dont execute the process
                if (spanishVersion == null || spanishVersion.IsFallback)
                    return;

                if (englishVersion == null)
                    return;

                //Grab the field that contains the final layout for english version
                var finalLayoutFieldEnglish = new LayoutField(englishVersion.Fields[Sitecore.FieldIDs.FinalLayoutField]);

                //Grab the field that contains the final layout for spanish version
                var finalLayoutFieldSpanish = new LayoutField(spanishVersion.Fields[Sitecore.FieldIDs.FinalLayoutField]);

                if (finalLayoutFieldEnglish == null)
                    return;

                //Get the final layout field value from english
                var finalLayoutDefinitionEnglish = LayoutDefinition.Parse(finalLayoutFieldEnglish.Value);

                ////Copy values from enlish version to the new language version
                //load a fields
                englishVersion.Fields.ReadAll();
                spanishVersion.Editing.BeginEdit();
                foreach (Field field in englishVersion.Fields)
                {
                    //Make sure to get content from used fields and non-sitecore fields (usually start with double underscore)
                    if (field != null && !field.Key.StartsWith("__"))
                    {
                        //Include the Debug entry for the field
                        Log.Debug($"Field copied VersionAddedEvent {field.Key}");
                        spanishVersion[field.Key] = englishVersion[field.Key];
                    }
                }
                finalLayoutFieldSpanish.Value = finalLayoutDefinitionEnglish.ToXml();
                spanishVersion.Editing.EndEdit();
                Log.Debug($"Finishing AI Translate From English to Spanish item {itemInput}  ");
                SheerResponse.Alert("Success", Array.Empty<string>());
                args.AbortPipeline();
                return;              
            }
            catch (Exception ex)
            {
                Log.Error("Error in AITranslateEngToSpaPipeline", ex,this);
                SheerResponse.ShowError($"An error has ocurred", ex.Message);
            }
        }
    }
}