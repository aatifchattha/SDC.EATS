using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace SDC.EATS.Plugins
{
    public class EIRReceivedCreate : IPlugin
    {
            #region Secure/Unsecure Configuration Setup
            private string _secureConfig = null;
            private string _unsecureConfig = null;

            public EIRReceivedCreate(string unsecureConfig, string secureConfig)
            {
                _secureConfig = secureConfig;
                _unsecureConfig = unsecureConfig;
            }
            #endregion
            public void Execute(IServiceProvider serviceProvider)
            {
                Guid EIRReceivedId = Guid.Empty;
                string AssignmentOfferName = string.Empty;
                string AssignmentOfferDate = string.Empty;
                Guid strEmailReceiverID = Guid.Empty;
                string CaseSubType = string.Empty;
                string EmailNotificationName = string.Empty;

                ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = factory.CreateOrganizationService(context.UserId);

                try
                {
                    if (context.MessageName.Equals("create", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (context.Depth > 1) { return; }
                        Entity entity = (Entity)context.InputParameters["Target"];
                        if (entity.LogicalName == "eat_eirreceived")
                        {
                            if (context.Stage == 40)
                            {
                                if (!context.OutputParameters.Contains("id")) { return; }
                                EIRReceivedId = new Guid(context.OutputParameters["id"].ToString());
                            }

                            tracer.Trace("AssignmentOfferCreatePlugin bypassed, email notification happening in workflow/email template. Plugin should be deactivated.");
                            //if (entity.Contains("ocl_name"))
                            //    AssignmentOfferName = entity["ocl_name"].ToString();
                            //CaseSubType = Tools.DataTools.GetCaseSubTypeName(service, entity.GetAttributeValue<EntityReference>("ocl_caseid").Id);
                            //if (String.IsNullOrEmpty(CaseSubType)) return;
                            //strEmailReceiverID = Tools.DataTools.GetContactIdByPersonId(service, entity.GetAttributeValue<EntityReference>("ocl_panelmember_id").Id);
                            //EmailNotificationName = "AssignmentOfferNotification";
                            //List<KeyValuePair<string, string>> mergeFields = new List<KeyValuePair<string, string>>();
                            //mergeFields.Add(new KeyValuePair<string, string>("«CaseSubType»", CaseSubType));
                            //Tools.DataTools.SendEmailToPanelMember(service, strEmailReceiverID, EmailNotificationName, mergeFields);
                        }

                    }
                }
                catch (Exception e)
                {
                    throw new InvalidPluginExecutionException(e.Message);
                }
            }
        
    }
}
